using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawerUpSelect : MonoBehaviour
{
    [Tooltip("アイテムデータ")]
    [SerializeField]
    private ItemData _itemData;

    public void PowerUp()
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

    public void AfterInitilize()
    {
        gameObject.SetActive(false);
    }
}
