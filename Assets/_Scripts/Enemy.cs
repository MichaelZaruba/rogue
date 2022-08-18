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
[RequireComponent (typeof(EnemyAttack))]
[RequireComponent(typeof(EnemyAnimationChanger))]
[RequireComponent(typeof(AnimationChange))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField, Range(20f, 1000f)] private float _health = 100;
    [SerializeField, Range(3f, 10f)] private float _rangePatrol;

    [SerializeField,Range(1f,5f)] protected float _radiusOfVision;
    [SerializeField,Range(1f,5f)] protected float _speed;
    [SerializeField, Range(5f, 15f)] private int _damage;

    [SerializeField] protected LayerMask _playerLayer;
    [SerializeField] protected EnemyAttackType _type;

    private EnemyAttack _enemyAttack;
    private EnemyAnimationChanger _animationChanger;
    private Gens _gen;
    private Canvas _canvas;
    private Image[] _healthBar;
    private TextMeshProUGUI _healthUI;
    private Game _game;

    private List<Gens> _gens = new List<Gens>();

    private AnimationChange _animationChange;
    private Player _player;
    private AttackPoint _attackPoint;

    protected Rigidbody2D _rigidbody;
    protected Animator _animator;
    protected EnemyAI _enemyAI;

    protected Vector3 _spawnPosition;

    protected float _startHealth;

    protected bool _moveRight;
    protected bool _movingRight;

    public bool IsFindPlayer;


    public bool TestMode;

    public int Damage => _damage;

    public AttackPoint AttackPoint => _attackPoint;

    public void Initialize(Game game, Player player, Gens gen)
    {
        InitializeComponent();
        InitializeEnemyAttack();
        InitializeEnemyAnimationChanger();
        _gen = gen;
        _spawnPosition = transform.position;
        _startHealth = _health;
        _healthUI.text = _health.ToString();
        _game = game;
        _player = player;
        InitializeEnemyAI();
    }
    
    private void InitializeEnemyAnimationChanger()
    {
        _animationChange.Animator = GetComponent<Animator>();
        _animationChanger.Initialize(_rigidbody,_enemyAttack, _animationChange);
    }

    private void InitializeComponent()
    {
        _animationChange = GetComponent<AnimationChange>();
       _enemyAttack = GetComponent<EnemyAttack>();
        _animationChanger = GetComponent<EnemyAnimationChanger>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _attackPoint = GetComponentInChildren<AttackPoint>();
        _animator = GetComponent<Animator>();
       _canvas = GetComponentInChildren<Canvas>();
        _healthBar = GetComponentsInChildren<Image>();
        _healthUI = GetComponentInChildren<TextMeshProUGUI>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void InitializeEnemyAttack()
    {
        _enemyAttack.Initialize(this, _type, _playerLayer);
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

    protected void CheckMovingDirection()
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
        gameObject.GetComponent<EnemyAI>().IsTargetActive = true;
        if (gameObject.GetComponent<BokalAttack>() != null)
            gameObject.GetComponent<BokalAttack>().Initialize(player.GetComponent<Player>());
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
           // collision.gameObject.GetComponent<Player>().GetDamage(_damage);

        }
    }

    private void Die()
    {
        int countGens = Random.Range(3, 10);
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