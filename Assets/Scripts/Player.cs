using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Collider _collider;
    private Rigidbody _rigidbody;

    public GameObject bullet;
    public float bulletDelay;

    public float speed;

    // speed�𐧌䂷��
    public float moveForceMultiplier;
    // �����ړ����ɋ@������E�Ɍ�����g���N
    public float yawTorqueMagnitude = 30.0f;
    // �����ړ����ɋ@�̂����E�ɌX����g���N
    public float rollTorqueMagnitude = 30.0f;
    // �o�l�̂悤�Ɏp�������ɖ߂��g���N
    public float restoringTorqueMagnitude = 100.0f;

    // �e�̔��˗p�t���O
    private bool _shotFlag;

    // ���݂̋@�̂̃��[�h
    public UnitType UnitType = UnitType.White;

    // �@�̃f�[�^ : Element0����White,Red,Blue,Yellow
    public UnitData[] unitDatas;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();

        // �h�ꑱ����̂�h�����߂�angularDrag��������x�傫�߂̒l�ɂ���
        _rigidbody.angularDrag = 20.0f;

        _shotFlag = true;
    }

    private void Update()
    {
        // �L�[���͂��牡�����̒l�擾
        float x = Input.GetAxisRaw("Horizontal");

        // �ړ������ɗ͂�������
        _rigidbody.AddForce(x * speed, 0, 0);
        // ���������ɗ͂�������
        _rigidbody.AddForce(moveForceMultiplier * -_rigidbody.velocity);

        // �v���C���[�̓��͂ɉ����Ďp�����Ђ˂낤�Ƃ���g���N
        Vector3 rotationTorque = new Vector3(0, x * yawTorqueMagnitude, -x * rollTorqueMagnitude);

        // ���݂̎p���̂���ɔ�Ⴕ���傫���ŋt�����ɂЂ˂낤�Ƃ���g���N
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Vector3 forward = transform.forward;
        Vector3 restoringTorque = new Vector3(forward.y - up.z, right.z - forward.x, up.x - right.y) * restoringTorqueMagnitude;

        // �@�̂Ƀg���N��������
        _rigidbody.AddTorque(rotationTorque + restoringTorque);

        // �|�W�V�����̐���
        if (transform.position.x < -20 || transform.position.x > 20)
        {
            float posX = transform.position.x < 0 ? -20.0f : 20.0f;
            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }

        // �e�̔���
        if (_shotFlag)
        {
            _shotFlag = false;
            StartCoroutine("ShotBullet");
        }
    }

    /// <summary>
    /// �e�X�g�{�^������̌Ăяo���p
    /// </summary>
    /// <param name="num"></param>
    public void UnitChange(int num)
    {
        UnitType type = (UnitType)Enum.ToObject(typeof(UnitType), num);
        UnitChange(type);
    }

    /// <summary>
    /// �@�̂̕ύX
    /// </summary>
    /// <param name="type">�@�̂̃��[�h</param>
    public void UnitChange(UnitType type)
    {
        UnitData data = unitDatas[(int)type];
        // ���b�V���ύX
        _meshFilter.mesh = data.Mesh;
        // �}�e���A���ύX
        Material[] materials = _meshRenderer.materials;
        materials[0] = data.Material;
        _meshRenderer.materials = materials;
    }

    private IEnumerator ShotBullet()
    {
        Instantiate(bullet, (transform.position + (transform.forward * 10)), Quaternion.identity);
        yield return new WaitForSeconds(bulletDelay);
        _shotFlag = true;
    }

}
