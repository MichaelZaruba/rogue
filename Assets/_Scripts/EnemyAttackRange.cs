using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class EnemyAttackRange : MonoBehaviour
{
    [SerializeField, Range(3f,5f)] private float _reloadAttackTime;
     private Bullet _bullet;
     private Enemy _enemy;
     private Animator _animator;

    private Player _player;

    private Vector3 playerPoint;

    public void Initialize(Player player, Bullet bullet, Enemy enemy)
    {
        _animator = GetComponent<Animator>();
        _enemy = enemy;
        _bullet = bullet;
        _player = player;
    }

    void Attack()
    {
        if (_enemy.Type == EnemyAttackType.Melee)
            return;
        playerPoint = _player.transform.position;
        CalculatePositionPlayer();
        var bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        bullet.transform.position = transform.position;
        StartCoroutine(PrepareAnotherAttack());
    }

    private void CalculatePositionPlayer()
    {
        Vector3 direction = (playerPoint - transform.position).normalized;
        Debug.DrawLine(playerPoint, playerPoint + direction * 10, Color.red, Mathf.Infinity);
        _bullet.direction = direction;
    }

    public IEnumerator PrepareAnotherAttack()
    {
        _animator.SetBool(EnemyAnim.IS_ATTACK, false);
        yield return new WaitForSeconds(_reloadAttackTime);
        _enemy.JustShot = false;
    }

}
