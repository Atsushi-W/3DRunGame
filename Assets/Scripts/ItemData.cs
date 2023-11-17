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

    [Tooltip("�e�̔��ˊԊu")]
    [SerializeField]
    private float _bulletDelay;
    [Tooltip("�e�̈З̓A�b�v")]
    [SerializeField]
    private float _bulletAttack;
    [Tooltip("�e�̎�������")]
    [SerializeField]
    private float _bulletSeconds;
    [Tooltip("HP����")]
    [SerializeField]
    private float _hpUp;
    [Tooltip("�@�̂̃X�s�[�h")]
    [SerializeField]
    private float _speed;
}
