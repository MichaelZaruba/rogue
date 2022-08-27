using Pathfinding;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyAI))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(EnemyAttackMelee))]
[RequireComponent(typeof(EnemyAnimationChanger))]
[RequireComponent(typeof(EnemyAttackRange))]
[RequireComponent(typeof(AnimationChange))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField, Range(20f, 1000f)] private float _health = 100;
    [SerializeField, Range(3f, 10f)] private float _rangePatrol;

    [SerializeField, Range(1f, 5f)] protected float _radiusOfVision;
    [SerializeField, Range(1f, 5f)] protected float _speed;
    [SerializeField, Range(5f, 15f)] private int _damage;

    [SerializeField] protected LayerMask _playerLayer;
    [SerializeField] protected EnemyAttackType _type;

    private EnemyAttackRange _enemyAttackRange;
    private EnemyAttackMelee _enemyAttackMelee;
    private EnemyAnimationChanger _animationChanger;
    private GamePrefab _gamePrefab;
    private Bullet _bullet;
    private Gens _gen;
    private Canvas _canvas;
    private Image[] _healthBar;
    private TextMeshProUGUI _healthUI;
    private Game _game;

    private List<Gens> _gens = new List<Gens>();

    private AnimationChange _animationChange;
    private Player _player;
    private AttackPoint _attackPoint;
    private GameSettings _gameSettings;

    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    protected EnemyAI _enemyAI;

    protected Vector3 _spawnPosition;

    protected float _startHealth;

    protected bool _moveRight;
    protected bool _movingRight;

    public bool IsFindPlayer;

    protected bool _justShot;
    public bool TestMode;

    public float Health { get => _health; set => _health = value; }
    public int Damage {get => _damage; set => _damage = value; }

    public EnemyAttackType Type => _type;

    public bool JustShot { get => _justShot; set => _justShot = value; }

    public AttackPoint AttackPoint => _attackPoint;

    public void Initialize(Game game, Player player, GamePrefab gamePrefab, GameSettings gameSettings)
    {
        InitializeComponent();
        InitializeComponentInChildren();
        InitializeEnemyAttack();
        InitializeEnemyAnimationChanger();
        InitializeSettings(game, player, gamePrefab, gameSettings);
        InitializeEnemyAI();
    }

    private void InitializeEnemyAnimationChanger()
    {
        _animationChange.Animator = GetComponent<Animator>();
        _animationChanger.Initialize(_rigidbody, _enemyAttackMelee, _animationChange);
    }

    private void InitializeSettings(Game game, Player player, GamePrefab gamePrefab, GameSettings gameSettings)
    {
        _gameSettings = gameSettings;
        _gamePrefab = gamePrefab;
        _gen = _gamePrefab.GensPrefab;
        _bullet = _gamePrefab.BulletPrefab;
        _spawnPosition = transform.position;
        _startHealth = _health;
        _healthUI.text = _health.ToString();
        _game = game;
        _player = player;
    }

    private void InitializeComponent()
    {
        _enemyAttackRange = GetComponent<EnemyAttackRange>();
        _animationChange = GetComponent<AnimationChange>();
        _enemyAttackMelee = GetComponent<EnemyAttackMelee>();
        _animationChanger = GetComponent<EnemyAnimationChanger>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void InitializeComponentInChildren()
    {
        _attackPoint = GetComponentInChildren<AttackPoint>();
        _canvas = GetComponentInChildren<Canvas>();
        _healthUI = GetComponentInChildren<TextMeshProUGUI>();
        _healthBar = GetComponentsInChildren<Image>();
    }

    private void InitializeEnemyAttack()
    {
        _enemyAttackRange.Initialize(_player, _bullet, this, _damage);
        _enemyAttackMelee.Initialize(this, _type, _playerLayer);
    }

    private void InitializeEnemyAI()
    {
        gameObject.GetComponent<EnemyAI>().Initialize(_player);
    }

    protected virtual void CheckMoveRight()
    {
        if (transform.position.x > _spawnPosition.x + _rangePatrol)
        {
            _moveRight = false;
        }
        else if (transform.position.x < _spawnPosition.x - _rangePatrol)
        {
            _moveRight = true;
        }
    }

    protected virtual void CheckMovingDirection()
    {
        if (_rigidbody.velocity.x > 0.01f && !_movingRight)
        {

            transform.rotation = Quaternion.Euler(0, 180, 0);

            _canvas.transform.rotation = Quaternion.Euler(0, 0, 0);
            _movingRight = true;
            return;
        }
        if (_rigidbody.velocity.x < -0.01f && _movingRight)
        {

            transform.rotation = Quaternion.Euler(0, 0, 0);
            _canvas.transform.rotation = Quaternion.Euler(0, 0, 0);
            _movingRight = false;

        }
    }

    protected virtual void Patrol()
    {
        if (IsFindPlayer)
            return;

        if (_moveRight)
        {
            _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
        }
        if (!_moveRight)
        {
            _rigidbody.velocity = new Vector2(-_speed, _rigidbody.velocity.y);
        }

    }

    protected virtual bool CheckPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, _radiusOfVision, _playerLayer);

        if (player == null)
        {
            IsFindPlayer = false;
            return false;
        }
        IsFindPlayer = true;
        _enemyAI.IsTargetActive = true;
        _enemyAttackRange.Initialize(_player, _bullet, this, _damage);
        return true;
    }

    public void GetDamage(int damage)
    {
        _health -= damage;
        _healthBar[1].fillAmount = _health / _startHealth;
        _healthUI.text = _health.ToString();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        int countGens = (int)(Random.Range(3, 10) * _gameSettings.MoreGens);
        for (int i = 0; i < countGens; i++)
        {
            Gens gen = Instantiate(_gen);
            _gens.Add(gen);
            gen.transform.position = new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f), transform.position.z);
        }
        _game.ReclaimEnemy(this);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, _radiusOfVision);
    }
}