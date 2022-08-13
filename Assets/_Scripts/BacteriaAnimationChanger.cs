using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class BacteriaAnimationChanger : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2;
    [SerializeField] private Bacteria _bacteria;
    [SerializeField] private AnimationChange _animationChange;
    private bool readyToMove = true;

    void Update()
    {
        ChangeAnimation();
    }

    void ChangeAnimation()
    {
        if (_bacteria.isAttacking && !readyToMove)
            return;
        if (_bacteria.prepareToAttack)
            readyToMove = true;
        if (_bacteria.isAttacking)
        {
            _animationChange.ChangeAnimationState(WorkAnim.Bacteria_Attack);
            readyToMove = false;
            return;
        }
        if (readyToMove)
        {
            if (_rigidbody2.velocity.x > 0.1f)
                _animationChange.ChangeAnimationState(WorkAnim.Bacteria_Moving);
            if (_rigidbody2.velocity.x < 0.1f)
                _animationChange.ChangeAnimationState(WorkAnim.Bacteria_Idle);
        }
    }
}
