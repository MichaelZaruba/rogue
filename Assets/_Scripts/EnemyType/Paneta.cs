using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paneta : Enemy
{
    private void FixedUpdate()
    {
        CheckMoveRight();
        Patrol();
        if (CheckPlayer())
        {

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

    protected override void CheckMoveRight()
    {
         base.CheckMoveRight();
    }
}
