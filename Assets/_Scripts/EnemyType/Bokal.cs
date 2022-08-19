using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Const;

public class Bokal : Enemy
{
    
    private void FixedUpdate()
    {
        CheckMoveRight();
        Patrol();
        if (CheckPlayer())
        {
            ChrgeAttack();
        }
    }

    protected override bool CheckPlayer()
    {
        return base.CheckPlayer();
    }

    protected override void Patrol()
    {
        base.Patrol();
    }

    private void ChrgeAttack()
    {
        if (_justShot)
            return;
        _animator.SetBool(EnemyAnim.IS_ATTACK, true);
        _justShot = true;
    }
   

}
