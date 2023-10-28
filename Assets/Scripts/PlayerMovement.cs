using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private                  float horizontal;
    private                  float vertical;
    private                  bool  touchRope;
    private                  bool  isFacingRight = true;

    [SerializeField] private Animator    playerAnimator;
    [SerializeField] private float       speed         = 8f;
    [SerializeField] private float       jumpingPower  = 16f;
    [SerializeField] private float       climbingSpeed = 8f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform   groundCheck;
    [SerializeField] private Transform   groundCheck2;
    [SerializeField] private Transform   rightWallCheck;
    [SerializeField] private Transform   leftWallCheck;
    [SerializeField] private LayerMask   groundLayer;
    [SerializeField] private LayerMask   ropeLayer;

    public                  Transform cameraTarget;
    private static readonly int       speed1 = Animator.StringToHash("Speed");


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        touchRope = TouchRope();

        // Jump
        if (Input.GetButtonDown("Jump") && (IsGrounded() || touchRope))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && TouchWall())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f) // en rajoutant (&& !TouchRope()) on ne descend pas quand on touche la corde 
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        // Grimper à la corde
        if (vertical > 0f && touchRope)
        {
            rb.velocity = new Vector2(rb.velocity.x, climbingSpeed);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        playerAnimator.SetFloat(speed1, Mathf.Abs(rb.velocity.x));
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer) || Physics2D.OverlapCircle(groundCheck2.position, 0.5f, groundLayer);
    }

    private bool TouchWall()
    {
        return Physics2D.OverlapCircle(rightWallCheck.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(leftWallCheck.position, 0.2f, groundLayer);
    }

    private bool TouchRope()
    {
        return Physics2D.OverlapCircle(transform.position, Math.Abs(transform.localScale.x/2), ropeLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
