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

        // 'maxLevel' → 'maxlevel'로 변경 (정의된 필드명과 일치)
        if (currentLevel >= data.maxlevel)
        {
            Debug.Log("이미 최대 강화 레벨입니다.");
            return;
        }
        // 3) costs로 재화 충분한지 확인
        // 4) 충분하면 TrySpendCurrency로 차감
        // 5) skinState.IncreaseLevel(data.skinType); 로 강화 레벨 +1
    }
}
