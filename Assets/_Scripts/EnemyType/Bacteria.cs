using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bacteria : Enemy
{
    [SerializeField] private float _rangeToPatrol;
    [SerializeField] private float _prepareAttackTime;
    [SerializeField] private float attackPointWidth;
    [SerializeField] private float attackPointHeight;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool prepareToAttack = false;
    public Transform _attackPoint;

    

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        CheckMoveRight();
        Patrol();
        CheckMovingDirection();
        if (CheckPlayer())
        {

        }
    }



    protected override void CheckMoveRight()
    {
        base.CheckMoveRight();
    }

    protected override void Patrol()
    {
        base.Patrol();
    }

    private void Attack()
    {
        Collider2D player = Physics2D.OverlapBox(_attackPoint.position, new Vector2(attackPointWidth, attackPointHeight), 0, _playerLayer);
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
    protected override bool CheckPlayer()
    {
        return base.CheckPlayer();
    }

    private IEnumerator PrepareToAttack()
    {
        yield return new WaitForSeconds(_prepareAttackTime);
        prepareToAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawCube(_attackPoint.position, new Vector2(attackPointWidth, attackPointHeight));
    }

}
