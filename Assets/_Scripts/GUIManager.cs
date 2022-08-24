using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private PlayerLevel _playerLevel;

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

    [Header("Experience")]
   
    private int _experinece;
    private const string EXPERIENCE = "Exp";

    [Header("Experience")]
    [SerializeField]
    private TextMeshProUGUI _experienceUI;
    [SerializeField]
    private TextMeshProUGUI _allExperienceInfoUI;
    private int _allExperinece;
    private const string ALL_EXPERIENCE = "AllExp";

    public static GUIManager _instance;

    private void Awake()
    {
        _instance = this;

        _gens = PlayerPrefs.GetInt(GensSafe);
        _gensGold = PlayerPrefs.GetInt(GensGoldSafe);
        _experinece = PlayerPrefs.GetInt(EXPERIENCE);
        _allExperinece = PlayerPrefs.GetInt(ALL_EXPERIENCE);

        _gensUI.text = _gens.ToString();
        _gensGoldUI.text = _gensGold.ToString();
        _experienceUI.text = _allExperinece.ToString();
        _allExperienceInfoUI.text = _allExperinece.ToString();
    }

    public void ValueInit()
    {
        _gensUI.text = _gens.ToString();
        _gensGoldUI.text = _gensGold.ToString();
        _experienceUI.text = _experinece.ToString();
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
            PlayerPrefs.SetInt(GensGoldSafe, _gensGold);
            _gensGoldUI.text = _gensGold.ToString();
        }
    }

    public int LocalExperience
    {
        get
        {
            return _experinece;
        }

        set
        { 
            _experinece = value;
            PlayerPrefs.SetInt(EXPERIENCE, _experinece);
            _playerLevel.NextLevelPlayer();
        }
    }

    public int Experience
    {
        get
        {
            return _allExperinece;
        }

        set
        {
            _allExperinece = value;
            PlayerPrefs.SetInt(ALL_EXPERIENCE, _allExperinece);
            _experienceUI.text = _allExperinece.ToString();
            _allExperienceInfoUI.text = _allExperinece.ToString();
        }
    }
}
