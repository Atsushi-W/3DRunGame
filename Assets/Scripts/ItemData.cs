using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    public float BulletDelay => _bulletDelay;
    public float BulletAttack => _bulletAttack;
    public float BulletSeconds => _bulletSeconds;
    public float HpUp => _hpUp;
    public float Speed => _speed;

    [Tooltip("弾の発射間隔")]
    [SerializeField]
    private float _bulletDelay;
    [Tooltip("弾の威力アップ")]
    [SerializeField]
    private float _bulletAttack;
    [Tooltip("弾の持続時間")]
    [SerializeField]
    private float _bulletSeconds;
    [Tooltip("HP増加")]
    [SerializeField]
    private float _hpUp;
    [Tooltip("機体のスピード")]
    [SerializeField]
    private float _speed;
}
