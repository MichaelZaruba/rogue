using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class CheckGround : MonoBehaviour
{
    [SerializeField] private PlayerMovement _player;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() && _player.Rigidbody.velocity.y < 0.01f)
        {
            _player.OnGround = true;
        }
    }
}
