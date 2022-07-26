using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [SerializeField,Range(1f,10f)] private float _speed;
    [SerializeField, Range(3f,12f)] private float _powerJump;

    private float _horizontalSpeed;

    private const string SPEED = "speed";
    private const string IS_JUMPING = "isJumping";
    private const string VERTICAL_SPEED = "VerticalSpeed";
  
    private bool _onGround;
    private bool isRightSide = true;

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
        _animator.SetBool(IS_JUMPING, true);
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _powerJump);
        _onGround = false;
    }

    private bool IsClickSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
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
        _animator.SetFloat(SPEED, Mathf.Abs(_horizontalSpeed));
        _animator.SetFloat(VERTICAL_SPEED, _rigidbody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _animator.SetBool(IS_JUMPING, false);
            _onGround = true;
        }    
    }
}
