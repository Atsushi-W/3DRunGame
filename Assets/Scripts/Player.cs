using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private Rigidbody _rigidbody;

    public GameObject bullet;
    public float bulletDelay;

    public float speed;

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

    // 機体データ : Element0からWhite,Red,Blue,Yellow
    public UnitData[] unitDatas;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

        // 揺れ続けるのを防ぐためにangularDragをある程度大きめの値にする
        _rigidbody.angularDrag = 20.0f;

        _shotFlag = true;
    }

    private void Update()
    {
        // キー入力から横方向の値取得
        float x = Input.GetAxisRaw("Horizontal");

        // 移動方向に力を加える
        _rigidbody.AddForce(x * speed, 0, 0);
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

    private IEnumerator ShotBullet()
    {
        Instantiate(bullet, (transform.position + (transform.forward * 10)), Quaternion.identity);
        yield return new WaitForSeconds(bulletDelay);
        _shotFlag = true;
    }

}
