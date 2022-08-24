using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    [Header("CharacteristiUI")]
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _stamina;

    [Header("MaxLevelUpgrade")]
    [SerializeField] private int _maxLevelDamage = 10;
    [SerializeField] private int _maxLevelHealth = 10;
    [SerializeField] private int _maxLevelStamina = 10;


    [Header("PriceUpgradeLevel")]
    [SerializeField] private int _priceDamage;
    [SerializeField] private int _priceHealth;
    [SerializeField] private int _priceStamina;

    [Header("PriceUpgradeLevelUI")]
    [SerializeField] private TextMeshProUGUI _priceDamageUI;
    [SerializeField] private TextMeshProUGUI _priceHealthUI;
    [SerializeField] private TextMeshProUGUI _priceStaminaUI;

    [Header("AmountUpgrade")]
    [SerializeField] private int _appDamage;
    [SerializeField] private int _appHealth;
    [SerializeField] private int _appStamina;

    [Header("levelNowUI")]
    [SerializeField] private TextMeshProUGUI _levelDamageUI;
    [SerializeField] private TextMeshProUGUI _levelHealthUI;
    [SerializeField] private TextMeshProUGUI _levelStaminaUI;

    [Header("SaveLevel")]
    private const string LEVEL_DAMAGE = "levelDamage";
    private const string LEVEL_HEALTH = "LevelHealth";
    private const string LEVEL_STAMINA = "LevelStamina";

    [Header("levelNow")]
    private int _levelDamage = 0;
    private int _levelHealth = 0;
    private int _levelStamina = 0;


    [Header("Other")]
    private Player _player;

    public void Initialize(Player player)
    {

        int levelStamina = PlayerPrefs.GetInt(LEVEL_STAMINA);
        int levelHealth = PlayerPrefs.GetInt(LEVEL_HEALTH);
        int levelDamage = PlayerPrefs.GetInt(LEVEL_DAMAGE);

        if (levelStamina != 0)
            _levelStamina = levelStamina;
        if (levelHealth != 0)
            _levelHealth = levelHealth;
        if (levelDamage != 0)
           _levelDamage = levelDamage;


        _player =  player;
        _health.text = _player.Health.ToString();
        _damage.text = _player.Damage.ToString();
        _stamina.text = _player.Stamina.ToString();

        _priceDamageUI.text = _priceDamage.ToString();
        _priceHealthUI.text = _priceHealth.ToString();
        _priceStaminaUI.text = _priceStamina.ToString();

        _levelDamageUI.text = "Level " + (_levelDamage+1).ToString();
        _levelHealthUI.text = "Level " + (_levelHealth + 1).ToString();
        _levelStaminaUI.text = "Level " + (_levelStamina + 1).ToString();
    }

    public void NextLevelDamage()
    {
        if (_levelDamage < _maxLevelDamage && GUIManager._instance.Gens >= _priceDamage)
        {

            GUIManager._instance.Gens -= _priceDamage;
            GUIManager._instance.ValueInit();
            _levelDamage++;
            _player.Damage += _appDamage;
            PlayerPrefs.SetInt(LEVEL_DAMAGE, _levelDamage);
            PlayerPrefs.SetInt(Player.DAMAGE, _player.Damage);
            _damage.text = _player.Damage.ToString();
            _levelDamageUI.text = "Level " + (_levelDamage+1).ToString();
        }
    }

    public void NextLevelHealth()
    {
        if (_levelHealth < _maxLevelHealth && GUIManager._instance.Gens >= _priceHealth)
        {
            GUIManager._instance.Gens -= _priceHealth;
            GUIManager._instance.ValueInit();
            _levelHealth++;
            _player.Health += _appHealth;
            PlayerPrefs.SetInt(LEVEL_HEALTH, _levelHealth);
            PlayerPrefs.SetInt(Player.HEALTH, (int)_player.Health);
            _health.text = _player.Health.ToString();
            _levelHealthUI.text = "Level " + (_levelHealth+1).ToString();
        }
    }

    public void NextLevelStamina()
    {
        if (_levelStamina < _maxLevelStamina && GUIManager._instance.Gens >= _priceStamina)
        {
            GUIManager._instance.Gens -= _priceStamina;
            GUIManager._instance.ValueInit();
            _levelStamina++;
            _player.Stamina += _appStamina;
            PlayerPrefs.SetInt(LEVEL_STAMINA, _levelStamina);
            PlayerPrefs.SetInt(Player.STAMINA, (int)_player.Stamina);
            var stamina = (int)_player.Stamina;
            _stamina.text = stamina.ToString();
            _levelStaminaUI.text = "Level " + (_levelStamina+1).ToString();
        }
    }

    public void InfoUIButton()
    {
        if (gameObject.activeSelf)
        {
            Game._instance.IsPause = false;
            gameObject.SetActive(false);
        }
        else
        {
            Game._instance.IsPause = true;
            gameObject.SetActive(true);
        }
    }
}
