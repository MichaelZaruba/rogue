using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperAttack : EnemyAttackMelee
{
    [SerializeField] private float _radiusOfAttack;
    [SerializeField] private float _bonusJumpYSpeed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private Collider2D _thisCollider;
    private Collider2D _player;
    private Vector3 _playerLastPosition;
    private Chomper _chomper;
    private bool _alreadyDamaged;
    private bool _readyToDamage;
    private bool _canStopAttacking;

    public override void Initialize(Enemy chomper, EnemyAttackType type, LayerMask player)
    {
        _playerLayerMask = player;
        _chomper = (Chomper)chomper;
        _type = type;
    }

    private void Update()
    {
        AttackMelee();
    }

    public override void AttackMelee()
    {
        if (PrepareAttack || IsAttacking)
            return;

        Collider2D player = Physics2D.OverlapCircle(transform.position, _radiusOfAttack, _playerLayerMask);
        if (player == null)
            return;
        _player = player;
        _playerLastPosition = _player.transform.position;
        _chomper.ChangeSpeed(new Vector2(0, 0));
        IsAttacking = true;
    }

    protected override void Attack()
    {
        Vector2 direction = (_playerLastPosition - transform.position).normalized;
        _chomper.ChangeSpeed(_jumpPower * direction + new Vector2(0, _bonusJumpYSpeed));
        if (_player != null)
            Physics2D.IgnoreCollision(_thisCollider, _player, true);
        _canStopAttacking = false;
        _readyToDamage = true;
        StartCoroutine(GetReadyToStopAttacking());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_readyToDamage)
            return;
        if (!_alreadyDamaged)
        {
            Player player;
            if (player = collision.gameObject.GetComponent<Player>())
            {
                player.GetDamage(_chomper.Damage);
                _alreadyDamaged = true;
                return;
            }
        }
        if (collision.gameObject.GetComponent<Ground>() && _canStopAttacking)
        {
            IsAttacking = false;
            PrepareAttack = true;
            _alreadyDamaged = false;
            _readyToDamage = false;
            if (_player != null)
                Physics2D.IgnoreCollision(_thisCollider, _player, false);
            _chomper.ActivateEnemyAI();
            StartCoroutine(PrepareToAttack());
        }
    }

    private IEnumerator GetReadyToStopAttacking()
    {
        yield return new WaitForSeconds(0.05f);
        _canStopAttacking = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _radiusOfAttack);
    }

}
