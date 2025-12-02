
using UnityEngine;

[System.Serializable]
public class EvolutionCost
{
    public ItemData item;  // 어떤 재화인지
    public int amount;     // 몇 개 쓰는지
}

[CreateAssetMenu(menuName = "Data/EvolutionUpgrade")]
public class EvolutionUpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;

    public EvolutionStatType statType;
    public int maxLevel;

    public EvolutionCost[] costs;
}

