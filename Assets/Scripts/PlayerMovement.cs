using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpingPower;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpBufferTime;

    public PlayerInputActions controls;
    public InputAction move;
    public InputAction jump;
    public InputAction fire;


    public LayerMask groundLayer;
    public Transform groundCheck;
    private Rigidbody2D rb;
    private Animator anim;

    private bool isFacingRight = true;
    private float horizontal;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        controls = new PlayerInputActions();
        
    }

    public void EnableControls()
    {
        move.Enable();
        jump.Enable();
        fire.Enable();
    }

    public void DisableControls()
    {
        move.Disable();
        jump.Disable();
        fire.Disable();
    }

    private void OnEnable()
    {
        move = controls.PlayerMovement.Move;
        move.Enable();

        jump = controls.PlayerMovement.Jump;
        jump.Enable();
        jump.performed += Jump;

        fire = controls.PlayerMovement.Fire;
        fire.Enable();
        fire.performed += FireAnimation;
    }

    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
        fire.Disable();
    }


    void Update()
    {
        horizontal = move.ReadValue<Vector2>().x;
        anim.SetFloat("horizontal", horizontal);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isGrounded", IsGrounded);

        if (horizontal != 0 && IsGrounded)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        Flip();
    }


    private void FixedUpdate()
    {
        ApplyMovement();
    }
    void ApplyMovement()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;

            transform.Rotate(0, 180, 0);
        }
    }

    public void FireAnimation(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetTrigger("FireWeapon");
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && IsGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if(context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}
