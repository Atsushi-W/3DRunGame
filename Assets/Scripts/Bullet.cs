using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletAttack => _bulletAttack;
    public float BulletSeconds => _bulletSeconds;

    [Tooltip("’e‚ÌˆÐ—Í")]
    [SerializeField]
    private float _bulletAttack;
    [Tooltip("’e‚ÌŽ‘±ŽžŠÔ")]
    [SerializeField]
    private float _bulletSeconds;

    // •b”ƒJƒEƒ“ƒg—p
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

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}
