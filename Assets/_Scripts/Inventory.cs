using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private AttackInventory _attacks;
    [SerializeField] private Talants _talants;

    public void Next()
    {
        _attacks.gameObject.SetActive(false);
        _talants.gameObject.SetActive(true);   
    }

    public void Back()
    {
        _attacks.gameObject.SetActive(true);
        _talants.gameObject.SetActive(false);
    }
}
