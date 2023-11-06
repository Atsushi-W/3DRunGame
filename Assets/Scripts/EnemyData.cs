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

    [Tooltip("エネミーの種類")]
    [SerializeField]
    private EnemyType _enemyType = default;

    [Tooltip("エネミーのプレハブ")]
    [SerializeField]
    private GameObject _enemyprefab = default;

    [Tooltip("エネミーのベース体力")]
    [SerializeField]
    private float _enemyHp;

    [Tooltip("エネミーのベース攻撃力")]
    [SerializeField]
    private float _enemyAttack;
}

// エネミーの種類
public enum EnemyType
{
    Mob,
    MiddleBoss,
    BigBoss,
}
