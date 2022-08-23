using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private GamePrefab _gamePrefab;

    [SerializeField] private DisplayTalant _displayTalant;
    [SerializeField] private Talants _talants;

    [SerializeField] private Info _info;

    [SerializeField] private AttackInventory _attackInventory;

    [SerializeField] private LookAtTargetCamera _lookAtTargetCamera;

    [SerializeField] private Image _staminaImage;
    [SerializeField] private Image _healthImage;

    [SerializeField, Range(1,10)] private int _numberLevel;

    [SerializeField] private EnemyFactory _enemyFactory;
 
    private SpawnPlayer _spawnPlayer;

    private List<Level> _levels = new List<Level>();   

    private List<SpawnEnemy> _spawnEnemies = new List<SpawnEnemy>();

    private List<Player> _players = new List<Player>();

    private List<Enemy> _enemys = new List<Enemy>();

    private void Start()
    {  
        if (PlayerPrefs.GetInt(Menu.SAFE_LEVEL ) != 0)
        {
            _numberLevel = PlayerPrefs.GetInt(Menu.SAFE_LEVEL);
        }

        StartNewGame();
    }

    private void StartNewGame()
    {
        CreateLevel();
        CreatePlayer();
        CreateEnemy();
        _attackInventory.Initialize(_players[0].GetComponent<PlayerAttack>());
        _info.Initialize(_players[0].GetComponent<Player>());
        _talants.Initialize(_displayTalant, this, _players[0].GetComponent<Player>(), _gameSettings);
        InitializeAttack();
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
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

    private void InitializeAttack()
    {
        var through = PlayerPrefs.GetInt(PlayerAttack.THROUGH_ATTACK);
        if (through != 0)
        {
            _attackInventory.ActivateItem(AttackType.Through, true);
            _players[0].GetComponent<PlayerAttack>().IsThroughAttackActivate = true;
        }

        var down = PlayerPrefs.GetInt(PlayerAttack.DOWN_ATTACK);
        if (through != 0)
        {
            _attackInventory.ActivateItem(AttackType.Down, true);
            _players[0].GetComponent<PlayerAttack>().IsThroughDownActivate = true;
        }
    }

    private void CreateLevel()
    {
        try
        {
            Level level = Instantiate(_gamePrefab.LevelsSafe[_numberLevel - 1]);
            level.Initialize();
            PlayerPrefs.SetInt(Menu.SAFE_LEVEL, _numberLevel);
            _spawnEnemies = level.SpawnsEnemies;
            _spawnPlayer = level.SpawnPlayer;
            _levels.Add(level);
        }
        catch { Debug.Log("Need add level!"); }
    }

    private void CreatePlayer()
    {
        Player player =  Instantiate(_gamePrefab.PlayerPrefab);
        CameraTarget(player);
        player.transform.localPosition = _spawnPlayer.transform.position;
        player.gameObject.GetComponent<TargetFinish>().Initialize(this);
        player.GetComponent<Player>().Initialize(_staminaImage, _healthImage, this, _attackInventory);
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
            enemy.Initialize(this, _players[0], _gamePrefab, _gameSettings);
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
