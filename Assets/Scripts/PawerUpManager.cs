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
    [Tooltip("�\������p���[�A�b�v�A�C�e����")]
    [SerializeField]
    private int _ItemCount;

    // �@�̑I���t���O�Ǘ��p
    private bool _unitflag;
    // �p���[�A�b�v�A�C�e��UI����
    private List<GameObject> _powerUpPanelChildren;

    protected override void Awake()
    {
        base.Awake();
        _unitflag = true;

        // �q�I�u�W�F�N�g���i�[
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
            // �p�l���\��
            _powerUpPanel.SetActive(true);

            // ���X�g���V���b�t��
            _powerUpPanelChildren = _powerUpPanelChildren.OrderBy(a => Guid.NewGuid()).ToList();

            // �V���b�t���������X�g�̐擪��������\��
            for (int i = 0; i < _ItemCount; i++)
            {
                _powerUpPanelChildren[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// �p���[�A�b�v�p�l���̏�����
    /// </summary>
    public void PanelInitilizeI()
    {
        for (int i = 0; i < _powerUpPanelChildren.Count; i++)
        {
            _powerUpPanelChildren[i].SetActive(false);
        }
    }
}
