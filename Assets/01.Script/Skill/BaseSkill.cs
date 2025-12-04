using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;
using static UnityEditor.Experimental.GraphView.GraphView;

public abstract class BaseSkill : MonoBehaviour
{
    [Header("Player")]
    [SerializeField]protected GameObject player;
    [SerializeField]protected Vector2 skillOrigin;
    

    protected BoxCollider2D playerCollider;
    protected float originalGravityScale; // 원래 중력값 저장할 변수
    protected Rigidbody2D rb;
    protected PlayerMovement playerMovement;
    public int playerLayer = 3;
    public int enemyLayer = 6;

    public abstract void SkillNum1();
    public abstract void SkillNum2();
    protected virtual void Awake()
    {
        player = this.gameObject;
        if (player == null)
            player = this.gameObject;

        rb = player.GetComponent<Rigidbody2D>();
        playerCollider = player.GetComponent<BoxCollider2D>();

        if (rb != null)
            originalGravityScale = rb.gravityScale;
        playerMovement = GetComponent<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement를 찾을 수 없습니다");
        }
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    protected Collider2D[] CheckRange(Vector2 size, float distance, float height)
    {
        Vector2 center = skillOrigin + Vector2.zero * distance + Vector2.up * height;
        return Physics2D.OverlapBoxAll(center, size, 0f);
    }

    protected void CaptureSkillOrigin() // 스킬 사용전 위치 체크
    {
        skillOrigin = player.transform.position;
    }

    protected IEnumerator Move(Vector2 direction, float distance, float duration) // 돌진
    {
        if (player == null || rb == null)
        {
            Debug.Log("player가 null입니다");
            yield break;
        }

        Vector2 start = rb.position;
        Vector2 end = start + direction.normalized * distance;

        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, true);
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;

            // 💡 transform.position이 아닌 Rigidbody 이동
            Vector2 next = Vector2.Lerp(start, end, t / duration);
            rb.MovePosition(next); // ≤ 이건 물리 충돌 체크함

            yield return null;
        }

        rb.MovePosition(end);

        rb.gravityScale = originalGravityScale;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, false);
    }


    protected IEnumerator Jump(Vector2 direction, float distance, float height, float duration) // 점프
    {
        if (player == null || rb == null)
        {
            Debug.Log("player 또는 rb가 null입니다.");
            yield break;
        }

        Vector2 start = rb.position;
        Vector2 peak = start + new Vector2(direction.x * distance / 2f, height);
        Vector2 end = start + new Vector2(direction.x * distance, 0f);

        float riseDuration = duration * 0.70f;
        float fallDuration = duration * 0.30f;

        rb.gravityScale = 0f;      // 중력 끔
        rb.velocity = Vector2.zero; // 속도 초기화

        // --- 상승 ---
        float t = 0f;
        while (t < riseDuration)
        {
            t += Time.deltaTime;
            float normalizedT = Mathf.Clamp01(t / riseDuration);

            float x = Mathf.Lerp(start.x, peak.x, normalizedT);
            float y = Mathf.Lerp(start.y, peak.y, normalizedT);

            rb.MovePosition(new Vector2(x, y));  // 물리 기반 이동
            yield return null;      // 물리 프레임 대기
        }

        // --- 하강 ---
        t = 0f;
        while (t < fallDuration)
        {
            t += Time.deltaTime;
            float normalizedT = Mathf.Clamp01(t / fallDuration);

            float x = Mathf.Lerp(peak.x, end.x, normalizedT);
            float y = Mathf.Lerp(peak.y, end.y, normalizedT);

            rb.MovePosition(new Vector2(x, y));  // 물리 기반 이동
            yield return null;
        }

        rb.MovePosition(end);       // 최종 위치 보정
        rb.gravityScale = originalGravityScale; // 중력 복구
    }


    protected void SpawnHitBoxEffect(Vector2 origin, Vector2 size, float distance, float height, float time)
    {
        float facing = 1f;
        if (playerMovement != null)
            facing = playerMovement.facingDirection;

        // DebugDrawBox에서 쓰던 계산 그대로!
        Vector2 center = origin + Vector2.right * distance * facing + Vector2.up * height;

        GameObject go = Instantiate(PlayerSkillController.Instance.hitBoxData.hitBoxPrefab, center, Quaternion.identity);
        go.GetComponent<HitBoxEffect>().Init(size, time);
    }



#if UNITY_EDITOR
    protected void DebugDrawBox(Vector2 origin, Vector2 boxSize, float distance, Color color, float height, float duration = 0.1f)
    {
        float facing = 1f;
        if (playerMovement != null) facing = playerMovement.facingDirection;

        Vector2 center = origin + Vector2.right * distance * facing + Vector2.up * height;

        Vector2 half = boxSize / 2f;

        Vector2 tl = center + new Vector2(-half.x, half.y);
        Vector2 tr = center + new Vector2(half.x, half.y);
        Vector2 bl = center + new Vector2(-half.x, -half.y);
        Vector2 br = center + new Vector2(half.x, -half.y);

        Debug.DrawLine(tl, tr, color, duration);
        Debug.DrawLine(tr, br, color, duration);
        Debug.DrawLine(br, bl, color, duration);
        Debug.DrawLine(bl, tl, color, duration);
    }
#endif
}
