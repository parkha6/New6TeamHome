using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/SkinEnhance")]
public class SkinEnhanceData : ScriptableObject
{
    public string enhanceName;
    public string description;

    public SkinType skinType;
    public int maxlevel;

    public EvolutionCost[] costs;
}
