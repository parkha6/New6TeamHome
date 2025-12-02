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

        // TODO: 여기서 permanentStats 안의 해당 능력치 레벨을 올려주기
        if (!wallet.TrySpendCurrency(upgradeData.costItem, upgradeData.costPerLevel))
        {
            Debug.Log("재화 부족");
            return;
        }
        else
        {
            if (upgradeData.statType == EvolutionStatType.MaxHP)
            {
                permanentStats.IncreaseMaxHealthLevel();
            }
            else if (upgradeData.statType == EvolutionStatType.Attack)
            {
                permanentStats.IncreaseAttackLevel();
            }
            else if (upgradeData.statType == EvolutionStatType.Defense)
            {
                permanentStats.IncreaseDefenseLevel();
            }
            else if (upgradeData.statType == EvolutionStatType.MoveSpeed)
            {
                permanentStats.IncreaseSpeedLevel();

            }
        }
    }
}
