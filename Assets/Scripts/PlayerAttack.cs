using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textAttack;
    [SerializeField] private TextMeshProUGUI _textDie;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private Player _player;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private float _attackRange;  

    [SerializeField, Range(1f, 30f)] private int attackDamage;

    [SerializeField, Range(0f, 1f)] private float _prepareAttackTime;
    [SerializeField, Range(0f, 1f)] private float _endAttackTime;

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
            _textAttack.gameObject.SetActive(true);
            _textAttack.text = attackDamage.ToString();
            enemy.GetComponent<Enemy>().takeDamage(attackDamage);
        }
        yield return new WaitForSeconds(_endAttackTime);
        _textAttack.gameObject.SetActive(false);
        _isAttacking = false;
        _player.IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,1,1,0.5f);
        
        Gizmos.DrawSphere(_attackPoint.position, _attackRange);
    }
}
