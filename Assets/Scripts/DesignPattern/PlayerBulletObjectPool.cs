using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// プレイヤーの弾生成を管理するオブジェクトプール
/// </summary>
public class PlayerBulletObjectPool : SingletonMonoBehaviour<PlayerBulletObjectPool>
{
    private Dictionary<int, List<GameObject>> pooledBulletObjects = new Dictionary<int, List<GameObject>>();

    
    public GameObject GetBulletObject(Vector3 pos, GameObject bullet, float power)
    {
        int key = bullet.GetInstanceID();

        // Dictionaryにkeyが存在しなければ作成
        if (pooledBulletObjects.ContainsKey(key) == false)
        {
            pooledBulletObjects.Add(key, new List<GameObject>());
        }

        List<GameObject> bulletObjects = pooledBulletObjects[key];
        GameObject go = null;

        for (int i = 0; i < bulletObjects.Count; i++)
        {
            go = bulletObjects[i];

            // 現在非アクティブ（未使用）であれば
            if (go.gameObject.activeInHierarchy == false)
            {
                // 位置と角度を設定しActiveにする
                go.transform.position = pos;
                go.transform.rotation = Quaternion.identity;
                // 弾の威力を設定
                go.GetComponent<Bullet>().Initialize(power);
                go.gameObject.SetActive(true);

                return go;
            }

        }
        // 使用できるものがない場合は生成する
        go = Instantiate(bullet, pos, Quaternion.identity);
        go.name = bullet.name;
        go.transform.parent = transform;
        // 弾の威力を設定
        go.GetComponent<Bullet>().Initialize(power);
        bulletObjects.Add(go);

        return go;
    }
}
