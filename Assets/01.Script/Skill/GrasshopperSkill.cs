using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasshopperSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(2f, 0.5f);
    public float skill1Distance = 0.4f;
    public float skill1Height = 0f;

    [Header("Skill 2 Settings")]
    public Vector2 skill2BoxSize = new Vector2(0.5f, 1f);
    public float skill2Distance = 0.5f;
    public float skill2Height = 0.5f;
    private void Awake()
    {
        player = this.gameObject;
    }

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
        if (player.transform.position.y > 0.2f)
            return;
        StartCoroutine(SkillNum1Attack());
    }

    private IEnumerator SkillNum1Attack()
    {
        yield return StartCoroutine(Jump(Vector2.right, 2.5f, 4, 0.6f));

        CaptureSkillOrigin();
        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

#if UNITY_EDITOR
        DebugDrawBox(skillOrigin, skill1BoxSize, skill1Distance, Color.red, skill1Height, 0.5f);
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
        CaptureSkillOrigin();
        StartCoroutine(Move(-Vector2.right, 2f, 0.2f));

        Collider2D[] hits = CheckRange(skill2BoxSize, skill2Distance, skill2Height);

#if UNITY_EDITOR
        DebugDrawBox(skillOrigin, skill2BoxSize, skill2Distance, Color.red, skill2Height, 0.5f);
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
