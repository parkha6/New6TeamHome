using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvolveNPC : MonoBehaviour, IInteractable
{
    public CurrencyWallet wallet;
    public PermanentStats permanentStats;
    public EvolutionUpgradeData[] upgrades;
    public Button evolutionButton;

    public void OnInteraction()
    {
        throw new System.NotImplementedException();
    }

    public void TryEvolveByIndex(int index)
    {
        EvolutionUpgradeData upgradeData = upgrades[index];
        // 여기 안에서 기존 TryEvolve 로직을 이 upgradeData 기준으로 돌릴 예정

        int currentLevel = 0;

        // 1) 현재 레벨 가져오기
        if (upgradeData.statType == EvolutionStatType.MaxHP)
            currentLevel = permanentStats.maxHpLevel;
        else if (upgradeData.statType == EvolutionStatType.Attack)
            currentLevel = permanentStats.attackLevel;
        else if (upgradeData.statType == EvolutionStatType.Defense)
            currentLevel = permanentStats.defenseLevel;
        else if (upgradeData.statType == EvolutionStatType.MoveSpeed)
            currentLevel = permanentStats.moveSpeedLevel;

        // 2) 최대 레벨이면 진화 불가
        if (currentLevel >= upgradeData.maxLevel)
        {
            Debug.Log("이미 최대 레벨입니다.");
            return;
        }

        // 3) 모든 비용 충분한지 먼저 확인
        foreach (EvolutionCost cost in upgradeData.costs)
        {
            if (!wallet.HasCurrency(cost.item, cost.amount))
            {
                Debug.Log("재화 부족");
                return;
            }
        }

        // 4) 이제 실제로 비용 차감
        foreach (EvolutionCost cost in upgradeData.costs)
        {
            wallet.TrySpendCurrency(cost.item, cost.amount);
        }

        // 5) 마지막으로 레벨 업
        if (upgradeData.statType == EvolutionStatType.MaxHP)
            permanentStats.maxHpLevel++;
        else if (upgradeData.statType == EvolutionStatType.Attack)
            permanentStats.attackLevel++;
        else if (upgradeData.statType == EvolutionStatType.Defense)
            permanentStats.defenseLevel++;
        else if (upgradeData.statType == EvolutionStatType.MoveSpeed)
            permanentStats.moveSpeedLevel++;
    }
}


    
