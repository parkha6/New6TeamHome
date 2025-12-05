using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkinEnhance")]
public class SkinEnhanceData : ScriptableObject
{
    public string enhanceName;
    public string description;

    public SkinType skinType;
    public int maxLevel;
    [Header("현재 강화 레벨 (0 ~ maxLevel)")]
    public int currentLevel;

    public EvolutionCost[] costs;
    public float[] enhanceValues;
}
