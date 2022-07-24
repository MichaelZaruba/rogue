using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private float horizontalSpeed;
    [SerializeField] private Rigidbody2D rigidbody2;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    private bool rightdir;
    private bool isJump;
    private bool onGround;
    private void Start()
    {
        rightdir = true;
        onGround = true;
    }

    void Update()
    {
        horizontalSpeed = 5 * Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
            isJump = true;
        animator.SetFloat("speed", Mathf.Abs(horizontalSpeed));
        animator.SetFloat("vertical speed", rigidbody2.velocity.y);
        if (horizontalSpeed < 0 && rightdir)
        {
            rightdir = false;
            spriteRenderer.flipX = true;
        }
        if (horizontalSpeed > 0 && !rightdir)
        {
            rightdir = true;
            spriteRenderer.flipX = false;
        }

    }

    private void FixedUpdate()
    {
        rigidbody2.velocity = new Vector2(horizontalSpeed, rigidbody2.velocity.y);
        if (isJump && onGround)
        {
            animator.SetBool("isJumping", true);
            rigidbody2.velocity = new Vector2(rigidbody2.velocity.x, 10);
            isJump = false;
            onGround = false;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Debug.Log("nigger");
        animator.SetBool("isJumping", false);
        onGround = true;
        
    }

}
