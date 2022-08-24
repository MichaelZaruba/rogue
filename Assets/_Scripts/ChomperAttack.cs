using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperAttack : EnemyAttackMelee
{
    [SerializeField] private float _radiusOfAttack;
    [SerializeField] private float _bonusJumpYSpeed;
    [SerializeField] private float _jumpPower;
    private Collider2D _player;
    private Vector3 _playerLastPosition;
    private Chomper _chomper;

    public override void Initialize(Enemy chomper, EnemyAttackType type, LayerMask player)
    {
        _attackPoint = GetComponentInChildren<AttackPoint>();
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

        Collider2D _player = Physics2D.OverlapCircle(transform.position, _radiusOfAttack);
        if (_player == null)
            return;
        _playerLastPosition = _player.transform.position;
        _chomper.ChangeSpeed(new Vector2(0, 0));
        IsAttacking = true;
    }

    protected override void Attack()
    {
        Vector2 direction = (_playerLastPosition - transform.position).normalized;
        _chomper.ChangeSpeed(_jumpPower * direction + new Vector2(0, _bonusJumpYSpeed));
        if (_player != null)
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), _player);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsAttacking || _chomper.OnGround)
            return;
        Player player;
        if (player = collision.gameObject.GetComponent<Player>())
        {
            player.GetDamage(_chomper.Damage);
            return;
        }
        if (collision.gameObject.GetComponent<Ground>())
        {
            IsAttacking = false;
            PrepareAttack = true;
            if (_player != null)
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), _player, false);
            StartCoroutine(PrepareToAttack());
            _chomper.OnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _chomper.OnGround = false;
        }
    }
}
