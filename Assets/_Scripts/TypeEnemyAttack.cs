using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TypeEnemyAttack : MonoBehaviour, IAttacker
{
    [SerializeField] private float _prepareAttackTime;

    public Transform _attackPoint;

    public float attackPointWidth;
    public float attackPointHeight;

    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool prepareToAttack = false;

    protected LayerMask _playerLayerMask;
    protected Enemy _enemy;

    [SerializeField] protected EnemyAttackType _type;


    public virtual void MeleeAttack()
    {
        if (prepareToAttack || isAttacking)
            return;

        Collider2D player = Physics2D.OverlapBox(_enemy.AttackPoint.transform.position,
            new Vector2(attackPointWidth, attackPointHeight), 0, _playerLayerMask);
        if (player == null)
            return;
        isAttacking = true;
    }

    public virtual void RangeAttack()
    {
        throw new System.NotImplementedException();
    }

    private void Attack()
    {
        Collider2D player = Physics2D.OverlapBox(_enemy.AttackPoint.transform.position,
            new Vector2(attackPointWidth, attackPointHeight), 0, _playerLayerMask);
        if (player != null)
        {
            player.gameObject.GetComponent<Player>().GetDamage(_enemy.Damage);
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
}

public enum EnemyAttackType
{
    Melee,
    Range
}
