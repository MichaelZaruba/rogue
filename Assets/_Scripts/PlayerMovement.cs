using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Transform _attackTransform;
    [SerializeField] private Animator _animator;
    [SerializeField] private  SpriteRenderer _spriteRenderer;
    [SerializeField] private AnimationChange _animationChange;
    [SerializeField] private float _staminaPerJump;
    [SerializeField] private float _staminaPerFastMove;
    [SerializeField, Range(3f,12f)] private float _powerJump;

    private Player _characteristic;
    
    private float _attackCorrectPosition = 1.3f;

    private float _horizontalSpeed;
    private float _verticalSpeed;

    private bool _isRightSide = true;

    public bool OnGround;
    public bool IsAttacking;
    public bool IsAttackingThrough;
    public bool IsAttackingDown;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Animator Animator => _animator;

    private void Awake()
    {
        _characteristic = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _characteristic.StaminaActive = false;
            _characteristic.MinusStamina(0, false);
        }

        if (IsClickSpace() && _characteristic.Stamina > 0)
        {
                _characteristic.MinusStamina(_staminaPerJump, true);
                Jump();     
        }

        else if (Input.GetKeyUp(KeyCode.Space)) 
        {
            _characteristic.MinusStamina(0, false);
        }
    }

    private void FixedUpdate()
    {
        if (IsAttackingThrough)
        {
            CalculateTroughSpeed();
        }
        if (IsAttackingDown)
        {
            CalculateDownSpeed();
        }
           
        if (Input.GetKey(KeyCode.LeftShift) && _characteristic.Stamina > 0 && !IsAttackingThrough && !IsAttackingDown)
        {
            if (_rigidbody.velocity.magnitude != 0)
                _characteristic.MinusStamina(_staminaPerFastMove, true);
 
                CalculateFastSpeed();
        }
        else if (!IsAttackingThrough && !IsAttackingDown)
        {
            CalculateSpeed();
        }

        Move();
        AnimationChange();
        ChangeSide();
    }

    private void CalculateDownSpeed()
    {
        _horizontalSpeed = _characteristic.Speed * Input.GetAxis("Horizontal");
        _verticalSpeed = -15f;
    }

    private void CalculateTroughSpeed()
    {
        
        _horizontalSpeed = _characteristic.Speed * Input.GetAxis("Horizontal") * 6f;
        _verticalSpeed = _rigidbody.velocity.y;
    }

    private void CalculateFastSpeed()
    {
        _horizontalSpeed = _characteristic.Speed * Input.GetAxis("Horizontal") * 1.5f;
        _verticalSpeed = _rigidbody.velocity.y;
    }

    private void CalculateSpeed()
    {
        _horizontalSpeed = _characteristic.Speed * Input.GetAxis("Horizontal");
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
        if (_horizontalSpeed < 0 && _isRightSide)
        {
            _isRightSide = false;
            _spriteRenderer.flipX = true;
            _attackTransform.position = new Vector3(transform.position.x - _attackCorrectPosition, transform.position.y, transform.position.z);
        }
        if (_horizontalSpeed > 0 && !_isRightSide)
        {
            _isRightSide = true;
            _spriteRenderer.flipX = false;
            _attackTransform.position = new Vector3(transform.position.x + _attackCorrectPosition, transform.position.y, transform.position.z);
        }
    }

    private void AnimationChange()
    {
        if (IsAttacking)
        {
            return;
        }

        if (OnGround)
        {
            if (Mathf.Abs(Rigidbody.velocity.x)> 0.01f)
                _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Run);
            if(Mathf.Abs(Rigidbody.velocity.x) <= 0.01f)
                _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Idle);
        }
        else
        {
            if (_verticalSpeed > 0)
                _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Jump);
            else _animationChange.ChangeAnimationState(Const.WorkAnim.Player_Fall);
        }
    }
}
