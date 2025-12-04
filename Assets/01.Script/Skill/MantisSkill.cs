using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantisSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(2f, 1f);
    public float skill1Distance = 0f;
    public float skill1Height = 0.5f;
    private float totalAtk1;

    [Header("Skill 2 Settings")]
    public Vector2 skill2BoxSize = new Vector2(3.5f, 0.5f);
    public float skill2Distance = -0.1f;
    public float skill2Height = 0.5f;
    private float totalAtk2;

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
        float playerAtk = PlayerManager.Instance.TotalAttack();
        if (enhanceData.currentLevel >= 1)
        {
            totalAtk1 = playerAtk * 2f;
        }
        CaptureSkillOrigin();
        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

        SpawnHitBoxEffect(skillOrigin, skill1BoxSize, skill1Distance, skill1Height, 0.3f);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakePhisicalDamage(totalAtk1);
                Debug.Log("Mantis Skill 1 Attack");
            }
        }
    }

    public override void SkillNum2()
    {
        float playerAtk = PlayerManager.Instance.TotalAttack();
        if (enhanceData.currentLevel >= 1)
        {
            totalAtk2 = playerAtk * 2f;
        }
        CaptureSkillOrigin();
        Collider2D[] hits = CheckRange(skill2BoxSize, skill2Distance, skill2Height);

        SpawnHitBoxEffect(skillOrigin, skill2BoxSize, skill2Distance, skill2Height, 0.3f);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakePhisicalDamage(totalAtk2);
                Debug.Log("Mantis Skill 2 Attack");
            }
        }
    }
}
