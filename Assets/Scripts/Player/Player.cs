using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D rb;
    private Collider2D _collider;



    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7.5f;
    private float moveInput;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float jumpTime = 0.5f;

    [Header("TurnCheck")]
    [HideInInspector] public bool _isFacingRight;

    private bool _isJumping;
    private bool _isFalling;
    private float jumpTimeCounter;

    [Header("GroundCheck")]
    private RaycastHit2D groundHit;
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask whatIsGround;
    

    private void Awake()
    {
        _isFacingRight = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Jump()
    {
        //button just pushed
        if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded())
        {
            _isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _animator.SetTrigger("Jump");
        }

        //button is pressed
        if (UserInput.instance.controls.Jumping.Jump.IsPressed())
        {
            if (jumpTimeCounter > 0 && _isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter == 0)
            {
                _isFalling = true;
                _isJumping = false;
            }
            else
            {
                _isJumping = false;
            }
        }

        if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame())
        {
            //button released
            _isJumping = false;
            _isFalling = true;
        }

        if (!_isJumping && CheckForLand())
        {
            _animator.SetTrigger("Land");
        }
    }

    #region Movement Functions
    private void Move()
    {
        TurnCheck();
        if(moveInput > 0 || moveInput < 0)
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }
        moveInput = UserInput.instance.moveInput.x;
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
    #endregion

    #region TurnCheck
    private void TurnCheck()
    {
        if (_isFacingRight && UserInput.instance.moveInput.x < 0)
        {
            Turn(false);
        }
        else if (!_isFacingRight && UserInput.instance.moveInput.x > 0)
        {
            Turn(true);
        }
    }

    private void Turn(bool turnRight)
    {
        if (turnRight)
        {
            _isFacingRight = true;
            transform.Rotate(0f, -180f, 0f);
        }
        else
        {
            _isFacingRight = false;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    #endregion

    #region GroundCheck

    private bool IsGrounded()
    {
        groundHit = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);

        if (groundHit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion

    #region LandedCheck
    private bool CheckForLand()
    {
        if (_isFalling)
        {
            if (IsGrounded())
            {
                //Player has landed
                _isFalling = false;

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    #endregion
}
