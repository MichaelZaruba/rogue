using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _prepareAttackTime;
    [SerializeField, Range(0f, 1f)] private float _endAttackTime;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private int attackDamage = 30;

    private const string PrepareAttack = "PrerareAttack";
    private bool _isAttacking;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();

        }
    }

    private void Attack()
    {
        if (_isAttacking)
            return;
        _isAttacking = true;
        _player.IsAttacking = true;
        StartCoroutine(PrerareAttack());
    }

    private IEnumerator PrerareAttack()
    {
        yield return new WaitForSeconds(_prepareAttackTime);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
        }
        yield return new WaitForSeconds(_endAttackTime);
        _isAttacking = false;
        _player.IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,1,1,0.5f);
        Gizmos.DrawSphere(_attackPoint.position, _attackRange);
    }
}
