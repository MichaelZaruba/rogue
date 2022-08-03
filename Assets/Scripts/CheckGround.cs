using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Const;

public class CheckGround : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ground>() && _player.Rigidbody.velocity.y < 0.01f)
        {
           _player.Animator.SetBool(WorkAnim.IS_JUMPING, false);
           _player.OnGround = true;
        }
    }
}
