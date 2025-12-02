using UnityEngine;

public class EnhanceNPC : MonoBehaviour
{
    public CurrencyWallet wallet;          // 재화
    public SkinUpdateState skinState;      // 각 외피 강화 레벨
    public SkinEnhanceData[] enhances;     // Mantis / Grasshopper / Cockroach 강화 데이터들

    public void TryEnhanceByIndex(int index)
    {
        SkinEnhanceData data = enhances[index];

        // 1) 현재 레벨 가져오기
        int currentLevel = 0;
        // 2) 최대 레벨 체크
        if (data.skinType == SkinType.Mantis)
            currentLevel = skinState.mantisLevel;
        else if (data.skinType == SkinType.Grasshopper)
            currentLevel = skinState.grasshopperLevel;
        else if (data.skinType == SkinType.Cockroach)
            currentLevel = skinState.cockroachLevel;

        if (currentLevel >= data.maxLevel)
        {
            Debug.Log("이미 최대 강화 레벨입니다.");
            return;
        }
        // 3) costs로 재화 충분한지 확인
          foreach (EvolutionCost cost in data.costs)
        {
            if (!wallet.HasCurrency(cost.item, cost.amount))
            {
                Debug.Log("재화 부족");
                return;
            }
        }
        // 4) 이제 실제로 비용 차감
        foreach (EvolutionCost cost in data.costs)
        {
            wallet.TrySpendCurrency(cost.item, cost.amount);
        }

        // 5) 마지막으로 레벨 업
        skinState.IncreaseLevel(data.skinType);
    }
}
