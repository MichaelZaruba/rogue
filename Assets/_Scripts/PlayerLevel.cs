using TMPro;
using UnityEngine;
using UnityEngine.UI;

public  class PlayerLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelUI;
    [SerializeField] private TextMeshProUGUI _ExperienceUI;
    [SerializeField] private TextMeshProUGUI _expNextLevelUI;
    [SerializeField] private Image _image;
    [SerializeField] private float _nextLevelExp;

    public const string LEVEL_PLAYER = "LevelPlayer";
    public int LevelValue;

    private void Start()
    {
        LevelValue = PlayerPrefs.GetInt(LEVEL_PLAYER);
        for( int i = 0; i < LevelValue; i++ )
        {
            if (LevelValue == 0)
                break;
            _nextLevelExp *= 1.5f;
        }
        NextLevelPlayer();    
    }

    public void NextLevelPlayer()
    {
        var experience = GUIManager._instance.LocalExperience;
        _image.fillAmount = experience / _nextLevelExp;

        _ExperienceUI.text = experience.ToString() + " /";
        _expNextLevelUI.text = _nextLevelExp.ToString();
        _levelUI.text = (LevelValue + 1).ToString();

        if (GUIManager._instance.LocalExperience > _nextLevelExp)
        {
            GUIManager._instance.LocalExperience = 0;
            _nextLevelExp *= 1.5f;
            LevelValue++;

            PlayerPrefs.SetInt(LEVEL_PLAYER, LevelValue);
            _expNextLevelUI.text = _nextLevelExp.ToString();
            _levelUI.text = (LevelValue+1).ToString();
        }
    }

}