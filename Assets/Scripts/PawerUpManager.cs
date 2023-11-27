using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PawerUpManager : SingletonMonoBehaviour<PawerUpManager>
{
    [SerializeField]
    private GameObject _unitPanel;
    [SerializeField]
    private GameObject _powerUpPanel;
    [Tooltip("表示するパワーアップアイテム個数")]
    [SerializeField]
    private int _ItemCount;

    // 機体選択フラグ管理用
    private bool _unitflag;
    // パワーアップアイテムUI総数
    private List<GameObject> _powerUpPanelChildren;

    protected override void Awake()
    {
        base.Awake();
        _unitflag = true;

        // 子オブジェクトを格納
        for (int i = 0; i < _powerUpPanel.transform.childCount; i++)
        {
            Transform childTransform = _powerUpPanel.transform.GetChild(i);
            _powerUpPanelChildren.Add(childTransform.gameObject);
        }
    }

    public void SelectPanelActive()
    {
        if (_unitflag)
        {
            _unitPanel.SetActive(true);
            _unitflag = false;
        }
        else
        {
            // パネル表示
            _powerUpPanel.SetActive(true);

            // リストをシャッフル
            _powerUpPanelChildren = _powerUpPanelChildren.OrderBy(a => Guid.NewGuid()).ToList();

            // シャッフルしたリストの先頭から個数分表示
            for (int i = 0; i < _ItemCount; i++)
            {
                _powerUpPanelChildren[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// パワーアップパネルの初期化
    /// </summary>
    public void PanelInitilizeI()
    {
        for (int i = 0; i < _powerUpPanelChildren.Count; i++)
        {
            _powerUpPanelChildren[i].SetActive(false);
        }
    }
}
