using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : SingletonMonoBehaviour<Player>
{
    public GameObject Bullet => _bullet;
    public float BulletDelay => _bulletDelay;
    public float BulletAttack => _bulletAttack;
    public float BulletSeconds => _bulletSeconds;
    public float Speed => _speed;

    [Tooltip("機体から射出する弾")]
    [SerializeField]
    private GameObject _bullet;
    [Tooltip("弾の発射間隔")]
    [SerializeField]
    private float _bulletDelay;
    [Tooltip("弾の威力")]
    [SerializeField]
    private float _bulletAttack;
    [Tooltip("弾の持続時間")]
    [SerializeField]
    private float _bulletSeconds;
    [Tooltip("機体の現HP")]
    [SerializeField]
    private float _hp;
    [Tooltip("機体のMAXHP")]
    [SerializeField]
    private float _maxHp;
    [Tooltip("機体のスピード")]
    [SerializeField]
    private float _speed;
    [Tooltip("HP表示用スライダー")]
    [SerializeField]
    private Slider _hpSlider;

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private Rigidbody _rigidbody;

    // speedを制御する
    public float moveForceMultiplier;
    // 水平移動時に機首を左右に向けるトルク
    public float yawTorqueMagnitude = 30.0f;
    // 水平移動時に機体を左右に傾けるトルク
    public float rollTorqueMagnitude = 30.0f;
    // バネのように姿勢を元に戻すトルク
    public float restoringTorqueMagnitude = 100.0f;

    // 弾の発射用フラグ
    private bool _shotFlag;

    // 現在の機体のモード
    public UnitType UnitType = UnitType.White;

    // 弾のデータ
    private Bullet _playerBullet;

    // 機体データ : Element0からWhite,Red,Blue,Yellow
    public UnitData[] unitDatas;

    protected override void Awake()
    {
        base.Awake();

        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

        // 揺れ続けるのを防ぐためにangularDragをある程度大きめの値にする
        _rigidbody.angularDrag = 20.0f;

        // Initilize
        _hp = _maxHp;
        _hpSlider.value = 1f;
        _playerBullet = _bullet.GetComponent<Bullet>();
        _bulletSeconds = _playerBullet.BulletSeconds;

        _shotFlag = true;
    }

    private void Update()
    {
        // 体力が0以下ならゲームオーバー(機体消失)
        if (_hp <= 0)
        {
            gameObject.SetActive(false);
        }

        // キー入力から横方向の値取得
        float x = Input.GetAxisRaw("Horizontal");

        // 移動方向に力を加える
        _rigidbody.AddForce(x * _speed, 0, 0);
        // 減速方向に力を加える
        _rigidbody.AddForce(moveForceMultiplier * -_rigidbody.velocity);

        // プレイヤーの入力に応じて姿勢をひねろうとするトルク
        Vector3 rotationTorque = new Vector3(0, x * yawTorqueMagnitude, -x * rollTorqueMagnitude);

        // 現在の姿勢のずれに比例した大きさで逆方向にひねろうとするトルク
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Vector3 forward = transform.forward;
        Vector3 restoringTorque = new Vector3(forward.y - up.z, right.z - forward.x, up.x - right.y) * restoringTorqueMagnitude;

        // 機体にトルクを加える
        _rigidbody.AddTorque(rotationTorque + restoringTorque);

        // ポジションの制限
        if (transform.position.x < -20 || transform.position.x > 20)
        {
            float posX = transform.position.x < 0 ? -20.0f : 20.0f;
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }

        // 弾の発射
        if (_shotFlag)
        {
            _shotFlag = false;
            StartCoroutine("ShotBullet");
        }
    }

    /// <summary>
    /// テストボタンからの呼び出し用
    /// </summary>
    /// <param name="num"></param>
    public void UnitChange(int num)
    {
        UnitType type = (UnitType)Enum.ToObject(typeof(UnitType), num);
        UnitChange(type);
    }

    /// <summary>
    /// 機体の変更
    /// </summary>
    /// <param name="type">機体のモード</param>
    public void UnitChange(UnitType type)
    {
        UnitData data = unitDatas[(int)type];
        // メッシュ変更
        _meshFilter.mesh = data.Mesh;
        // マテリアル変更
        Material[] materials = _meshRenderer.materials;
        materials[0] = data.Material;
        _meshRenderer.materials = materials;
    }

    /// <summary>
    /// 弾発射間隔変更
    /// </summary>
    /// <param name="type">減らすディレイ</param>
    public void UpdateBulletDelay(float bulletDelay)
    {
        _bulletDelay -= bulletDelay;
    }

    /// <summary>
    /// 弾威力変更
    /// </summary>
    /// <param name="type">増加威力</param>
    public void UpdateBulletAttack(float bulletAttack)
    {
        _bulletAttack += bulletAttack;
    }

    /// <summary>
    /// 弾の持続時間変更
    /// </summary>
    /// <param name="type">増加持続時間</param>
    public void UpdateBulletSeconds(float bulletSeconds)
    {
        _bulletSeconds += bulletSeconds;
    }

    /// <summary>
    /// 体力変更
    /// </summary>
    /// <param name="type">増加体力</param>
    public void UpdateMaxHelth(float helth)
    {
        _maxHp += helth;
        _hp += helth;
        UpdateHPValue();
    }

    /// <summary>
    /// スピード変更
    /// </summary>
    /// <param name="type">増加スピード</param>
    public void UpdateSpeed(float speed)
    {
        _speed += speed;
    }

    /// <summary>
    /// 弾の射出
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotBullet()
    {
        PlayerBulletObjectPool.Instance.GetBulletObject((transform.position + (transform.forward * 10)), _bullet, _bulletAttack);
        yield return new WaitForSeconds(_bulletDelay);
        _shotFlag = true;
    }

    /// <summary>
    /// HP表示のアップデート
    /// </summary>
    private void UpdateHPValue()
    {
        // 現HP / MAXHPの計算結果をスライダーのValueに代入
        _hpSlider.value = _hp / _maxHp;
    }

    /// <summary>
    /// エネミーとの接触時
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        // 接触がエネミーであれば
        if (collision.gameObject.tag == "Enemy")
        {
            // TODO:体力処理を入れる
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            _hp -= enemy.Hp;
            UpdateHPValue();
        }
    }

    /// <summary>
    /// 弾に当たった時
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // 弾の判定
        if (other.gameObject.tag == "EnemyBullet")
        {
            float attack = other.GetComponent<Bullet>().BulletAttack;

            // HPのアップデート
            _hp -= attack;
            UpdateHPValue();

            if (_hp <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
