using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float bulletpos;

    public float Hp => _hp;
    public EnemyData EnemyData => _enemyData;

    [Tooltip("エネミーデータ")]
    [SerializeField]
    private EnemyData _enemyData;

    [Tooltip("エネミーの体力")]
    [SerializeField]
    private float _hp;

    [Tooltip("エネミーのHP表示用スライダー")]
    [SerializeField]
    private Slider _hpSlider;

    [Tooltip("Mob用アイテムリスト")]
    [SerializeField]
    private GameObject[] _ItemList;

    // 弾の発射用フラグ
    private bool _shotFlag;

    private void Update()
    {
        if (_enemyData != null)
        {
            switch (_enemyData.EnemyType)
            {
                case EnemyType.MiddleBoss:
                    // 弾の発射
                    if (_shotFlag)
                    {
                        _shotFlag = false;
                        StartCoroutine("ShotBullet");
                    }
                    break;
            }
        }
    }

    public void Initialize(EnemyData data)
    {
        _hp = data.EnemyHp;
        _enemyData = data;
        _shotFlag = true;
        _hpSlider.value = 1f;
    }

    // 弾が当たった時
    private void OnTriggerEnter(Collider other)
    {
        // 弾の判定
        if (other.gameObject.tag == "Bullet")
        {
            float attack = other.GetComponent<Bullet>().BulletAttack;

            // HPのアップデート
            _hp -= attack;
            UpdateHPValue();

            if (_hp <= 0)
            {
                if (_enemyData.EnemyType == EnemyType.Mob)
                {
                    GameObject go = Instantiate(_ItemList[Random.Range(0,_ItemList.Length)], gameObject.transform.position, Quaternion.identity);
                    Quaternion _rot = Quaternion.Euler(0, 180, 0);
                    Quaternion _q = go.transform.rotation;
                    go.transform.rotation = _q * _rot;
                }

                gameObject.SetActive(false);
            }
        }  
    }

    /// <summary>
    /// HP表示のアップデート
    /// </summary>
    private void UpdateHPValue()
    {
        // 現HP / MAXHPの計算結果をスライダーのValueに代入
        _hpSlider.value = _hp / _enemyData.EnemyHp;
    }

    // 機体(プレイヤーなど)が当たった時
    private void OnCollisionEnter(Collision collision)
    {
        // 接触がエネミーなら即リターンする
        if (collision.gameObject.tag == "Enemy")
        {
            return;
        }

        // 接触がプレイヤーの場合
        if (collision.gameObject.tag == "Player")
        {
            if (_enemyData.EnemyType == EnemyType.Mob)
            {
                GameObject go = Instantiate(_ItemList[Random.Range(0, _ItemList.Length)], gameObject.transform.position, Quaternion.identity);
                Quaternion _rot = Quaternion.Euler(0, 180, 0);
                Quaternion _q = go.transform.rotation;
                go.transform.rotation = _q * _rot;
            }
            // 非表示にする
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 弾の射出
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShotBullet()
    {
        GameObject go = Instantiate(_enemyData.EnemyBullet, (transform.position + (transform.forward * bulletpos)), Quaternion.identity);
        Quaternion _rot = Quaternion.Euler(0, 180, 0);
        Quaternion _q = go.transform.rotation;
        go.transform.rotation = _q * _rot;
        yield return new WaitForSeconds(2);
        _shotFlag = true;
    }
}

