using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private bool  touchRope;
    private bool  touchWall;
    private bool  touchFloor;
    private bool  isFacingRight = true;

    [SerializeField] private Animator    playerAnimator;
    [SerializeField] private float       moveSpeed         = 8f;
    [SerializeField] private float       jumpingPower  = 16f;
    [SerializeField] private float       climbingSpeed = 8f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform   groundCheck;
    [SerializeField] private Transform   groundCheck2;
    [SerializeField] private Transform   rightWallCheck;
    [SerializeField] private Transform   leftWallCheck;
    [SerializeField] private Transform   rightWallCheck2;
    [SerializeField] private Transform   leftWallCheck2;
    [SerializeField] private LayerMask   groundLayer;
    [SerializeField] private LayerMask   ropeLayer;

    public                  Transform cameraTarget;
    private static readonly int       speed          = Animator.StringToHash("Speed");
    private static readonly int       isTouchingWall  = Animator.StringToHash("isTouchingWall");
    private static readonly int       airSpeed        = Animator.StringToHash("AirSpeed");
    private static readonly int       isTouchingFloor = Animator.StringToHash("isTouchingFloor");


    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical   = Input.GetAxisRaw("Vertical");
        touchRope  = TouchRope();
        touchWall  = TouchWall();
        touchFloor = IsGrounded();
        playerAnimator.SetBool(isTouchingWall, touchWall);
        playerAnimator.SetBool(isTouchingFloor, touchFloor);
        playerAnimator.SetFloat(airSpeed, rb.velocity.y);
        playerAnimator.SetFloat(speed, Mathf.Abs(rb.velocity.x));

        // Jump
        if (Input.GetButtonDown("Jump") && (touchFloor || touchRope))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonDown("Jump") && touchWall)
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
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer) || Physics2D.OverlapCircle(groundCheck2.position, 0.5f, groundLayer);
    }

    private bool TouchWall()
    {
        return Physics2D.OverlapCircle(rightWallCheck.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(leftWallCheck.position, 0.2f, groundLayer)
            || Physics2D.OverlapCircle(rightWallCheck2.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(leftWallCheck2.position, 0.2f, groundLayer);
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
