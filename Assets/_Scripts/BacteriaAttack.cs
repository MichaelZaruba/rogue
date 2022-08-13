using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacteriaAttack : MonoBehaviour
{
    [SerializeField] private Bacteria _bacteria;
    [SerializeField] private LayerMask _playerLayerMask;
    

    void Update()
    {
        CheckIfPlayerIsNear();
    }

    private void CheckIfPlayerIsNear()
    {
        if (_bacteria.prepareToAttack || _bacteria.isAttacking)
        {
            return;
        }

        Collider2D player = Physics2D.OverlapBox(_bacteria._attackPoint.position, new Vector2(1.02f, 0.21f), 0, _playerLayerMask);
        if (player == null)
            return;
        _bacteria.isAttacking = true;
    }

}
