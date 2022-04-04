using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 3f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0f, 10f);
    [SerializeReference] GameObject arrow;
    [SerializeReference] Transform bow;

    Vector2 moveInput;
    Rigidbody2D rb2d;
    Animator animator;
    CapsuleCollider2D bodyColldier;
    BoxCollider2D feetCollider;
    float gravity;

    bool isAlive = true;
    bool canFire = true;
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
        if (isAlive)
        {
            Run();
            ClimbLadder();
            Die();
        }
    }

    void OnMove(InputValue value)
    {
        if (isAlive)
        {
            moveInput = value.Get<Vector2>();
            FlipSprite();
        }
    }

    void OnJump(InputValue value)
    {
        if (isAlive)
        {
            if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                rb2d.velocity += new Vector2(0f, jumpSpeed);
            }
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

    void Die()
    {
        if (bodyColldier.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            isAlive = false;
            animator.SetTrigger("isDead");
            GetComponent<SpriteRenderer>().color = Color.red;
            rb2d.velocity = deathKick;
            Invoke("PlayerLostLife", 2);
        }
    }

    void PlayerLostLife()
    {
        FindObjectOfType<LevelManager>().ProcessPlayerDeath();
    }

    void OnFire(InputValue value)
    {
        if (isAlive && canFire)
        {
            canFire = false;
            animator.SetTrigger("isShooting");
            Invoke("FireArrow", 0.3f);
        }
    }

    void FireArrow()
    {
        Instantiate(arrow, bow.position, transform.rotation);
        Invoke("ResetFire", 0.3f);
    }

    void ResetFire()
    {
        canFire = true;
    }
}
