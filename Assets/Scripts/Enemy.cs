using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float bulletpos;

    public float Hp => _hp;
    public EnemyData EnemyData => _enemyData;

    [Tooltip("�G�l�~�[�f�[�^")]
    [SerializeField]
    private EnemyData _enemyData;

    [Tooltip("�G�l�~�[�̗̑�")]
    [SerializeField]
    private float _hp;

    // �e�̔��˗p�t���O
    private bool _shotFlag;

    private void Update()
    {
        if (_enemyData != null)
        {
            switch (_enemyData.EnemyType)
            {
                case EnemyType.MiddleBoss:
                    // �e�̔���
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

    // �e������������
    private void OnTriggerEnter(Collider other)
    {
        float attack = other.GetComponent<Bullet>().BulletAttack;

        _hp -= attack;

        if (_hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    // �@��(�v���C���[�Ȃ�)������������
    private void OnCollisionEnter(Collision collision)
    {
        // �ڐG���v���C���[�̏ꍇ
        if (collision.gameObject.tag == "Player")
        {
            // TODO:�̗͏���������
        }

        // ��\���ɂ���
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �e�̎ˏo
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

