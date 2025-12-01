using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CockroachSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(1.1f, 1f);
    public float skill1Distance = 1.1f;

    [Header("Skill 2 Settings")]
    public Vector2 skill2BoxSize = new Vector2(2.5f, 2.5f);
    public float skill2Distance = 2.5f;

    void Update() // Test
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SkillNum1();
        }
    }

    public override void SkillNum1()
    {
        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance);

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
        
    }
}
