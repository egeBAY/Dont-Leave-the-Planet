using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    public float playerSpeed = 1f;
    public float jumpHeight = 1f;
    
    private Rigidbody2D body;
    private Animator animator;
    private bool onGround;
    bool isGroundedCheckStop = false;
    Vector2 movementVector = Vector2.zero;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movementVector.x = UserInput.instance_.MoveInput.x * playerSpeed;

        HandleSpriteDirection();

        HandleGroundedCheck();

        SetAnimations();
    }

    private void FixedUpdate()
    {
        SetAnimations();

        if (!onGround)
            movementVector.y += Physics2D.gravity.y * Time.fixedDeltaTime;

        body.MovePosition(body.position + movementVector * Time.fixedDeltaTime);
    }

    private void HandleSpriteDirection()
    {
        if (movementVector.x > 0)
            transform.localScale = new Vector3(-0.75f, 0.75f, 1f);
        else if (movementVector.x < 0)
            transform.localScale = new Vector3(0.75f, 0.75f, 1f);
    }

    public void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed && onGround)
        {
            //body.velocity = new Vector2(body.velocity.x, jumpHeight);
            animator.SetTrigger("jump");
            onGround = false;
            isGroundedCheckStop = true;
            movementVector.y = jumpHeight;
            StartCoroutine(ResetGroundedCheckStop());

        }
    }

    private void HandleGroundedCheck()
    {
        onGround = IsGrounded();

        if (onGround)
        {
            //animator.SetBool("Jumping", false);
            //animator.SetFloat("InputX", Mathf.Abs(movementVector.x));
            movementVector.y = 0;
        }
    }

    private void SetAnimations()
    {
        animator.SetBool("IsWalking", movementVector.x != 0);
        animator.SetBool("OnGround", onGround);
    }

    private bool IsGrounded()
    {

        // Vector3 rayOffSet = new Vector3(0, -0.75f, 0);

        if (isGroundedCheckStop)
            return false;
        RaycastHit2D result = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer);
        return result.collider != null;
    }


    private IEnumerator ResetGroundedCheckStop()
    {
        yield return new WaitForSeconds(0.1f);
        isGroundedCheckStop = false;
    }
}
