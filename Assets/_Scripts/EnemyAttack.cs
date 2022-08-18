using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : TypeEnemyAttack, IAttacker
{
    [SerializeField] private float _rangeToPatrol;
    [SerializeField] private float _prepareAttackTime;

    public Transform _attackPoint;

    public float attackPointWidth;
    public float attackPointHeight;

    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool prepareToAttack = false;

    [SerializeField] private LayerMask _playerLayerMask;
    private Enemy _enemy;

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;
    }

    void Update()
    {
        if (_type == EnemyAttackType.Melee)
            MeleeAttack();
        else
            RangeAttack();
    }

    public override void MeleeAttack()
    {
        if (prepareToAttack || isAttacking)
            return;

        Collider2D player = Physics2D.OverlapBox(_enemy._attackPoint.position,
            new Vector2(attackPointWidth, attackPointHeight), 0, _playerLayerMask);
        if (player == null)
            return;
        isAttacking = true;
    }

    public void RangeAttack()
    {
        throw new System.NotImplementedException();
    }

    private void Attack()
    {
        Collider2D player = Physics2D.OverlapBox(_enemy._attackPoint.position,
            new Vector2(attackPointWidth, attackPointHeight), 0, _playerLayerMask);
        if (player != null)
        {
            player.gameObject.GetComponent<Player>().GetDamage(_enemy._damage);
        }
    }

    private void EndAttack()
    {
        prepareToAttack = true;
        isAttacking = false;
        StartCoroutine(PrepareToAttack());
    }

    private IEnumerator PrepareToAttack()
    {
        yield return new WaitForSeconds(_prepareAttackTime);
        prepareToAttack = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawCube(_attackPoint.position, new Vector2(attackPointWidth, attackPointHeight));
    }
}
