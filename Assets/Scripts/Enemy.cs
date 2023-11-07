using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    // 弾が当たった時
    private void OnTriggerEnter(Collider other)
    {
        float attack = other.GetComponent<Bullet>().BulletAttack;

        _hp -= attack;

        if (_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // 機体(プレイヤーなど)が当たった時
    private void OnCollisionEnter(Collision collision)
    {
        // 接触がプレイヤーの場合
        if (collision.gameObject.tag == "Player")
        {
            // TODO:体力処理を入れる
        }

        // 非表示にする
        gameObject.SetActive(false);
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

