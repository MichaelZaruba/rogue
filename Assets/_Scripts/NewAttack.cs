using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAttack : MonoBehaviour
{
    [SerializeField] private AttackType _type;
    [SerializeField,Range(1f,3f)] private float _speedRotation;
    private AttackInventory _inventory;
    private Player _player;
    public bool InRange;
    
    public void Initialize(Player player, AttackInventory attackInventory)
    {
        _inventory = attackInventory;
           _player = player; 
    }



    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0, _speedRotation, 0);

        if (!InRange)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            _inventory.ActivateItem(_type);
            if (_type == AttackType.Through)
            {
                PlayerPrefs.SetInt(PlayerAttack.THROUGH_ATTACK, 1);
                _player.GetComponent<PlayerAttack>().IsThroughAttackActivate = true;
            }

            if (_type == AttackType.Down)
            {
                PlayerPrefs.SetInt(PlayerAttack.DOWN_ATTACK, 1);
                _player.GetComponent<PlayerAttack>().IsThroughDownActivate = true;
            }
            Destroy(gameObject);
        }
    }
}
