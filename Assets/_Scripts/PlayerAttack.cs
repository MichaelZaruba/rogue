using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private PlayerMovement _player;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private LayerMask _enemyLayers;

    [SerializeField] private TextMeshProUGUI _textAttack;
    [SerializeField] private TextMeshProUGUI _textDie;

    [SerializeField, Range(0f, 1f)] private float _prepareAttackTime;
    [SerializeField, Range(0f, 1f)] private float _endAttackTime;

    private PlayerCharacteristic _characteristic;

    private bool _isAttacking;

    private void Awake()
    {
        _characteristic = GetComponent<PlayerCharacteristic>();
    }

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

        if (_characteristic.Stamina > 0)
        {
            _isAttacking = true;
            _player.IsAttacking = true;
            _characteristic.MinusStamina(3, true);
            StartCoroutine(PrerareAttack());
        }
           
    }

    private IEnumerator PrerareAttack()
    {
        yield return new WaitForSeconds(_prepareAttackTime);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _characteristic.RangeAttack, _enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            _textAttack.gameObject.SetActive(true);
            _textAttack.text = _characteristic.Damage.ToString();
            enemy.GetComponent<Enemy>().takeDamage(_characteristic.Damage);
        }
        yield return new WaitForSeconds(_endAttackTime);
        _textAttack.gameObject.SetActive(false);
        _isAttacking = false;
        _player.IsAttacking = false;
        _characteristic.MinusStamina(0, false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,1,1,0.5f);
        
        Gizmos.DrawSphere(_attackPoint.position, 1.28f);
    }
}
