using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class BokalAttack : MonoBehaviour
{
    [SerializeField] private float _reloadAttackTime;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Bokal _bocal;
    [SerializeField] private Animator _animator;

    private Vector3 playerPoint;

    void Attack()
    {
        playerPoint = _bocal.PlayerPosition.position;
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
        _animator.SetBool(WorkAnim.Bokal_Attack, false);
        yield return new WaitForSeconds(_reloadAttackTime);
        _bocal.justShot = false;
    }

}
