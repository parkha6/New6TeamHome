using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolveNPC : MonoBehaviour
{
    public CurrencyWallet wallet;
    public PermanentStats permanentStats;

    public EvolutionUpgradeData upgradeData;

    public void TryEvolve()
    {
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
            permanentStats.IncreaseMaxHealthLevel();
        else if (upgradeData.statType == EvolutionStatType.Attack)
            permanentStats.IncreaseAttackLevel();
        else if (upgradeData.statType == EvolutionStatType.Defense)
            permanentStats.IncreaseDefenseLevel();
        else if (upgradeData.statType == EvolutionStatType.MoveSpeed)
            permanentStats.IncreaseSpeedLevel();
    }

    }


    
