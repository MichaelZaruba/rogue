using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _rangePatrol;
    [SerializeField] private Gens _gen;
    [SerializeField] private TextMeshProUGUI _healthUI;
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _canvas;

    [SerializeField] private float _health = 100;
    [SerializeField] private int _amountGens;

    [SerializeField] protected LayerMask _playerLayer;

    [SerializeField] protected float _radiusOfVision;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _damage;


    [SerializeField] protected Rigidbody2D _rigidbody;

    protected Vector3 _startPosition;

    private Game _game;

    private List<Gens> _gens = new List<Gens>();

    protected float _startHealth;

    private Player _player;

    protected bool _moveRight;
    protected bool _movingRight;
    protected EnemyAI _enemyAI;

    public bool IsFindPlayer;


    public Transform PlayerPosition;

    public bool TestMode;

    public void Initialize(Game game, Player player)
    {
        _startPosition = transform.position;
        _startHealth = _health;
        _healthUI.text = _health.ToString();
        _game = game;
        _player = player;
        Debug.Log(player);
        gameObject.GetComponent<EnemyAI>().Initialize(player);
        _enemyAI = GetComponent<EnemyAI>();
    }

    protected virtual void CheckMoveRight()
    {
        if (transform.position.x > _startPosition.x + _rangePatrol)
        {
            _moveRight = false;
        }
        else if (transform.position.x < _startPosition.x - _rangePatrol)
        {
            _moveRight = true;
        }
    }

    protected void CheckMovingDirection()
    {
        if (_rigidbody.velocity.x > 0.01f && !_movingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _canvas.transform.transform.rotation = Quaternion.Euler(0, 0, 0);
            _movingRight = true;
            return;
        }
        if (_rigidbody.velocity.x < 0.01f && _movingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _canvas.transform.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        PlayerPosition = player.transform;
        return true;
    }

   public void GetDamage(int damage)
    {
        _health -= damage;
        _healthBar.fillAmount = _health / _startHealth;
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