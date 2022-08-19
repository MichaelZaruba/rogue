using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class EnemyAnimationChanger : MonoBehaviour
{
     private Rigidbody2D _rigidbody;
     private EnemyAttack _enemyAttack;
     private AnimationChange _animationChange;
    private bool readyToMove = true;

    public void Initialize(Rigidbody2D rigidbody, EnemyAttack enemyAttack, AnimationChange animationChange)
    {
        _rigidbody = rigidbody;
        _enemyAttack = enemyAttack;
        _animationChange = animationChange;
    }

    void Update()
    {
        ChangeAnimation();
    }

    void ChangeAnimation()
    {
        if (_enemyAttack.IsAttacking && !readyToMove)
            return;
        if (_enemyAttack.PrepareAttack)
            readyToMove = true;
        if (_enemyAttack.IsAttacking)
        {
            _animationChange.ChangeAnimationState(EnemyAnim.ATTACK);
            readyToMove = false;
            return;
        }
        if (readyToMove)
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > 0.1f)
                _animationChange.ChangeAnimationState(EnemyAnim.MOVING);
            if (Mathf.Abs(_rigidbody.velocity.x) < 0.1f)
                _animationChange.ChangeAnimationState(EnemyAnim.IDLE);
        }
    }
}
