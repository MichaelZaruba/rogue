﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemAttack : MonoBehaviour
{
    
    [SerializeField] private GameObject[] _offWhenActivate;
    [SerializeField] private RawImage _imageInfo;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private GameObject _newAttackInfo;

    private PlayerAttack _playerAttack;

    public int countPurpleGens;
    public AttackType Type;

    public void Initialize(PlayerAttack playerAttack)
    {
        _playerAttack = playerAttack;
        _textPrice.text = countPurpleGens.ToString();
    }

    public void ActivateItemAttack(bool isInitialize)
    {
        gameObject.SetActive(true);
        for(int i = 0; i < _offWhenActivate.Length; i++)
        {
            if (!isInitialize)
            {
                _newAttackInfo.SetActive(true);
                ShowInfo();
            }
                _offWhenActivate[i].SetActive(false);
               
        }
        ActivateAttack(Type);
    }

    public void ShowInfo()
    {
        _imageInfo.gameObject.SetActive(true);
    }

    private void ActivateAttack(AttackType type)
    {
       switch (type)
        {
            case AttackType.Default:
               _playerAttack.IsAttackActivate = true;
                break;
            case AttackType.Down:
                _playerAttack.IsThroughDownActivate = true;
                break;
            case AttackType.Through:
                _playerAttack.IsThroughAttackActivate = true;
                break;
        }
    }
}