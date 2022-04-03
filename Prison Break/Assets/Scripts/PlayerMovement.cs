using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float climbSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator animator;
    CapsuleCollider2D bodyColldier;
    BoxCollider2D feetCollider;
    float gravity;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyColldier = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravity = rb2d.gravityScale;
    }

    void Update()
    {
        Run();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        FlipSprite();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb2d.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        rb2d.velocity = new Vector2(moveInput.x*runSpeed, rb2d.velocity.y);

        animator.SetBool("isRunning", Mathf.Abs(moveInput.x) > Mathf.Epsilon);
    }

    void FlipSprite()
    {
        bool playerHasHorisonatlSpeed = Mathf.Abs(moveInput.x) > Mathf.Epsilon;

        if(playerHasHorisonatlSpeed)
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1f);
    }

    void ClimbLadder()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb2d.gravityScale = 0f;
            rb2d.velocity = new Vector2(rb2d.velocity.x, moveInput.y * climbSpeed);

            if (Mathf.Abs(moveInput.y) > Mathf.Epsilon)
                animator.SetBool("isClimbing", true);
            else
                animator.SetBool("isClimbing", false);
        }
        else
        {
            animator.SetBool("isClimbing", false);
            rb2d.gravityScale = gravity;
        }
    }
}
