using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] private PlayerMovement _player;

    [SerializeField] private Transform _attackPoint;

    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private AnimationChange _animationChange;
    [SerializeField] private TextMeshProUGUI _textAttack;
    [SerializeField] private TextMeshProUGUI _textDie;

    [SerializeField] private float _staminaPerAttack;
    [SerializeField, Range(0f, 1f)] private float _prepareAttackTime;
    [SerializeField, Range(0f, 1f)] private float _endAttackTime;
    private List<Collider2D> _enemyCollider = new List<Collider2D>();
    private Player _characteristic;


    private void Awake()
    {
        _characteristic = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) && !_player.IsAttacking)
        {
            Attack(Const.WorkAnim.Player_Attack, 0.2f,2.5f, true);
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Attack(Const.WorkAnim.Player_Attack, 0.5f, _characteristic.RangeAttack, false);
        }
    }

    private void Attack(string correctAttack,float endAttackTime, float rangeAttack, bool troughAttack)
    {
        if (_player.IsAttacking && _player.IsAttackingThrough)
            return;

        if (_characteristic.Stamina >= _staminaPerAttack)
        {
            if (troughAttack)
                _player.IsAttackingThrough = true;
            _player.IsAttacking = true;
            _characteristic.MinusStamina(_staminaPerAttack, true);
            StartCoroutine(PrerareAttack(correctAttack, endAttackTime, rangeAttack));
        }
    }

    private IEnumerator PrerareAttack(string correctAttack, float endAttackTime, float rangeAttack)
    {
        SearchInRangeAndTakeDamage(rangeAttack);
        AnimationAttack(correctAttack);
        yield return new WaitForSeconds(endAttackTime);
        EndAttack();  
    }
    private void SearchInRangeAndTakeDamage(float rangeAttack)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, rangeAttack, _enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), enemy);
            _textAttack.gameObject.SetActive(true);
            _textAttack.text = _characteristic.Damage.ToString();
            enemy.GetComponent<Enemy>().TakeDamage(_characteristic.Damage);
            _enemyCollider.Add(enemy);
        }
    }

    private void EndAttack()
    {
        foreach (var enemy in _enemyCollider)
        {
            if (enemy != null)
            {
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), enemy, false);   
            }
        }
        _textAttack.gameObject.SetActive(false);
        _player.IsAttackingThrough = false;
        _player.IsAttacking = false;
        _characteristic.MinusStamina(0, false);
    }

    private void AnimationAttack(string correctAttack)
    {
        if (_player.IsAttacking)
        {
            _animationChange.ChangeAnimationState(correctAttack);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawSphere(_attackPoint.position, 1.28f);
    }
}
