using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrasshopperSkill : BaseSkill
{
    [Header("Skill 1 Settings")]
    public Vector2 skill1BoxSize = new Vector2(2f, 0.5f);
    public float skill1Distance = 0f;
    public float skill1Height = 0f;
    public Vector2 attackDirection;

    [Header("Skill 2 Settings")]
    public Vector2 skill2BoxSize = new Vector2(0.5f, 1f);
    public float skill2Distance = 0.5f;
    public float skill2Height = 0.5f;

    [Header("SkillEnhance Data")]
    public SkinEnhanceData enhanceData;
    protected override void Awake()
    {
        base.Awake();
        player = this.gameObject;
        enhanceData = PlayerSkillController.Instance.enhanceGrData;
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
        // 공중에서 시전 불가
        if (player.transform.position.y > 0.2f)
            return;

        // 좌우 판별 추가 (Cockroach 방식과 동일하게)
        float currentFacingDirection = playerMovement.facingDirection;
        attackDirection = new Vector2(currentFacingDirection, 0);

        

        StartCoroutine(SkillNum1Attack());
    }

    private IEnumerator SkillNum1Attack()
    {
        // 좌우 방향을 Jump에 적용
        yield return StartCoroutine(Jump(attackDirection, 2.5f, 4, 0.6f));

        CaptureSkillOrigin();

        Collider2D[] hits = CheckRange(skill1BoxSize, skill1Distance, skill1Height);

#if UNITY_EDITOR
        DebugDrawBox(skillOrigin, skill1BoxSize, skill1Distance, Color.red, skill1Height, 0.5f);
#endif

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                Debug.Log("Grasshopper Skill 1 Attack");
            }
        }
    }

    public override void SkillNum2()
    {
        float currentFacingDirection = playerMovement.facingDirection;
        Vector2 attackDirection = new Vector2(currentFacingDirection, 0);

        // 백스텝이니까 반대 방향으로 이동
        Vector2 backStepDirection = -attackDirection;

        CaptureSkillOrigin();
        StartCoroutine(Move(backStepDirection, 2f, 0.2f));

        Collider2D[] hits = CheckRange(skill2BoxSize, skill2Distance, skill2Height);

#if UNITY_EDITOR
        DebugDrawBox(skillOrigin, skill2BoxSize, skill2Distance, Color.red, skill2Height, 0.5f);
#endif
        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent<Enemy>(out Enemy enemy))
            {
                Debug.Log("Grasshopper Skill 2 Attack");
            }
        }
    }

}
