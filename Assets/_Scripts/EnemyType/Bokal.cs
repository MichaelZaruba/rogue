using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Const;

public class Bokal : Enemy
{
    [SerializeField] private float _positionToPatrol;
    [SerializeField] private Animator _animator;

    private Vector3 _startPosition;
    

    private bool _moveRight;
    public bool justShot;

    

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        CheckMoveRight();
        Patrol();
        if (CheckPlayer())
        {
            
            ChrgeAttack();
        }
    }

    private void CheckMoveRight()
    {
        if (transform.position.x > _startPosition.x + _positionToPatrol)
        {
            _moveRight = false;
        }
        else if (transform.position.x < _startPosition.x - _positionToPatrol)
        {
            _moveRight = true;
        }
    }

    private void Patrol()
    {
        if (_moveRight)
        {
            _rigidbody.velocity = Vector2.right *  _speed;
        }
        if (!_moveRight)
            _rigidbody.velocity = Vector2.left * _speed;
    }

    private void ChrgeAttack()
    {
        if (justShot)
            return;
        _animator.SetBool(WorkAnim.Bokal_Attack, true);
        justShot = true;
    }
   

}
