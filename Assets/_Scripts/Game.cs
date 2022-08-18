using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Talants _talants;

    [SerializeField] private Info _info;

    [SerializeField] private AttackInventory _attackInventory;

    [SerializeField] private Player _player;

    [SerializeField] private LookAtTargetCamera _lookAtTargetCamera;

    [SerializeField] private Image _staminaImage;
    [SerializeField] private Image _healthImage;

    [SerializeField] private Gens _gensPrefab;

    [SerializeField] 
    private List<Level> _levelsSafe;

    [SerializeField, Range(1,10)] private int _numberLevel;

    [SerializeField] private EnemyFactory _enemyFactory;
 
    private SpawnPlayer _spawnPlayer;

    private List<Level> _levels = new List<Level>();   

    private List<SpawnEnemy> _spawnEnemies = new List<SpawnEnemy>();

    private List<Player> _players = new List<Player>();

    private List<Enemy> _enemys = new List<Enemy>();

    private void Start()
    {  
        StartNewGame();
        _attackInventory.Initialize(_players[0].GetComponent<PlayerAttack>());
        _info.Initialize(_players[0].GetComponent<Player>());
        _talants.Initialize(this, _players[0].GetComponent<Player>());
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

    public void RestartGameAfterDiePlayer()
    {
        _numberLevel = 1;
        PlayerPrefs.DeleteAll();
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
        player.GetComponent<Player>().Initialize(_staminaImage, _healthImage, this);
        _players.Add(player);
    }

    private void CameraTarget(Player player)
    {
        _lookAtTargetCamera.Initialize(player);
    }

    private void CreateEnemy()
    {
        Enemy enemy;
        foreach (var position in _spawnEnemies)
        {
            enemy = _enemyFactory.Get(position.EnemyType);
            enemy.gameObject.transform.position = position.transform.position;
            enemy.Initialize(this, _players[0], _gensPrefab);
            _enemys.Add(enemy);
        } 
    }

    public void ReclaimEnemy(Enemy enemy)
    {
        _enemys.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void ReclaimPlayer(Player player)
    {
        _players.Remove(player);
        Destroy(player.gameObject);
        //TODO Lose
    }

    private void CreateBuff()
    { 
        //TODO Buff
    }

}
