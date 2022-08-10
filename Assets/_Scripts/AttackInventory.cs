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
}
