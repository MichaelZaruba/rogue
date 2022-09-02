using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy
{
    [HideInInspector] public bool CanMove = true;
    private void Update()
    {
        CheckMoveRight();
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
        if (!CanMove)
            return false;

        Collider2D player = Physics2D.OverlapCircle(transform.position, _radiusOfVision, _playerLayer);

        if (player == null)
        {
            IsFindPlayer = false;
            return false;
        }
        IsFindPlayer = true;
        _enemyAI.IsTargetActive = true;
        return true;
    }

    protected override void Patrol()
    {
        if (_enemyAI.IsTargetActive)
            return;
        base.Patrol();
    }

    protected override void CheckMoveRight()
    {
        base.CheckMoveRight();
    }
    public void ChangeSpeed(Vector2 newSpeed)
    {
        CanMove = false;
        _enemyAI.IsTargetActive = false;
        _rigidbody.velocity = newSpeed;
    }

    public void ActivateEnemyAI()
    {
        CanMove = true;
        _enemyAI.IsTargetActive = true;
    }



}
