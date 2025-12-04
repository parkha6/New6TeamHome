using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(1f, 1f);
    public float skill1Distance = 1f;
    public float skill1Height = 0.5f;

    [Header("Skill 2 Settings")]
    public Vector2 skill2BoxSize = new Vector2(3.5f, 0.5f);
    public float skill2Distance = -0.1f;
    public float skill2Height = 0.5f;

    [Header("SkillEnhance Data")]
    public SkinEnhanceData enhanceData;
    

    protected override void Awake()
    {
        base.Awake();
        player = this.gameObject;
        enhanceData = PlayerSkillController.Instance.enhanceMaData;
    }

    void Update() // Test
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SkillNum1();
        }
        if (enhanceData.maxLevel >= 2)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                SkillNum2();
            }
        }
    }

    public override void SkillNum1()
    {
        CaptureSkillOrigin();
        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

        SpawnHitBoxEffect(skillOrigin, skill1BoxSize, skill1Distance, skill1Height, 0.3f);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                //TakeDamage
                Debug.Log("Mantis Skill 1 Attack");
            }
        }
    }

    public override void SkillNum2()
    {
        CaptureSkillOrigin();
        Collider2D[] hits = CheckRange(skill2BoxSize, skill2Distance, skill2Height);

        SpawnHitBoxEffect(skillOrigin, skill2BoxSize, skill2Distance, skill2Height, 0.3f);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                //TakeDamage
                Debug.Log("Mantis Skill 2 Attack");
            }
        }
    }
}
