using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("AttackThrough")]
    [SerializeField] private float _durationAttackThrough;
    [SerializeField] private float _rangeAttackThrough;

    [Header("Attack")]
    [SerializeField] private float _durationAttack;
    [SerializeField] private float _rangeAttack;

    [Header("AttackDown")]
    [SerializeField] private float _durationAttackDown;
    [SerializeField] private float _rangeAttackDown;

    [Header("Other")]


    [SerializeField] private Transform _attackPoint;

    [SerializeField] private GhostSprites _ghostSprites;

    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private AnimationChange _animationChange;
    [SerializeField] private TextMeshProUGUI _textAttack;
    [SerializeField] private TextMeshProUGUI _textDie;

    [SerializeField] private float _staminaPerAttack;
    [SerializeField, Range(0f, 1f)] private float _prepareAttackTime;
    [SerializeField, Range(0f, 1f)] private float _endAttackTime;
    private List<Collider2D> _enemyCollider = new List<Collider2D>();
    private PlayerMovement _playerMovement;
    private Player _player;
    private Collider2D[] _hitEnemies;

    [HideInInspector]public float chanceOfCrit;

    [Header("ActivateInInventory")]
    public bool IsThroughAttackActivate;
    public bool IsThroughDownActivate;
    public bool IsAttackActivate;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.S) && IsThroughDownActivate && !_playerMovement.IsAttacking && !_playerMovement.OnGround)
        {
            AttackDown(Const.PlayerAnim.Player_Jump_Attack, _durationAttackDown, _rangeAttackDown);
            return;
        }

        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) &&
            _playerMovement.Rigidbody.velocity.magnitude > 0.01f &&!_playerMovement.IsAttacking && IsThroughAttackActivate)
        {
            ThroughAttack(Const.PlayerAnim.Player_Dash_Attack, _durationAttackThrough, _rangeAttackThrough);
            return;
        }

        if (Input.GetMouseButtonDown(0) && IsAttackActivate)
        {
            Attack(Const.PlayerAnim.Player_Attack, _durationAttack, _rangeAttack);
        }
    }

    private void ThroughAttack(string correctAnimationAttack, float endAttackTime, float rangeAttack)
    {
        if (_playerMovement.IsAttacking)
            return;

        if (_player.Stamina >= _staminaPerAttack)
        {

            _playerMovement.CorrectEffect.SetActive(true);
            _ghostSprites.trailSize = 15;

            _playerMovement.IsAttackingThrough = true;
            _playerMovement.IsAttacking = true;
            _player.MinusStamina(_staminaPerAttack, true);
            StartCoroutine(PrerareAttack(correctAnimationAttack, endAttackTime, rangeAttack));
        }
    }

    private void Attack(string correctAnimationAttack, float endAttackTime, float rangeAttack)
    {
        if (_playerMovement.IsAttacking)
            return;

        if (_player.Stamina >= _staminaPerAttack)
        {
            _ghostSprites.trailSize = 0;
            _playerMovement.IsAttacking = true;
            _player.MinusStamina(_staminaPerAttack, true);
            StartCoroutine(PrerareAttack(correctAnimationAttack, endAttackTime, rangeAttack));
        }

    }

    private void AttackDown(string correctAnimationAttack, float endAttackTime, float rangeAttack)
    {
        if (_playerMovement.IsAttacking)
            return;

        if (_player.Stamina >= _staminaPerAttack)
        {
            _ghostSprites.trailSize = 0;
            _playerMovement.IsAttackingDown = true;

            _playerMovement.IsAttacking = true;
            _player.MinusStamina(_staminaPerAttack, true);
            StartCoroutine(PrerareAttack(correctAnimationAttack, endAttackTime, rangeAttack));
        }
    }

    private IEnumerator PrerareAttack(string correctAnimationAttack, float endAttackTime, float rangeAttack)
    {
        SearchInRange(rangeAttack);
        TakeDamage();
        AnimationAttack(correctAnimationAttack);
        yield return new WaitForSeconds(endAttackTime);
        EndAttack();
    }

    private void SearchInRange(float rangeAttack)
    {
        Collider2D[] hitEnemies = _hitEnemies =Physics2D.OverlapCircleAll(_attackPoint.position, rangeAttack, _enemyLayers);
    }

    private void TakeDamage()
    {
        System.Random random = new System.Random();
        
        int damage = (int)(_player.Damage * _player.DamageRatio);
        if (random.Next(100) < (int)chanceOfCrit)
            damage *= 2;
        Debug.Log(damage);

        foreach (Collider2D enemy in _hitEnemies)
        {
            if (_playerMovement.IsAttackingThrough)
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), enemy);
            _textAttack.gameObject.SetActive(true);
            _textAttack.text = "-" + damage.ToString();
            enemy.GetComponent<Enemy>().GetDamage(damage);
            _enemyCollider.Add(enemy);
        }
    }

    private void EndAttack()
    {
        ActivateCollision();
        OffEffectTroughAttack();
        AttackOff();
        _player.MinusStamina(0, false);
        _textAttack.gameObject.SetActive(false);
        _ghostSprites.trailSize = 5;
    }

    private void ActivateCollision()
    {
        foreach (var enemy in _enemyCollider)
        {
            if (enemy != null)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), enemy, false);
            }
        }
        _enemyCollider.Clear();
    }

    private void OffEffectTroughAttack()
    {
        _playerMovement.EffectLeft.SetActive(false);
        _playerMovement.EffectRight.SetActive(false);
    }

    private void AttackOff()
    {
        _playerMovement.IsAttackingDown = false;
        _playerMovement.IsAttackingThrough = false;
        _playerMovement.IsAttacking = false;
    }

    private void AnimationAttack(string correctAttack)
    {
        if (_playerMovement.IsAttacking)
        {
            _animationChange.ChangeAnimationState(correctAttack);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawSphere(_attackPoint.position, 2.5f);
    }
}
