using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu: MonoBehaviour 
{
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _continue;

    private int _continueLevel;

    public const string SAFE_LEVEL = "SafeLevel";

    private void Awake()
    {
        _continueLevel = PlayerPrefs.GetInt(SAFE_LEVEL);
        if (_continueLevel <= 1)
        {
            _continue.SetActive(false);
        }
        else
        {
            _continue.SetActive(true);  
        }

    }

    public void StartNewGame()
    {
        PlayerPrefs.SetInt(SAFE_LEVEL, 0);
        SceneManager.LoadScene("Game");
    } 

    public void ContinueGame()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void OpenSettings()
    {
        _settings.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    } 
}