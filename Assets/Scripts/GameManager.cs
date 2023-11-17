using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public EnemyData[] enemyDatas;

    /// <summary>
    /// �G�l�~�[�����e�X�g
    /// </summary>
    public void CreateEnemy(int num)
    {
        // ���̃|�W�V����
        //Vector3 pos = new Vector3(Random.Range(-20,20), 0, 130);

        //EnemyObjectPool.Instance.GetEnemyObject(pos, enemyDatas[num]);

        SpawnManager.Instance.Spawn((EnemyType)Enum.ToObject(typeof(EnemyType), num));
    }

}
