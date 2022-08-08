using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gensText;

    private int _gens;
    public const string GensSafe = "Gens";
    public static GUIManager _instance;

    private void Awake()
    {
        _instance = this;
       _gens =  PlayerPrefs.GetInt(GensSafe);
        _gensText.text = _gens.ToString();
    }

    public int Gens
    {
        get
        {
            return _gens;
        }

        set
        {
            _gens = value;
            PlayerPrefs.SetInt(GensSafe, _gens);
            _gensText.text = _gens.ToString();
        }
    }
}
