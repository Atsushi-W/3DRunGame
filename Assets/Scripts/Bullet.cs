using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletAttack => _bulletAttack;
    public float BulletSeconds => _bulletSeconds;

    [Tooltip("�e�̈З�")]
    [SerializeField]
    private float _bulletAttack;
    [Tooltip("�e�̎�������")]
    [SerializeField]
    private float _bulletSeconds;

    // �b���J�E���g�p
    private float _seconds;

    private void Update()
    {
        _seconds += Time.deltaTime;
        if (_seconds >= _bulletSeconds)
        {
            _seconds = 0;
            gameObject.SetActive(false);
        }
    }

    public void Initialize(float power)
    {
        _bulletAttack = power;
    }

    public void UpdateBulletSeconds(float seconds)
    {
        _bulletSeconds = seconds;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
