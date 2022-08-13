using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Enemy
{
    [SerializeField] private float _rangeToPatrol;
    [SerializeField] private float _prepareAttackTime;
    [HideInInspector] public Transform _attackPoint;
    [SerializeField] private LayerMask _playerLayerMask;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool prepareToAttack = false;
    [HideInInspector] public bool movingRight = false;

    private Vector3 _startPosition;
    private bool _moveRight;
    

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        CheckMoveRight();
        CheckMovingDirection();
        Patrol();
        if (CheckPlayer())
        {

        }
    }

    private void CheckMovingDirection()
    {
        if (_rigidbody.velocity.x > 0 && !movingRight)
        {

            movingRight = true;
            return;
        }
        if (_rigidbody.velocity.x < 0 && movingRight)
        {
            movingRight = false;
        }
    }

    private void CheckMoveRight()
    {
        if (transform.position.x > _startPosition.x + _rangeToPatrol)
        {
            _moveRight = false;
        }
        else if (transform.position.x < _startPosition.x - _rangeToPatrol)
        {
            _moveRight = true;
        }
    }

    private void Patrol()
    {
        if (_moveRight)
        {
            _rigidbody.velocity = Vector2.right * _speed;
        }
        if (!_moveRight)
            _rigidbody.velocity = Vector2.left * _speed;
    }
    private void Attack()
    {
        Collider2D player = Physics2D.OverlapBox(_attackPoint.position, new Vector2(1.02f, 0.21f), 0, _playerLayerMask);
        if (player != null)
        {
            player.gameObject.GetComponent<Player>().GetDamage(_damage);
        }
    }

    private void EndAttack()
    {
        prepareToAttack = true;
        isAttacking = false;
        StartCoroutine(PrepareToAttack());
    }

    private IEnumerator PrepareToAttack()
    {
        yield return new WaitForSeconds(_prepareAttackTime);
        prepareToAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawCube(_attackPoint.position, new Vector2(1.02f, 0.21f));
    }

}
