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

    public abstract void SkillNum1();
    public abstract void SkillNum2();

    protected Collider2D[] CheckRange(Vector2 size, float distance)
    {
        Vector2 forward = player.transform.right;
        Vector2 center = (Vector2)player.transform.position + forward * distance;
        return Physics2D.OverlapBoxAll(center, size, 0f);
    }

    protected void MovePlayer(Vector2 direction, float distance)
    {
        player.transform.position += (Vector3)(direction.normalized * distance);
    }



#if UNITY_EDITOR
    protected void DebugDrawBox(Vector2 boxSize, float distance, Color color, float duration = 0.1f)
    {
        Vector2 center = (Vector2)player.transform.position + Vector2.right * distance;

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
