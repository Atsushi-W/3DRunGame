using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/UnitData")]
public class UnitData : ScriptableObject
{
    public UnitType UnitType => _unitType;
    public Mesh Mesh => _mesh;
    public Material Material => _material;
    public GameObject Bullet => _bullet;

    [Tooltip("�@�̂̎��")]
    [SerializeField]
    private UnitType _unitType = default;

    [Tooltip("�@�̂̃��b�V��")]
    [SerializeField]
    private Mesh _mesh;

    [Tooltip("�@�̂̃}�e���A��")]
    [SerializeField]
    private Material _material;

    [Tooltip("�e�̃v���n�u")]
    [SerializeField]
    private GameObject _bullet = default;

}

// �@�̂̎��
public enum UnitType
{
    White,
    Red,
    Blue,
    Yellow,
}
