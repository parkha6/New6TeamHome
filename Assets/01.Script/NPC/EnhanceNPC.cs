using TMPro;
using UnityEngine;

public class EnhanceNPC : MonoBehaviour, IInteractable
{
    public CurrencyWallet wallet;          // 재화
    public SkinUpdateState skinState;      // 각 외피 강화 레벨
    public SkinEnhanceData[] enhances;     // Mantis / Grasshopper / Cockroach 강화 데이터들
    public Transform player;      // 플레이어 Transform
    public float closeDistance = 3f; // 이 거리보다 멀어지면 UI 닫기

    // 강화 UI 패널 + 텍스트들
    public GameObject enhanceUI;
    public TextMeshProUGUI mantisText;
    public TextMeshProUGUI grasshopperText;
    public TextMeshProUGUI cockroachText;

    void Start()
    {
        if (enhanceUI != null)
            enhanceUI.SetActive(false);

        UITextSet();
    }

    void Update()
    {
        // UI가 열려 있을 때만 거리 체크
        if (enhanceUI != null && enhanceUI.activeSelf)
        {
            if (player == null) return;

            float dist = Vector3.Distance(player.position, transform.position);

            if (dist > closeDistance)
            {
                enhanceUI.SetActive(false);
            }
        }
    }

    public void OnInteraction()
    {
        if (enhanceUI != null)
            enhanceUI.SetActive(true);
    }

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

    public void UITextSet()
    {
        // 규칙: 0=사마귀, 1=메뚜기, 2=바퀴벌레 라고 약속
        if (mantisText != null && enhances.Length > 0)
            mantisText.text = enhances[0].name;

        if (grasshopperText != null && enhances.Length > 1)
            grasshopperText.text = enhances[1].name;

        if (cockroachText != null && enhances.Length > 2)
            cockroachText.text = enhances[2].name;
    }

}
