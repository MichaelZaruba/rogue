using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _prepareAttackTime;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private int attackDamage = 30;
    

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
        _player.Animator.SetBool("isAttacking", true);
        StartCoroutine(StopAttack());
      Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayers);
      foreach(Collider2D enemy in hitEnemies)
      {
         enemy.GetComponent<Enemy>().takeDamage(attackDamage);
         
      }
    }

    private IEnumerator StopAttack ()
    {
        yield return new WaitForSeconds(0.35f);
        _player.Animator.SetBool("isAttacking", false);
        _isAttacking = false;
    }
}
