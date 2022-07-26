using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;

    [SerializeField] private Transform _spawnPositionPlayer;
    [SerializeField] private Transform _spawnEnemyPosition;

    private List<Player> _players = new List<Player>();
    private List<Enemy> _enemys = new List<Enemy>();
    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Player player =  Instantiate(_player);
        player.transform.localPosition = _spawnPositionPlayer.position;
        _players.Add(player);
    }

    private void CreateEnemy()
    {
        //TODO ENEmy
    }

    private void CreateBuff()
    { 
        //TODO Buff
    }

}
