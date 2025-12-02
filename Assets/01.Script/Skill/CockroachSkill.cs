using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CockroachSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(1.1f, 1f);
    public float skill1Distance = 1.1f;
    public float skill1Height = 1f;
    public float skill1AttackDistance = 5;

    [Header("Skill 2 Settings")]
    public Vector2 skill2BoxSize = new Vector2(2.5f, 2.5f);
    public float skill2Distance = 2.5f;
    public float skill2Height = 1f;

    void Update() // Test
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SkillNum1();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SkillNum2();
        }
    }

    public override void SkillNum1()
    {

        CaptureSkillOrigin();
        StartCoroutine(Move(Vector2.right, 5f, 0.2f));

        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

#if UNITY_EDITOR
        DebugDrawBox(skillOrigin, skill1BoxSize, skill1Distance, Color.red, skill1Height, 0.2f);
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
        
    }
}
