using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NewControls : MonoBehaviour
{
    [Header("D�j� d�fini automatiquement. Changer si �a ne fonctionne pas.")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] SpriteRenderer sprite_renderer;

    [Header("isGrounded?! :( faut pas gronder")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Movement and speed")]
    private float horizontal;
    private float speed = 8f;
    private float jumpForce = 10f;
    private bool isFacingRight = true;
    [SerializeField] bool EnSaut = false;
    [SerializeField] bool canMove = true;
    Vector2 move;

    [Header("Dashing proprieties")]
    [SerializeField] bool canDash = true;
    public bool isDashing;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashingTime = 0.4f;
    [SerializeField] float dashingCooldown = 1f;
    [SerializeField] float tm;
    private IEnumerator coroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void Update()
    {
        if (isFacingRight && horizontal > 0.01f)
        {
            Flip();
        }
        else if (!isFacingRight && horizontal < 0.01f)
        {
            Flip();
        }
        else if (isDashing)
        {
            return;
        }
        if (Input.GetKey(KeyCode.LeftShift) && canDash == true)
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && EnSaut == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            EnSaut = true;
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D SolDetection1)                 // ============== JUMP : GROUND DETECTION
    {
        EnSaut = false;
        canDash = true;
    }
    private void ExitTriggerEnter2D(Collider2D SolDetection1)
    {
        EnSaut = true;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (isFacingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            sprite_renderer.flipX = true;
        }
        else if (!isFacingRight)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            sprite_renderer.flipX = false;
        }
        horizontal = context.ReadValue<Vector2>().x;
    }

    IEnumerator Dash()                                                      // ============== DASHING : COROUTINE
    {
        canDash = false;
        isDashing = true;
        tm = Time.time;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if (sprite_renderer.flipX == true)
        {
            rb.velocity = Vector2.left * dashSpeed;
        }
        else
        {
            rb.velocity = Vector2.right * dashSpeed;
        }

        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        rb.velocity = new Vector2(transform.localScale.x * 0, 0f);
        yield return new WaitForSeconds(dashingCooldown);
        if (EnSaut == false)
        {
            canDash = true;
        }
    }
}
