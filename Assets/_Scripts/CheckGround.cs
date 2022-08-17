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
            _player.CountJump = 0;
            _player.OnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            _player.OnGround = false;
        }
    }
}
