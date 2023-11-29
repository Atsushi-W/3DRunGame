using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public EnemyType[] EnemyWave;

    // 現Waveカウント
    private int _count;

    private void Start()
    {
        _count = 0;
        WaveStart();
    }

    /// <summary>
    /// エネミー生成テスト
    /// </summary>
    public void CreateEnemy(int num)
    {
        // 仮のポジション
        //Vector3 pos = new Vector3(Random.Range(-20,20), 0, 130);

        //EnemyObjectPool.Instance.GetEnemyObject(pos, enemyDatas[num]);

        SpawnManager.Instance.Spawn((EnemyType)Enum.ToObject(typeof(EnemyType), num));
    }


    /// <summary>
    /// Waveのスタート
    /// </summary>
    public void WaveStart()
    {
        if (!SpawnManager.Instance.SpawnFlag)
        {
            SpawnManager.Instance.SetSpawnFlag(true);
        }

        // カウントがWaveリストの上限を超えてた場合はリセット
        if (_count > EnemyWave.Length)
        {
            _count = 0;
        }

        SpawnManager.Instance.Spawn(EnemyWave[_count]);
        _count++;
    }

}
