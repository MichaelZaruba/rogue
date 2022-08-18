using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talants : MonoBehaviour
{
    [SerializeField] private ItemTalant[] _talants;

    private Player _player;
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;

    private Game _game;
    private float plusChanceOfCrit = 0.1f;

    public void Initialize(Game game, Player player)
    {
        _player = player;
        _playerMovement = _player.GetComponent<PlayerMovement>();
        _playerAttack = _player.GetComponent<PlayerAttack>();
        _game = game;
        _talants = GetComponentsInChildren<ItemTalant>();
        foreach(var talant in _talants)
        {
            talant.Initialize(_game, this);
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
                break;
            case TypeTalant.DamageRatio:
                break;
            case TypeTalant.MoreRangeAttack:
                _player.RangeAttack += 0.12f;
                break;
            case TypeTalant.MoreExperience:
                break;
            case TypeTalant.DoubleDamage:
                _playerAttack.chanceOfCrit = _playerAttack.chanceOfCrit + plusChanceOfCrit;
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
}
