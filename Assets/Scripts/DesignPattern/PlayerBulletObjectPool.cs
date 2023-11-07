using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �v���C���[�̒e�������Ǘ�����I�u�W�F�N�g�v�[��
/// </summary>
public class PlayerBulletObjectPool : SingletonMonoBehaviour<PlayerBulletObjectPool>
{
    private Dictionary<int, List<GameObject>> pooledBulletObjects = new Dictionary<int, List<GameObject>>();

    
    public GameObject GetBulletObject(Vector3 pos, GameObject bullet, float power)
    {
        int key = bullet.GetInstanceID();

        // Dictionary��key�����݂��Ȃ���΍쐬
        if (pooledBulletObjects.ContainsKey(key) == false)
        {
            pooledBulletObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> bulletObjects = pooledBulletObjects[key];
        GameObject go = null;

        for (int i = 0; i < bulletObjects.Count; i++)
        {
            go = bulletObjects[i];

            // ���ݔ�A�N�e�B�u�i���g�p�j�ł����
            if (go.gameObject.activeInHierarchy == false)
            {
                // �ʒu�Ɗp�x��ݒ肵Active�ɂ���
                go.transform.position = pos;
                go.transform.rotation = Quaternion.identity;
                // �e�̈З͂�ݒ�
                go.GetComponent<Bullet>().Initialize(power);
                go.gameObject.SetActive(true);

                return go;
            }

        }
        // �g�p�ł�����̂��Ȃ��ꍇ�͐�������
        go = Instantiate(bullet, pos, Quaternion.identity);
        go.name = bullet.name;
        go.transform.parent = transform;
        // �e�̈З͂�ݒ�
        go.GetComponent<Bullet>().Initialize(power);
        bulletObjects.Add(go);

        return go;
    }
}
