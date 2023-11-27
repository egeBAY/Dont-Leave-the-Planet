using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeed = 1f;
    public float jumpHeight = 1f;

    private Rigidbody2D body;
    private Animator animator;
    private float moveInput;
    private bool onGround;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
        SetAnimations();
    }

    private void Move()
    {
        moveInput = UserInput.instance_.MoveInput.x;

        // Movement with transform
        // transform.Translate(moveInput * playerSpeed, 0f, 0f); 
        // Movement with rigidbody
        if (moveInput > 0)
            transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
        else if(moveInput < 0)
            transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        
        body.velocity = new Vector2(moveInput * playerSpeed, body.velocity.y);
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (onGround)
            {
                body.velocity = new Vector2(body.velocity.x, jumpHeight);
                animator.SetTrigger("jump");
                onGround = false;
            }
        }
        
        
    }

    private void SetAnimations()
    {
        animator.SetBool("IsWalking", moveInput != 0);
        animator.SetBool("OnGround", onGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {   
            onGround = true;
        }
    }
}
