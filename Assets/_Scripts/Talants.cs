using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talants : MonoBehaviour
{
    [SerializeField] private ItemTalant[] _talants;

    private Player _player;

    private Game _game;

    public void Initialize(Game game, Player player)
    {
        _player = player;
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
               var player = _player.GetComponent<PlayerMovement>();
                player.MaxJump++;
                break;
            case TypeTalant.MoreGens:
                break;
            case TypeTalant.DamageRatio:
                break;
            case TypeTalant.MoreRangeAttack:
                break;
            case TypeTalant.MoreExperience:
                break;
            case TypeTalant.DoubleDamage:
                break;
            case TypeTalant.FastStamina:
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
