using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : TypeEnemyAttack, IAttacker
{
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

    public override void MeleeAttack()
    {
        base.MeleeAttack();
    }

    public override void RangeAttack()
    {
        throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawCube(_attackPoint.position, new Vector2(attackPointWidth, attackPointHeight));
    }
}
