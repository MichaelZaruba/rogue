using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class EnemyAnimationChanger : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2;
    [SerializeField] private EnemyAttack _enemyAttack;
    [SerializeField] private AnimationChange _animationChange;
    private bool readyToMove = true;

    void Update()
    {
        ChangeAnimation();
    }

    void ChangeAnimation()
    {
        if (_enemyAttack.isAttacking && !readyToMove)
            return;
        if (_enemyAttack.prepareToAttack)
            readyToMove = true;
        if (_enemyAttack.isAttacking)
        {
            _animationChange.ChangeAnimationState(WorkAnim.Bacteria_Attack);
            readyToMove = false;
            return;
        }
        if (readyToMove)
        {
            if (Mathf.Abs(_rigidbody2.velocity.x) > 0.1f)
                _animationChange.ChangeAnimationState(WorkAnim.Bacteria_Moving);
            if (Mathf.Abs(_rigidbody2.velocity.x) < 0.1f)
                _animationChange.ChangeAnimationState(WorkAnim.Bacteria_Idle);
        }

        
    }
}
