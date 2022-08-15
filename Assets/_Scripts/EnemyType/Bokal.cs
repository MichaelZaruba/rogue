using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Const;

public class Bokal : Enemy
{
    [SerializeField] private Animator _animator;

    public bool justShot;
    
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
        if (justShot)
            return;
        _animator.SetBool(WorkAnim.Bokal_Attack, true);
        justShot = true;
    }
   

}
