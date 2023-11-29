using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public EnemyType[] EnemyWave;

    // ��Wave�J�E���g
    private int _count;

    private void Start()
    {
        _count = 0;
        WaveStart();
    }

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


    /// <summary>
    /// Wave�̃X�^�[�g
    /// </summary>
    public void WaveStart()
    {
        if (!SpawnManager.Instance.SpawnFlag)
        {
            SpawnManager.Instance.SetSpawnFlag(true);
        }

        // �J�E���g��Wave���X�g�̏���𒴂��Ă��ꍇ�̓��Z�b�g
        if (_count > EnemyWave.Length)
        {
            _count = 0;
        }

        SpawnManager.Instance.Spawn(EnemyWave[_count]);
        _count++;
    }

}
