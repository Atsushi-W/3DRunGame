using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// エネミーの生成を管理するオブジェクトプール
/// </summary>
public class EnemyObjectPool : SingletonMonoBehaviour<EnemyObjectPool>
{
    private Dictionary<int, List<GameObject>> pooledEnemyObjects = new Dictionary<int, List<GameObject>>();

    
    public GameObject GetEnemyObject(Vector3 pos, EnemyData enemy)
    {
        int key = enemy.EnemyPrefab.GetInstanceID();

        // Dictionaryにkeyが存在しなければ作成
        if (pooledEnemyObjects.ContainsKey(key) == false)
        {
            pooledEnemyObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> enemyObjects = pooledEnemyObjects[key];
        GameObject go = null;

        for (int i = 0; i < enemyObjects.Count; i++)
        {
            go = enemyObjects[i];

            // 現在非アクティブ（未使用）であれば
            if (go.gameObject.activeInHierarchy == false)
            {
                // 位置と角度を設定しActiveにする
                go.transform.position = pos;
                go.transform.rotation = Quaternion.identity;
                Quaternion _rot = Quaternion.Euler(0, 180, 0);
                Quaternion _q = go.transform.rotation;
                go.transform.rotation = _q * _rot;
                // HPの設定
                go.GetComponent<Enemy>().Initialize(enemy);

                go.gameObject.SetActive(true);

                return go;
            }

        }
        // 使用できるものがない場合は生成する
        go = Instantiate(enemy.EnemyPrefab, pos, Quaternion.identity);
        // フォワード方向を手前にする
        Quaternion rot = Quaternion.Euler(0, 180, 0);
        Quaternion q = go.transform.rotation;
        go.transform.rotation = q * rot;
        go.name = enemy.EnemyPrefab.name;
        go.transform.parent = transform;
        // HPの設定
        go.GetComponent<Enemy>().Initialize(enemy);

        enemyObjects.Add(go);

        return go;
    }
}
