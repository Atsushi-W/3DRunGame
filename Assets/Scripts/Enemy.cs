using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Tooltip("�G�l�~�[��HP�\���p�X���C�_�[")]
    [SerializeField]
    private Slider _hpSlider;

    [Tooltip("Mob�p�A�C�e�����X�g")]
    [SerializeField]
    private GameObject[] _ItemList;

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
        _hpSlider.value = 1f;
    }

    // �e������������
    private void OnTriggerEnter(Collider other)
    {
        // �e�̔���
        if (other.gameObject.tag == "Bullet")
        {
            float attack = other.GetComponent<Bullet>().BulletAttack;

            // HP�̃A�b�v�f�[�g
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
    /// HP�\���̃A�b�v�f�[�g
    /// </summary>
    private void UpdateHPValue()
    {
        // ��HP / MAXHP�̌v�Z���ʂ��X���C�_�[��Value�ɑ��
        _hpSlider.value = _hp / _enemyData.EnemyHp;
    }

    // �@��(�v���C���[�Ȃ�)������������
    private void OnCollisionEnter(Collision collision)
    {
        // �ڐG���G�l�~�[�Ȃ瑦���^�[������
        if (collision.gameObject.tag == "Enemy")
        {
            return;
        }

        // �ڐG���v���C���[�̏ꍇ
        if (collision.gameObject.tag == "Player")
        {
            if (_enemyData.EnemyType == EnemyType.Mob)
            {
                GameObject go = Instantiate(_ItemList[Random.Range(0, _ItemList.Length)], gameObject.transform.position, Quaternion.identity);
                Quaternion _rot = Quaternion.Euler(0, 180, 0);
                Quaternion _q = go.transform.rotation;
                go.transform.rotation = _q * _rot;
            }
            // ��\���ɂ���
            gameObject.SetActive(false);
        }
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

