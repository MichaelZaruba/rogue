using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IAttacker
{
    [SerializeField] private float _prepareAttackTime;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private float _attackWidth;
    [SerializeField] private float _attackHeight;

    [HideInInspector] public bool IsAttacking;
    [HideInInspector] public bool PrepareAttack;

    protected Enemy _enemy;

    protected LayerMask _playerLayerMask;

    protected EnemyAttackType _type;

    public float AttackWight => _attackWidth;
    public float AttackHeight => _attackHeight;

    public void Initialize(Enemy enemy, EnemyAttackType type, LayerMask player)
    {
        _playerLayerMask = player;
        _enemy = enemy;
        _type = type;
    }

    void Update()
    {
        if (_type == EnemyAttackType.Melee)
            MeleeAttack();
        else
            RangeAttack();
    }

    public virtual void MeleeAttack()
    {
        if (PrepareAttack || IsAttacking)
            return;

        Collider2D player = Physics2D.OverlapBox(_enemy.AttackPoint.transform.position,
            new Vector2(_attackWidth, _attackHeight), 0, _playerLayerMask);
        if (player == null)
            return;
        IsAttacking = true;
    }

    public virtual void RangeAttack()
    {
        throw new System.NotImplementedException();
    }

    private void Attack()
    {
        Collider2D player = Physics2D.OverlapBox(_enemy.AttackPoint.transform.position,
            new Vector2(_attackWidth, _attackHeight), 0, _playerLayerMask);
        if (player != null)
        {
            player.gameObject.GetComponent<Player>().GetDamage(_enemy.Damage);
        }
    }

    private void EndAttack()
    {
        PrepareAttack = true;
        IsAttacking = false;
        StartCoroutine(PrepareToAttack());
    }

    private IEnumerator PrepareToAttack()
    {
        yield return new WaitForSeconds(_prepareAttackTime);
        PrepareAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawCube(_attackPoint.position, new Vector2(_attackWidth, _attackHeight));
    }
}
