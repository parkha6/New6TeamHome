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
    public float dashDuration;
    public float dashCooldown;
    private Vector2 moveInput;
    public LayerMask groundLayerMask;
    private bool isGrounded;
    private bool isDashing = false;
    private bool canDash;
    public float facingDirection = 1.0f; // 바라보는 방향 디폴트는 1f(오른쪽)
    private float originalGravityScale; // 원래 중력값 저장할 변수
    private Collider2D playerCollider;
    [SerializeField] SpriteRenderer spriteRenderer;


    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        canDash = true;
        originalGravityScale = rb.gravityScale;
        playerCollider = GetComponent<Collider2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // 입력 값이 Vector2 형태이므로 ReadValue<Vector2>()를 사용해 moveInput에 할당
        if (moveInput.x != 0)
        {
            facingDirection = Mathf.Sign(moveInput.x); // moveInput.x > 0 이면 1.0f(우), 아니면 -1.0f(좌) 저장  mathf.sign = 값을 받아 양수면 1.0, 음수면 -1.0을 저장함. 마지막으로 입력된 방향값을 변수에 저장하기 위함
            spriteRenderer.flipX = (facingDirection < 0);
        }
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
        if (context.started && !isDashing && canDash)
        {
            Debug.Log("대시 시작");
            StartCoroutine(DashCorutine());
            
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

    //public void Dash()
    //{
    //    float dashDirectionX = moveInput.x != 0 ? moveInput.x : 1f; // 이동중이면(moveinput.x값이 있으면 그대로 사용, 없으면 1f(오른쪽) 사용하여 대시 방향을 결정

    //    Vector2 dashDirection = new Vector2(dashDirectionX, 0f);

    //    rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))  // 태그 Ground 로
        {
            //Vector2 vector2 = rb.velocity;   // ground태그 + 착지순간 y값 0이면 true
            //if (vector2.y == 0)
            //{
                
            //}
            isGrounded = true;
        }
    }

    private IEnumerator DashCorutine()
    {
        isDashing = true;
        canDash = false;
        if (playerCollider != null) // 기획의도대로 콜라이더 비활성화하여 회피기능 추가
        {
            playerCollider.enabled = false;
        }
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero; // 대시 작동 시 순간적으로 플레이어에게 가해지는 물리력 0으로 만들어 온전히 대시만 기능하게 함

        float dashDirectionX = moveInput.x != 0 ? moveInput.x : 1f; // 이동중이면(moveinput.x값이 있으면 그대로 사용, 없으면 1f(오른쪽) 사용하여 대시 방향을 결정

        Vector2 dashDirection = new Vector2(facingDirection, 0f);

        rb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        if (playerCollider != null)
        {
            playerCollider.enabled = true;
        }
        rb.gravityScale = originalGravityScale; // 원래대로 중력 되돌려줌.
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        Debug.Log("대시 사용 가능");
    }

    private void OnEnable() // 오브젝트가 꺼졌을 때 버그 방지용 코드. 초기화 해야하는 변수들 초기화 시켜준다.
    {
        isDashing = false;
        canDash = true;
        StopAllCoroutines();
    }
}
