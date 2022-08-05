using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class Player : MonoBehaviour
{   
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private Animator _animator;
    [SerializeField] private  SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject Ground;
    [SerializeField] private AnimationChange _animationChange;
    [SerializeField,Range(1f,10f)] private float _speed;
    [SerializeField, Range(3f,12f)] private float _powerJump;

    private float _horizontalSpeed;
    private float _verticalSpeed;

    

    public bool OnGround;
    private bool isRightSide = true;

    public Rigidbody2D Rigidbody => _rigidbody;
    public Animator Animator => _animator;

    void Update()
    {       
        if (IsClickSpace())
        {
            Jump();
        }  
    }

    private void FixedUpdate()
    {
        CalculateSpeed();
        Move();
        AnimationChange();
        ChangeSide();
    }
    private void CalculateSpeed()
    {
        _horizontalSpeed = _speed * Input.GetAxis("Horizontal");
        _verticalSpeed = _rigidbody.velocity.y;
    }

    private void Move()
    {       
        _rigidbody.velocity = new Vector2(_horizontalSpeed, _verticalSpeed);
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _powerJump);
        _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Jump);
        OnGround = false;
    }

    private bool IsClickSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGround)
        {
            return true;
        }
        return false;
    }

    private void ChangeSide()
    {
        if (_horizontalSpeed < 0 && isRightSide)
        {
            isRightSide = false;
            _spriteRenderer.flipX = true;
        }
        if (_horizontalSpeed > 0 && !isRightSide)
        {
            isRightSide = true;
            _spriteRenderer.flipX = false;
        }
    }

    private void AnimationChange()
    {
        if (OnGround)
        {
            if (_horizontalSpeed > 0.01f)
                _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Run);
            else _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Idle);
        }
        else
        {
            if (_verticalSpeed > 0)
                _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Jump);
            else _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Fall);
        }
    }

    

}
