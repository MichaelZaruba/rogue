using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bokal : Enemy
{
    [SerializeField] private float _positionToPatrol;
     private Vector3 _startPosition;

    private bool _moveRight;

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
    private bool CheckPlayer()
    {
        return true;
    }

}
