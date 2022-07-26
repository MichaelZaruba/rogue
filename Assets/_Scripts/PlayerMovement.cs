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

    private int _countJump = 0;

    public int MaxJump;

    public GameObject EffectRight;
    public GameObject EffectLeft;

    [HideInInspector]  public GameObject CorrectEffect;
    public Rigidbody2D Rigidbody => _rigidbody;
    public Animator Animator => _animator;

    public bool OnGround;
    public bool IsAttacking;
    public bool IsAttackingThrough;
    public bool IsAttackingDown;

    public int CountJump { get => _countJump; set => _countJump = value; }

    private void Awake()
    {
        CorrectEffect = EffectRight;
       _characteristic = GetComponent<Player>();
        _animationChange.Animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Game._instance.IsPause)
            return;

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
        if (Game._instance.IsPause)
        {
            _rigidbody.velocity = new Vector2 (0, _rigidbody.velocity.y);
            return;
        }

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
            if (_rigidbody.velocity.magnitude != 0 && OnGround)
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
        OnGround = false;
    }

    private bool IsClickSpace()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (OnGround || _countJump < MaxJump))
        {
            Debug.Log(MaxJump);  
            _countJump++;
            return true;
        }
        return false;
    }

    private void ChangeSide()
    {
        if (IsAttacking)
        {
            return;
        }
        if (_horizontalSpeed < 0 && _isRightSide)
        {
            CorrectEffect = EffectLeft;
            _isRightSide = false;
            _spriteRenderer.flipX = true;
            _attackTransform.position = new Vector3(transform.position.x - _attackCorrectPosition, transform.position.y, transform.position.z);
        }
        if (_horizontalSpeed > 0 && !_isRightSide)
        {
            CorrectEffect = EffectRight;
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
            if (Mathf.Abs(Rigidbody.velocity.x) > 0.01f && Mathf.Abs(Rigidbody.velocity.x) <= _characteristic.Speed)
                _animationChange.ChangeAnimationState(Const.PlayerAnim.Player_Run);
            if(Mathf.Abs(Rigidbody.velocity.x) <= 0.01f)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    _animationChange.ChangeAnimationState(Const.PlayerAnim.Player_Crouch);
                    return;
                }

                _animationChange.ChangeAnimationState(Const.PlayerAnim.Player_Idle);
            }
            if (Mathf.Abs(Rigidbody.velocity.x) > _characteristic.Speed + 0.01f)
                _animationChange.ChangeAnimationState(Const.PlayerAnim.Player_Sprint);
        }
        else
        {
            if (_verticalSpeed > 0)
                _animationChange.ChangeAnimationState(Const.PlayerAnim.Player_Jump);
            else _animationChange.ChangeAnimationState(Const.PlayerAnim.Player_Fall);
        }
    }
}
