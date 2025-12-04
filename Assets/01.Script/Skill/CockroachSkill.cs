using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CockroachSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(3f, 1f);
    public float skill1Distance = 0f;
    public float skill1Height = 0.5f;
    public float skill1AttackDistance = 3;
    private float totalAtk1;

    [Header("SkillEnhance Data")]
    public SkinEnhanceData enhanceData;

    public float invincibility;
    protected override void Awake()
    {
        base.Awake();
        player = this.gameObject;
        enhanceData = PlayerSkillController.Instance.enhanceCoData;
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
            totalAtk1 = playerAtk * 1.7f;
        }
        float currentFacingDirection = playerMovement.facingDirection;
        Vector2 attackDirection = new Vector2(currentFacingDirection, 0);
        CaptureSkillOrigin();
        StartCoroutine(Move(attackDirection, skill1AttackDistance, 0.2f));

        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

        SpawnHitBoxEffect(skillOrigin, skill1BoxSize, skill1Distance, skill1Height, 0.3f);

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.TakePhisicalDamage(totalAtk1);
                Debug.Log("Cockroach Skill 1 Attack");
            }
        }
    }
    public override void SkillNum2()
    {
        if (PlayerManager.Instance.isInvincible) return;
        PlayerManager.Instance.invincibilityDuration = invincibility;
        invincibility = 5f;
        PlayerManager.Instance.Invincibility();
        Debug.Log($"{invincibility}초");
    }
}
