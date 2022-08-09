using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Const;

public class Bokal : Enemy
{
    [SerializeField] private float _positionToPatrol;
    [SerializeField] private float _radiusOfVision;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private Animator _animator;

    private Vector3 _startPosition;
    public Transform playerPosition;

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
            MoveToPlayer();
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



    private bool CheckPlayer()
    {

        Collider2D player = Physics2D.OverlapCircle(transform.position, _radiusOfVision, _playerLayer);

        if (player == null)
            return false;
        //reurn false if there is a wall between player and bokal
        playerPosition = player.transform;
        return true;
    }

    private void MoveToPlayer()
    {

    }

    private void ChrgeAttack()
    {
        if (justShot)
            return;
        _animator.SetBool(WorkAnim.Bokal_Attack, true);
        justShot = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, _radiusOfVision);
    }


}
