using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(2f, 2f);
    public float skill1Distance = 1f;
    public float skill1Height = 1f;

    [Header("Skill 2 Settings")]
    public Vector2 skill2BoxSize = new Vector2(3f, 0.5f);
    public float skill2Distance = 0f;
    public float skill2Height = 1.2f;
    public override void SkillNum1()
    {
        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

#if UNITY_EDITOR
        DebugDrawBox(skill1BoxSize, skill1Distance, Color.red, skill1Height, 0.2f);
#endif

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                //TakeDamage
                Debug.Log("Skill 1 Attack");
            }
        }
    }

    public override void SkillNum2()
    {
        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

#if UNITY_EDITOR
        DebugDrawBox(skill1BoxSize, skill1Distance, Color.red, skill1Height, 0.2f);
#endif

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                //TakeDamage
                Debug.Log("Skill 1 Attack");
            }
        }
    }
}
