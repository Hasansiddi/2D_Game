using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Robot_Movement : MonoBehaviour
{

    // Movement
    private float horizontal;
    private const float MoveSpeed = 8.5F;
    private const float JumpHeight = 10F;
    private bool isGrounded = true;
    private bool isFacingRight = true;

    // Game Information
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFacingRight && horizontal < 0)
        {
            Flip();
        } 
        else if (!isFacingRight && horizontal > 0)
        {
            Flip();
        }

        AnimateMovement();
        rb.velocity = new Vector2(horizontal * MoveSpeed, rb.velocity.y);
    }

    // Getting this so that it can be updated per framp in Update method
    public void OnMove(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    // Only Jump on keypress, can't hold it down to fly
    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            isGrounded = false;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        Vector3 localPosition = transform.localPosition;
        localScale.x *= -1;
        if(isFacingRight == false)
        {
            localPosition.x -= 1;
        }
        else
        {
            localPosition.x += 1;
        }
        transform.localPosition = localPosition;
        transform.localScale = localScale;
    }

    private void AnimateMovement()
    {
        if (horizontal != 0)
        {
            animator.SetBool("moving", true);
        } else
        {
            animator.SetBool("moving", false);
        }
    }
}
