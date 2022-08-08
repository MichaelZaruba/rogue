using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gensText;

    private int _gens;
    public const string Gens = "Gens";
    public static GUIManager _instance;

    public int Star
    {
        get
        {
            return _gens;
        }

        set
        {
            _gens = value;
            PlayerPrefs.SetInt(Gens, _gens);
            _gensText.text = _gens.ToString();
        }
    }
}
