using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackInventory : MonoBehaviour
{
   [SerializeField] private ItemAttack[] _itemsAttack;

    public void Initialize(PlayerAttack game)
    {
        _itemsAttack = GetComponentsInChildren<ItemAttack>();
        foreach(var item in _itemsAttack)
        {
            item.Initialize(game);
        }
    }

    public void ActivateItem(AttackType type, bool isInitialize)
    {
        foreach(var item in _itemsAttack)
        {
            if(type == item.Type)
            {
                item.ActivateItemAttack(isInitialize);
            }
        }
    }

    public void AttacksUIButton()
    {
        if (gameObject.activeSelf)
        {
            Game._instance.IsPause = false;
            gameObject.SetActive(false);
        }
        else
        {
            Game._instance.IsPause = true;
            gameObject.SetActive(true);
        }
    }
}
