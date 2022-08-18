using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Enemy
{

    private void Start()
    {
        _spawnPosition = transform.position;
    }

    private void Update()
    {
        CheckMoveRight();
        Patrol();
        CheckMovingDirection();
        if (CheckPlayer())
        {

        }
    }



    protected override void CheckMoveRight()
    {
        base.CheckMoveRight();
    }

    protected override void Patrol()
    {
        base.Patrol();
    }

    protected override bool CheckPlayer()
    {
        return base.CheckPlayer();
    }

}
