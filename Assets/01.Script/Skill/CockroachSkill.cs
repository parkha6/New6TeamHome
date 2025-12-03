using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CockroachSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(3f, 3f);
    public float skill1Distance = 1f;
    public float skill1Height = 1f;
    public float skill1AttackDistance = 3;

    public float invincibility;
    protected override void Awake()
    {
        base.Awake();
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
        float currentFacingDirection = playerMovement.facingDirection;
        Vector2 attackDirection = new Vector2(currentFacingDirection, 0);
        CaptureSkillOrigin();
        StartCoroutine(Move(attackDirection, skill1AttackDistance, 0.2f));

        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

#if UNITY_EDITOR
        DebugDrawBox(skillOrigin, skill1BoxSize, skill1Distance, Color.red, skill1Height, 0.5f);
#endif
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                //TakeDamage
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
