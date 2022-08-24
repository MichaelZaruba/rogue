using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talants : MonoBehaviour
{
    [SerializeField] private ItemTalant[] _talants;

    private DisplayTalant _displayTalant;
    private Enemy _enemy;
    private Player _player;
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private GameSettings _gameSettings;

    private Game _game;
    private float plusChanceOfCrit = 0.1f;

    public void Initialize(DisplayTalant displayTalant,Game game, Player player,GameSettings gameSettings)
    {
        _displayTalant = displayTalant;
        _gameSettings = gameSettings;
        _player = player;
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerAttack = _player.GetComponent<PlayerAttack>();
        _game = game;
        _talants = GetComponentsInChildren<ItemTalant>();
        foreach(var talant in _talants)
        {
            talant.Initialize(_game, this, displayTalant);
        }
    }

    public void ActivateTalant(TypeTalant type)
    {
        switch (type)
        {
            case TypeTalant.DoubleJump:
                _playerMovement.MaxJump++;
                break;
            case TypeTalant.MoreGens:
                _gameSettings.MoreGens++;
                break;
            case TypeTalant.DamageRatio:
                _player.DamageRatio = 1.25f;
                break;
            case TypeTalant.MoreRangeAttack:
                _player.RangeAttack += 0.12f;
                break;
            case TypeTalant.MoreExperience:
                break;
            case TypeTalant.DoubleDamage:
                _playerAttack.chanceOfCrit = _playerAttack.chanceOfCrit + 100 * plusChanceOfCrit;
                plusChanceOfCrit *= 0.9f;
                break;
            case TypeTalant.FastStamina:
                _player._speedFillingStamina += 0.05f;
                break;
            case TypeTalant.Bleeding:
                break;
            case TypeTalant.Vampirism:
                break;
            case TypeTalant.SecondChance:
                break;
            case TypeTalant.Shield:
                break;
            case TypeTalant.MoreLight:
                break;
            case TypeTalant.LightingEnemy:
                break;
        }
    }

    public void TalantsUIButton()
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
