using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{

    [Header("default Gens")]
    [SerializeField]
    private TextMeshProUGUI _gensUI;
    private int _gens;
    public const string GensSafe = "Gens";

    [Header("Gold Gens")]
    [SerializeField] 
    private TextMeshProUGUI _gensGoldUI;
    private int _gensGold;
    private const string GensGoldSafe = "GensGold";

    [Header("Purple  Gens")]
    [SerializeField] 
    private TextMeshProUGUI _gensPurpleUI;
    private int _gensPurple;
    private const string GensPurpleSafe = "GensPurple";

    public static GUIManager _instance;

    private void Awake()
    {
        _instance = this;

        _gens = PlayerPrefs.GetInt(GensSafe);
        _gensGold = PlayerPrefs.GetInt(GensGoldSafe);
        _gensPurple = PlayerPrefs.GetInt(GensPurpleSafe);

        _gensUI.text = _gens.ToString();
        _gensGoldUI.text = _gensGold.ToString();
        _gensPurpleUI.text = _gensPurple.ToString();
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
            _gensUI.text = _gens.ToString();
        }
    }

    public int GensGold
    {
        get
        {
            return _gensGold;
        }

        set
        {
            _gensGold = value;
            PlayerPrefs.SetInt(GensGoldSafe, _gens);
            _gensGoldUI.text = _gensGold.ToString();
        }
    }

    public int GensPurple
    {
        get
        {
            return _gensPurple;
        }

        set
        {
            _gensPurple = value;
            PlayerPrefs.SetInt(GensPurpleSafe, _gens);
            _gensPurpleUI.text = _gensPurple.ToString();
        }
    }
}
