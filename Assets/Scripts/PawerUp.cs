using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawerUp : MonoBehaviour
{

    [Tooltip("アイテムデータ")]
    [SerializeField]
    private ItemData _itemData;

    // 機体(プレイヤー)に当たった時
    private void OnCollisionEnter(Collision collision)
    {
        // 接触がプレイヤーの場合
        if (collision.gameObject.tag == "Player")
        {
            if (_itemData.BulletDelay > 0)
            {
                Player.Instance.UpdateBulletDelay(_itemData.BulletDelay);
            }

            if (_itemData.BulletAttack > 0)
            {
                Player.Instance.UpdateBulletAttack(_itemData.BulletAttack);
            }

            if (_itemData.BulletSeconds > 0)
            {
                Player.Instance.UpdateBulletSeconds(_itemData.BulletSeconds);
            }

            if (_itemData.HpUp > 0)
            {
                Player.Instance.UpdateMaxHelth(_itemData.HpUp);
            }

            if (_itemData.Speed > 0)
            {
                Player.Instance.UpdateSpeed(_itemData.Speed);
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 壁に当たった時
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}

