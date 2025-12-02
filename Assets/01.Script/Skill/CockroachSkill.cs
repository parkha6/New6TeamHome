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

        CaptureSkillOrigin();
        StartCoroutine(Move(Vector2.right, skill1AttackDistance, 0.2f));

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
        if (PlayerManager.Instance.isInvincible) return;
        PlayerManager.Instance.invincibilityDuration = invincibility;
        invincibility = 5f;
        PlayerManager.Instance.Invincibility();
        Debug.Log($"{invincibility}");
    }
}
