using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy
{
    private void FixedUpdate()
    {
        CheckMoveRight();
        Patrol();
        CheckMovingDirection();
        if (CheckPlayer())
        {

        }
    }

    protected override void CheckMovingDirection()
    {
        base.CheckMovingDirection();
    }

    protected override bool CheckPlayer()
    {
        return base.CheckPlayer();
    }

    protected override void Patrol()
    {
        base.Patrol();
    }

    protected override void CheckMoveRight()
    {
        base.CheckMoveRight();
    }
}
