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
    private bool _inputDisabled;
    private bool _isHit;
    private bool _isStunned;
    private float _elapsedTime;
    
    [SerializeField] private float hitInputDisabledDuration = 0.4f;
    [SerializeField] private float stunInputDisabledDuration = 0.8f;
    [SerializeField] private Vector2     knockBackStr = new(10f, 10f);
    [SerializeField] private Animator    playerAnimator;
    [SerializeField] private float       moveSpeed     = 8f;
    [SerializeField] private float       jumpingPower  = 16f;
    [SerializeField] private float       climbingSpeed = 8f;
    [SerializeField] private float       jumpPowerMultiplier = 0.5f; //applied when under slowness effect
    [SerializeField] private Transform   groundCheck;
    [SerializeField] private Transform   rightWallCheck;
    [SerializeField] private Transform   leftWallCheck;
    [SerializeField] private Transform   rightWallCheck2;
    [SerializeField] private Transform   leftWallCheck2;
    [SerializeField] private LayerMask   groundLayer;
    [SerializeField] private LayerMask   ropeLayer;

    public Rigidbody2D rb;
    public bool inCinematic;
    public                  Transform cameraTarget;
    private static readonly int       speed           = Animator.StringToHash("Speed");
    private static readonly int       isTouchingWall  = Animator.StringToHash("isTouchingWall");
    private static readonly int       airSpeed        = Animator.StringToHash("AirSpeed");
    private static readonly int       isTouchingFloor = Animator.StringToHash("isTouchingFloor");
    private static readonly int       isClimbingRope  = Animator.StringToHash("isClimbingRope");
    private static readonly int       isTouchingRope  = Animator.StringToHash("isTouchingRope");


    void Update()
    {
        if (inCinematic)
        {
            playerAnimator.SetBool(isTouchingRope, true);
            playerAnimator.SetFloat(isClimbingRope, 1);
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical   = Input.GetAxisRaw("Vertical");
        touchRope  = TouchRope();
        touchWall  = TouchWall();
        touchFloor = IsGrounded();
        playerAnimator.SetBool(isTouchingWall,  touchWall);
        playerAnimator.SetBool(isTouchingFloor, touchFloor);
        playerAnimator.SetBool(isTouchingRope, touchRope);
        playerAnimator.SetFloat(airSpeed, rb.velocity.y);
        playerAnimator.SetFloat(speed, Mathf.Abs(rb.velocity.x));

        // Jump
        float ySpeed = EffectManager.SlownessTriggered == true ?  jumpingPower * jumpPowerMultiplier : jumpingPower;
        if (!_inputDisabled)
        {
            if (Input.GetButtonDown("Jump") && (touchFloor || touchRope))
            {
                AudioManager.Instance.PlaySFX("Jump");
                rb.velocity = new Vector2(rb.velocity.x, ySpeed);
            }

            if (Input.GetButtonDown("Jump") && touchWall)
            {
                AudioManager.Instance.PlaySFX("WallJump");
                rb.velocity = new Vector2(rb.velocity.x, ySpeed);
            }

            if (Input.GetButtonUp("Jump") &&
                rb.velocity.y > 0f) // en rajoutant (&& !TouchRope()) on ne descend pas quand on touche la corde 
            {
                rb.velocity = new Vector2(rb.velocity.x, ySpeed * 0.5f);
            }
        }

        // Grimper à la corde
        if (vertical > 0f && touchRope)
        {
            rb.position = new Vector2(5.5f,          rb.position.y);
            rb.velocity = new Vector2(rb.velocity.x, climbingSpeed);
            playerAnimator.SetFloat(isClimbingRope, rb.velocity.y);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (inCinematic) return;
        float knockBackDirection = EffectManager.KnockBackToTheRight == true ?  1 : -1;
        if (!_inputDisabled)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
        
        if (EffectManager.KnockBackTriggered)
        {
            _isHit = true;
            _inputDisabled = true;
            EffectManager.KnockBackTriggered = false;
            rb.velocity = new Vector2(knockBackDirection * knockBackStr.x, knockBackStr.y);
        }

        if (EffectManager.StunTriggered)
        {
            _isStunned = true;
            _inputDisabled = true;
            EffectManager.StunTriggered = false;
            rb.velocity = new Vector2(0, 0);
        }
        
        if (_inputDisabled)
        {
            _elapsedTime += Time.deltaTime;
        }

        if (_elapsedTime > hitInputDisabledDuration)
        {
            _isHit = false;
        }

        if (_elapsedTime > stunInputDisabledDuration)
        {
            _isStunned = false;
        }

        if (!_isStunned && !_isHit && _inputDisabled)
        {
            _inputDisabled = false;
            _elapsedTime = 0;
        }
        
        if (EffectManager.SlownessTriggered)
        {
            rb.velocity = new Vector2(rb.velocity.x / 2, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool TouchWall()
    {
        return (Physics2D.OverlapCircle(rightWallCheck.position, 0.2f, groundLayer) 
               || Physics2D.OverlapCircle(leftWallCheck.position, 0.2f, groundLayer) 
               || Physics2D.OverlapCircle(rightWallCheck2.position, 0.2f, groundLayer) 
               || Physics2D.OverlapCircle(leftWallCheck2.position, 0.2f, groundLayer));
    }

    private bool TouchRope()
    {
        return Physics2D.OverlapCircle(transform.position, Math.Abs(transform.localScale.x/2), ropeLayer);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) && !_inputDisabled)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
