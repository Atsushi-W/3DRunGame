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

    [Tooltip("機体の種類")]
    [SerializeField]
    private UnitType _unitType = default;

    [Tooltip("機体のメッシュ")]
    [SerializeField]
    private Mesh _mesh;

    [Tooltip("機体のマテリアル")]
    [SerializeField]
    private Material _material;

    [Tooltip("弾のプレハブ")]
    [SerializeField]
    private GameObject _bullet = default;

}

// 機体の種類
public enum UnitType
{
    White,
    Red,
    Blue,
    Yellow,
}
