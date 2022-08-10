using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _stamina;

    private Player _player;

    public void Initialize(Player player)
    {
        _player =  player;
        _health.text = _player.Health.ToString();
        _damage.text = _player.Damage.ToString();
        _stamina.text = _player.Stamina.ToString();
    }
}
