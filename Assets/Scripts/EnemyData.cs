using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject
{
    public EnemyType EnemyType => _enemyType;
    public GameObject EnemyPrefab => _enemyprefab;
    public float EnemyHp => _enemyHp;
    public float EnemyAttack => _enemyAttack;

    [Tooltip("�G�l�~�[�̎��")]
    [SerializeField]
    private EnemyType _enemyType = default;

    [Tooltip("�G�l�~�[�̃v���n�u")]
    [SerializeField]
    private GameObject _enemyprefab = default;

    [Tooltip("�G�l�~�[�̃x�[�X�̗�")]
    [SerializeField]
    private float _enemyHp;

    [Tooltip("�G�l�~�[�̃x�[�X�U����")]
    [SerializeField]
    private float _enemyAttack;
}

// �G�l�~�[�̎��
public enum EnemyType
{
    Mob,
    MiddleBoss,
    BigBoss,
}
