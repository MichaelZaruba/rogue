﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public  class PlayerLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelUI;
    [SerializeField] private TextMeshProUGUI _ExperienceUI;
    [SerializeField] private TextMeshProUGUI _expNextLevelUI;
    [SerializeField] private int[] _goldGensPerLevel;
    [SerializeField] private int[] _experiencePerLevel;
    [SerializeField] private Image _image;
    [SerializeField] private int _nextLevelExp;

    public const string LEVEL_PLAYER = "LevelPlayer";
    public int LevelValue;

    private void Start()
    {
        LevelValue = PlayerPrefs.GetInt(LEVEL_PLAYER);    
        _nextLevelExp = _experiencePerLevel[LevelValue];
        NextLevelPlayer();
    }

    public void NextLevelPlayer()
    {
        var experience = GUIManager._instance.LocalExperience;
        Debug.Log(experience);
        _image.fillAmount = experience / (float)_nextLevelExp;

        _ExperienceUI.text = experience.ToString() + " /";
        _expNextLevelUI.text = _nextLevelExp.ToString();
        _levelUI.text = (LevelValue + 1).ToString();

        if (GUIManager._instance.LocalExperience > _nextLevelExp)
        {
           
            GUIManager._instance.LocalExperience = 0;
            LevelValue++;
            _nextLevelExp = _experiencePerLevel[LevelValue];
            GUIManager._instance.GensGold += _goldGensPerLevel[LevelValue];
            Debug.Log("tut");
            PlayerPrefs.SetInt(LEVEL_PLAYER, LevelValue);
            _expNextLevelUI.text = _nextLevelExp.ToString();
            _levelUI.text = (LevelValue + 1).ToString();
        }
    }

}