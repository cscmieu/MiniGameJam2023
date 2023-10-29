using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _horizontal;
    private float _vertical;
    private bool  _touchRope;
    private bool  _touchWall;
    private bool  _touchFloor;
    private bool  _isFacingRight = true;
    public bool _inputDisabled;
    private bool _isHit;
    private bool _isStunned;
    private float _elapsedTime;
    
    [SerializeField] private float bumpInputDisabledDuration = 0.3f;
    [SerializeField] private float hitInputDisabledDuration = 0.4f;
    [SerializeField] private float stunInputDisabledDuration = 0.8f;
    [SerializeField] private Vector2     knockBackStr = new(10f, 10f);
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
    
    public                  GameObject  lamp;
    public                  Animator    playerAnimator;
    public                  Rigidbody2D rb;
    public                  bool        inCinematic;
    public                  Transform   cameraTarget;
    private static readonly int         speed           = Animator.StringToHash("Speed");
    private static readonly int         isTouchingWall  = Animator.StringToHash("isTouchingWall");
    private static readonly int         airSpeed        = Animator.StringToHash("AirSpeed");
    private static readonly int         isTouchingFloor = Animator.StringToHash("isTouchingFloor");
    private static readonly int         isClimbingRope  = Animator.StringToHash("isClimbingRope");
    private static readonly int         isTouchingRope  = Animator.StringToHash("isTouchingRope");
    private static readonly int         isHurt          = Animator.StringToHash("isHurt");
    private static readonly int         isStunned       = Animator.StringToHash("isStunned");


    private void Update()
    {
        if (inCinematic)
        {
            return;
        }
    
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical   = Input.GetAxisRaw("Vertical");
        _touchRope  = TouchRope();
        _touchWall  = TouchWall();
        _touchFloor = IsGrounded();
        playerAnimator.SetBool(isTouchingWall,  _touchWall);
        playerAnimator.SetBool(isTouchingFloor, _touchFloor);
        playerAnimator.SetBool(isTouchingRope, _touchRope);
        playerAnimator.SetFloat(airSpeed, rb.velocity.y);
        playerAnimator.SetFloat(speed, Mathf.Abs(rb.velocity.x));
        playerAnimator.SetBool(isStunned, _isStunned);
        playerAnimator.SetBool(isHurt, _isHit);
        // Jump
        var ySpeed = EffectManager.SlownessTriggered ?  jumpingPower * jumpPowerMultiplier : jumpingPower;
        if (!_inputDisabled)
        {
            if (Input.GetButtonDown("Jump") && (_touchFloor || _touchRope))
            {
                AudioManager.Instance.PlaySFX("Jump");
                rb.velocity = new Vector2(rb.velocity.x, ySpeed);
            }

            if (Input.GetButtonDown("Jump") && _touchWall)
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
        if (_vertical > 0f && _touchRope)
        {
            lamp.transform.localRotation = Quaternion.Euler(0,0,0);
            rb.position = new Vector2(5.5f,          rb.position.y);
            var velocity = rb.velocity;
            velocity    = new Vector2(velocity.x, climbingSpeed);
            rb.velocity = velocity;
            playerAnimator.SetFloat(isClimbingRope, velocity.y);
        }
        else
        {
            lamp.transform.localRotation = Quaternion.Euler(0,0,-90);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        if (inCinematic) return;
        float knockBackDirection = EffectManager.KnockBackToTheRight ?  1 : -1;
        if (!_inputDisabled)
        {
            rb.velocity = new Vector2(_horizontal * moveSpeed, rb.velocity.y);
        }
        
        if (EffectManager.KnockBackTriggered)
        {
            _isHit = true;
            _inputDisabled = true;
            playerAnimator.SetBool(isHurt, true);
            EffectManager.KnockBackTriggered = false;
            rb.velocity = new Vector2(knockBackDirection * knockBackStr.x, knockBackStr.y);

            ScoreManager.malus += 20;
        }

        if (EffectManager.StunTriggered)
        {
            _isStunned = true;
            _inputDisabled = true;
            EffectManager.StunTriggered = false;
            rb.velocity = new Vector2(0, 0);
        }
        
        if (EffectManager.BumpTriggered)
        {
            _isStunned = true;
            _inputDisabled = true;
            EffectManager.BumpTriggered = false;
        }
        
        if (_inputDisabled)
        {
            _elapsedTime += Time.deltaTime;
        }

        if (_elapsedTime > hitInputDisabledDuration)
        {
            _isHit = false;
            playerAnimator.SetBool(isHurt, false);
        }

        if (_elapsedTime > stunInputDisabledDuration)
        {
            _isStunned = false;
            playerAnimator.SetBool(isStunned, false);
        }

        if (_elapsedTime > bumpInputDisabledDuration)
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
            var velocity = rb.velocity;
            velocity    = new Vector2(velocity.x / 2, velocity.y);
            rb.velocity = velocity;
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
        var transform1 = transform;
        return Physics2D.OverlapCircle(transform1.position, Math.Abs(transform1.localScale.x/2), ropeLayer);
    }

    private void Flip()
    {
        if (((!_isFacingRight || !(_horizontal < 0f)) && (_isFacingRight || !(_horizontal > 0f))) || _inputDisabled) return;
        
        _isFacingRight = !_isFacingRight;
        var transform1 = transform;
        var localScale = transform1.localScale;
        localScale.x          *= -1f;
        transform1.localScale =  localScale;
    }
}
