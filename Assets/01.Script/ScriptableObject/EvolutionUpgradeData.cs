
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EvolutionUpgrade")]
public class EvolutionUpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;

    public EvolutionStatType statType;
    public int maxLevel;

    public ItemData costItem;  // 어떤 재화로
    public int costPerLevel;   // 레벨당 비용
}

