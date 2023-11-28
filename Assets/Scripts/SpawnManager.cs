using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : SingletonMonoBehaviour<SpawnManager>
{
    public bool SpawnFlag => _spawnFlag;

    [Tooltip("MobWave")]
    [SerializeField]
    private WaveData _mobWave;

    [Tooltip("MiddleBossWave")]
    [SerializeField]
    private WaveData _middleBossWave;

    [Tooltip("BossWave")]
    [SerializeField]
    private WaveData _bossWave;

    [Tooltip("テスト用のスポーンスイッチ")]
    [SerializeField]
    private bool _spawnFlag;

    public void SetSpawnFlag(bool flag)
    {
        _spawnFlag = flag;
    }

    public bool Spawn(EnemyType enemyType)
    {
        if (_spawnFlag)
        {
            _spawnFlag = false;
            StartCoroutine("EnemySpawn", enemyType);
            return true;
        }

        return false;
    }

    IEnumerator EnemySpawn(EnemyType enemyType)
    {
        Vector3 pos = new Vector3(0, 0, 130);

        switch (enemyType)
        {
            case EnemyType.Mob:
                for (int i = 0; i < _mobWave.EnemyList.Count; i++)
                {
                    pos = new Vector3(Random.Range(-20, 20), 0, 130);
                    EnemyObjectPool.Instance.GetEnemyObject(pos, _mobWave.EnemyList[i]);
                    yield return new WaitForSeconds(7.5f);
                }
                _spawnFlag = true;
                GameManager.Instance.WaveStart();
                break;
            case EnemyType.MiddleBoss:
                EnemyObjectPool.Instance.GetEnemyObject(pos, _middleBossWave.EnemyList[0]);
                yield return new WaitForSeconds(10f);
                //_spawnFlag = true;
                GameManager.Instance.WaveStart();
                break;
            case EnemyType.BigBoss:
                EnemyObjectPool.Instance.GetEnemyObject(pos, _bossWave.EnemyList[0]);
                yield return new WaitForSeconds(10f);
                // ラストボスは倒されたら_spawnFlagを入れる
                //_spawnFlag = true;
                //GameManager.Instance.WaveStart();
                break;
        }


    }
}
