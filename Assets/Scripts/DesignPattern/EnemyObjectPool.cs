using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �G�l�~�[�̐������Ǘ�����I�u�W�F�N�g�v�[��
/// </summary>
public class EnemyObjectPool : SingletonMonoBehaviour<EnemyObjectPool>
{
    private Dictionary<int, List<GameObject>> pooledEnemyObjects = new Dictionary<int, List<GameObject>>();

    
    public GameObject GetEnemyObject(Vector3 pos, EnemyData enemy)
    {
        int key = enemy.EnemyPrefab.GetInstanceID();

        // Dictionary��key�����݂��Ȃ���΍쐬
        if (pooledEnemyObjects.ContainsKey(key) == false)
        {
            pooledEnemyObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> enemyObjects = pooledEnemyObjects[key];
        GameObject go = null;

        for (int i = 0; i < enemyObjects.Count; i++)
        {
            go = enemyObjects[i];

            // ���ݔ�A�N�e�B�u�i���g�p�j�ł����
            if (go.gameObject.activeInHierarchy == false)
            {
                // �ʒu�Ɗp�x��ݒ肵Active�ɂ���
                go.transform.position = pos;
                go.transform.rotation = Quaternion.identity;
                Quaternion _rot = Quaternion.Euler(0, 180, 0);
                Quaternion _q = go.transform.rotation;
                go.transform.rotation = _q * _rot;
                // HP�̐ݒ�
                go.GetComponent<Enemy>().Initialize(enemy);

                go.gameObject.SetActive(true);

                return go;
            }

        }
        // �g�p�ł�����̂��Ȃ��ꍇ�͐�������
        go = Instantiate(enemy.EnemyPrefab, pos, Quaternion.identity);
        // �t�H���[�h��������O�ɂ���
        Quaternion rot = Quaternion.Euler(0, 180, 0);
        Quaternion q = go.transform.rotation;
        go.transform.rotation = q * rot;
        go.name = enemy.EnemyPrefab.name;
        go.transform.parent = transform;
        // HP�̐ݒ�
        go.GetComponent<Enemy>().Initialize(enemy);

        enemyObjects.Add(go);

        return go;
    }
}
