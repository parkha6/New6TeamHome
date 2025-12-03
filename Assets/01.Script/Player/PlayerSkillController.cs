using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEditor.Build;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    public static PlayerSkillController Instance { get; private set; }

    private SkinType skinType;

    public SkinEnhanceData enhanceCoData;
    public SkinEnhanceData enhanceGrData;
    public SkinEnhanceData enhanceMaData;

    private void Awake()
    {
        // 같은 씬에서 중복 생성 방지
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        SetState(SkinType.Cockroach);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetState(SkinType.Cockroach);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetState(SkinType.Mantis);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetState(SkinType.Grasshopper);
    }
    public void SetState(SkinType state)
    {
        skinType = state;

        RemoveSkills();

        switch (skinType)
        {
            case SkinType.Cockroach:
                gameObject.AddComponent<CockroachSkill>();
                break;
            case SkinType.Mantis:
                gameObject.AddComponent<MantisSkill>();
                break;
            case SkinType.Grasshopper:
                gameObject.AddComponent<GrasshopperSkill>();
                break;
        }
    }
    void RemoveSkills()
    {
        var cockroachSkill = GetComponent<CockroachSkill>();
        if (cockroachSkill != null) Destroy(cockroachSkill);

        var mantisSkill = GetComponent<MantisSkill>();
        if (mantisSkill != null) Destroy(mantisSkill);

        var grasshopperSkill = GetComponent<GrasshopperSkill>();
        if (grasshopperSkill != null) Destroy(grasshopperSkill);
    }
}
