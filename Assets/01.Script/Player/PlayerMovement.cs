using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public float dashForce;
    private Vector2 moveInput;
    public LayerMask groundLayerMask;
    private bool isGrounded;
    private bool isDashing = false;


    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // 입력 값이 Vector2 형태이므로 ReadValue<Vector2>()를 사용해 moveInput에 할당
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && isGrounded)
        {
            Jump();
        }
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && !isDashing)
        {
            Dash();
            Debug.Log("대시 시작");
        }
    }


    private void FixedUpdate()
    {
        Move();
    }


    public void Move()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    public void Dash()
    {
        isDashing = true;
        float dashDirectionX = moveInput.x != 0 ? moveInput.x : 1f; // 이동중이면(moveinput.x값이 있으면 그대로 사용, 없으면 1f(오른쪽) 사용하여 대시 방향을 결정

        Vector2 dashDirection = new Vector2(dashDirectionX, 0f);

        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 태그 Ground 로
        {
            Vector2 vector2 = rb.velocity;   // ground태그 + 착지순간 y값 0이면 true
            if (vector2.y == 0)
            {
                isGrounded = true;
            }
        }
    }
}
