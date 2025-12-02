using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public float attackDamage; // 추후 계산합시다.
    public Vector2 attackBoxSize = new Vector2(1f, 1f); // 공격 판정 박스 크기
    public float attackDistance = 0.5f; // 사거리
    public LayerMask enemyLayer;
    private float facingDirection; // playermovement에서 가져와서 사용
    private Rigidbody2D rb;
    private PlayerMovement movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (movement != null)
        {
            facingDirection = movement.facingDirection;
        }
    }

    public void OnNomalAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("일반공격 시도");
            NomalAttack();
        }
    }

    public void NomalAttack()
    {
        Vector2 origin = rb.position; 
        Vector2 offset = new Vector2(attackDistance * facingDirection, 0f); // 전방
        Vector2 point = origin + offset;
        Collider2D[] hits = Physics2D.OverlapBoxAll(point, attackBoxSize, 0f, enemyLayer);
        DebugDrawBox(point, attackBoxSize, Color.red, 0.2f);
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy)) // 실제로 데미지를 주기 위해서 enemy(스크립트)컴퍼넌트가 있는지 확인하고 있으면 데미지로직 실행
            {
                Debug.Log("적에게 일반공격 적중"); // 추후 데미지 계산해서 넣자
            }
        }
    }

    private void DebugDrawBox(Vector2 center, Vector2 size, Color color, float duration)
    {
        // 디버그박스의 네 모서리 계산 스킬이랑 비슷하게
        Vector2 halfSize = size / 2;
        Vector2 p1 = center + new Vector2(halfSize.x, halfSize.y);
        Vector2 p2 = center + new Vector2(-halfSize.x, halfSize.y);
        Vector2 p3 = center + new Vector2(-halfSize.x, -halfSize.y);
        Vector2 p4 = center + new Vector2(halfSize.x, -halfSize.y);

        Debug.DrawLine(p1, p2, color, duration);
        Debug.DrawLine(p2, p3, color, duration);
        Debug.DrawLine(p3, p4, color, duration);
        Debug.DrawLine(p4, p1, color, duration);
    }
}
