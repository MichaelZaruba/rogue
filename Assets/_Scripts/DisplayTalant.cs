using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTalant : MonoBehaviour
{
   [SerializeField] private DisplayTalantItem[] _itemTalant;



    public void Initialize(TypeTalant type, Image spriteRenderer)
    {
        foreach(var item in _itemTalant)
        {
            if (item.Activate)
                continue;
            Debug.Log(spriteRenderer);
            item.GetComponent<Image>().sprite = spriteRenderer.sprite;
            item.gameObject.SetActive(true);
            item.Activate = true;
            return;
        }
    }
}
