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
    [SerializeField,Range(1f,10f)] private float _speed;
    [SerializeField, Range(3f,12f)] private float _powerJump;

    private float _horizontalSpeed;

    public string _currentState;

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
    }

    private void Move()
    {       
        _rigidbody.velocity = new Vector2(_horizontalSpeed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        _animator.SetBool(WorkAnim.IS_JUMPING, true);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _powerJump);
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
        _animator.SetFloat(WorkAnim.SPEED, Mathf.Abs(_horizontalSpeed));
        _animator.SetFloat(WorkAnim.VERTICAL_SPEED, _rigidbody.velocity.y);
    }

    public void ChangeAnimationState(string newState)
    {
        if (_currentState == newState)
            return;
        _currentState = newState;
        Animator.Play(newState);
    }

}
