using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EvolveNPC : MonoBehaviour, IInteractable
{
    public CurrencyWallet wallet;
    public PermanentStats permanentStats;
    public EvolutionUpgradeData[] upgrades;
    public GameObject evolutionUI;

    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI speedText;

    public Transform player;      // 플레이어 Transform
    public float closeDistance = 3f; // 이 거리보다 멀어지면 UI 닫기

    public void Start()
    {
        evolutionUI.SetActive(false);
        UITextSet();
    }

    void Update()
    {
        // 🔹 UI가 열려 있을 때만 거리 체크
        if (evolutionUI != null && evolutionUI.activeSelf)
        {
            if (player == null) return;

            float dist = Vector3.Distance(player.position, transform.position);

            if (dist > closeDistance)
            {
                evolutionUI.SetActive(false);
            }
        }
    }

    public void OnInteraction()
    {
        evolutionUI.SetActive(true);
    }
    public void UITextSet()
    {
        atkText.text = $"{upgrades[0].name}";
        defText.text = $"{upgrades[1].name}";
        hpText.text = $"{upgrades[2].name}";
        speedText.text = $"{upgrades[3].name}";
    }

    public void TryEvolveByIndex(int index)
    {
        EvolutionUpgradeData upgradeData = upgrades[index];
        // 여기 안에서 기존 TryEvolve 로직을 이 upgradeData 기준으로 돌릴 예정
        Debug.Log($"현재 강화 능력치 {upgradeData}");

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


    
