using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public abstract class BaseSkill : MonoBehaviour
{
    [Header("플레이어")]
    protected GameObject player;

    public abstract void SkillNum1();
    public abstract void SkillNum2();

    protected Collider2D[] CheckRange(Vector2 Size, float distance)
    {
        Vector2 facing = GetFacing();
        Vector2 center = (Vector2)player.transform.position + facing * distance;

        return Physics2D.OverlapBoxAll(center, Size, 0f);
    }

    protected void MovePlayer(Vector2 dir, float dist)
    {
        player.transform.position += (Vector3)(dir.normalized * dist);
    }

    protected Vector2 GetFacing()
    {
        return player.transform.localScale.x > 0 ? Vector2.right : Vector2.left;
    }


#if UNITY_EDITOR
    protected void DebugDrawBox(Vector2 boxSize, float distance, Color color, float duration = 0.1f)
    {
        Vector2 facing = GetFacing();
        Vector2 center = (Vector2)player.transform.position + facing * distance;

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
