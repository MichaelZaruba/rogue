using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private LookAtTargetCamera _lookAtTargetCamera;

     
    [SerializeField] private List<Level> _levelsSafe;
    [SerializeField, Range(1,10)] private int _numberLevel;

    private Finish _finish;
    private SpawnPlayer _spawnPlayer;

    private List<Level> _levels = new List<Level>();   
    private List<SpawnEnemy> _spawnEnemies = new List<SpawnEnemy>();

    private List<Player> _players = new List<Player>();
    private List<Enemy> _enemys = new List<Enemy>();

    private void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        CreateLevel();
        CreatePlayer();
        CreateEnemy();
    }

    public void NextLevel()
    {
        _numberLevel++;
        EndGame();
    }

    private void EndGame()
    {
        foreach(var level in _levels)
        {
            Destroy(level.gameObject);
        }    
        foreach(var player in _players)
        {
            Destroy(player.gameObject);
        }
        foreach(var enemy in _enemys)
        {
            Destroy(enemy.gameObject);
        }
        _levels.Clear();
        _players.Clear();
        _enemys.Clear();

        StartNewGame();
    }

    private void CreateLevel()
    {
        try
        {
            Level level = Instantiate(_levelsSafe[_numberLevel - 1]);
            level.Initialize();
            _spawnEnemies = level.SpawnsEnemies;
            _spawnPlayer = level.SpawnPlayer;
            _finish = level.Finish;
            _levels.Add(level);
        }
        catch { Debug.Log("Need add level!"); }
    }

    private void CreatePlayer()
    {
        Player player =  Instantiate(_player);
        CameraTarget(player);
        player.transform.localPosition = _spawnPlayer.transform.position;
        player.gameObject.GetComponent<TargetFinish>().Initialize(this);    
        _players.Add(player);
    }

    private void CameraTarget(Player player)
    {
        _lookAtTargetCamera.Initialize(player);
    }

    private void CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemy);
        _enemys.Add(enemy);
        enemy.gameObject.transform.position = _spawnEnemies[0].transform.position;
    }

    private void CreateBuff()
    { 
        //TODO Buff
    }

}
