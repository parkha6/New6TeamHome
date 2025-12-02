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

    //protected BoxCollider2D playerCollider;
    //protected float originalGravityScale; // 원래 중력값 저장할 변수
    //protected Rigidbody2D rb;

    public abstract void SkillNum1();
    public abstract void SkillNum2();
    //private void Awake()
    //{
    //    player = this.gameObject;
    //    if (player == null)
    //        player = this.gameObject;

    //    rb = player.GetComponent<Rigidbody2D>();
    //    playerCollider = player.GetComponent<BoxCollider2D>();

    //    if (rb != null)
    //        originalGravityScale = rb.gravityScale;
    //}

    protected Collider2D[] CheckRange(Vector2 size, float distance, float height)
    {
        Vector2 center = skillOrigin + Vector2.right * distance + Vector2.up * height;
        return Physics2D.OverlapBoxAll(center, size, 0f);
    }
    protected void CaptureSkillOrigin() // 스킬 사용전 위치 체크
    {
        skillOrigin = player.transform.position;
    }

    protected IEnumerator Move(Vector2 direction, float distance, float duration) // 돌진
    {
        Vector2 start = player.transform.position;
        Vector2 end = start + direction.normalized * distance;

        //rb.gravityScale = 0f;
        //rb.velocity = Vector2.zero;

        float t = 0;
        while (t < duration)
        {
            //if (playerCollider != null)
            //{
            //    playerCollider.enabled = false;
            //}
            t += Time.deltaTime;
            player.transform.position = Vector2.Lerp(start, end, t / duration);
            yield return null;
        }
        player.transform.position = end;

        //if (playerCollider != null)
        //{
        //    playerCollider.enabled = true;
        //}
        //rb.gravityScale = originalGravityScale;
    }
    protected IEnumerator Jump(Vector2 direction, float distance, float height, float duration) // 점프
    {
        Vector2 start = player.transform.position; // 시작 위치
        Vector2 peak = start + new Vector2(direction.x * distance / 2f, height); // 대각 점프
        Vector2 end = start + new Vector2(direction.x * distance, 0f); // 대각 하강

        float riseDuration = duration * 0.70f; // 상승 속도
        float fallDuration = duration * 0.30f; // 하강 속도

        // 상승
        float t = 0f;
        while (t < riseDuration)
        {
            t += Time.deltaTime;
            float normalizedT = Mathf.Clamp01(t / riseDuration);
            float x = Mathf.Lerp(start.x, peak.x, normalizedT);
            float y = Mathf.Lerp(start.y, peak.y, normalizedT);
            player.transform.position = new Vector2(x, y);
            yield return null;
        }

        // 하강
        t = 0f;
        while (t < fallDuration)
        {
            t += Time.deltaTime;
            float normalizedT = Mathf.Clamp01(t / fallDuration);
            float x = Mathf.Lerp(peak.x, end.x, normalizedT);
            float y = Mathf.Lerp(peak.y, end.y, normalizedT);
            player.transform.position = new Vector2(x, y);
            yield return null;
        }

        player.transform.position = end; // 최종 위치 보정
    }

#if UNITY_EDITOR
    protected void DebugDrawBox(Vector2 origin, Vector2 boxSize, float distance, Color color, float height, float duration = 0.1f)
    {
        Vector2 center = origin + Vector2.right * distance + Vector2.up * height;

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
