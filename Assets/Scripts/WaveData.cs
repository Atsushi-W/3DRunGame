using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/WaveData")]
public class WaveData : ScriptableObject
{
    public List<EnemyData> EnemyList => _enemyList;

    [Tooltip("Wave�G�l�~�[�̃��X�g")]
    [SerializeField]
    private List<EnemyData> _enemyList;
}