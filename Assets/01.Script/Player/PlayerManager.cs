using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    public event Action OnPlayerStatusChanged; // statusUI에 알려주기 위한 이벤트
    public event Action OnPlayerInvChanged; // invUI에 알려주기 위한 이벤트
    public bool isInvincible = false; 
    public float invincibilityDuration; //무적 지속 시간

    public Dictionary<string, EquipmentItemData> EquippedItems { get; private set; } = new Dictionary<string, EquipmentItemData>();
    public Dictionary<int, int> CurrencyAmounts { get; private set; } = new Dictionary<int, int>(); // item의 id를 사용하여 딕셔너리로 관리

    [Header("Default Data & Config")]
    [SerializeField]
    private TextAsset defaultPlayerDataAsset; // 디폴트데이터(json)

    public PlayerSaveData CurrentSaveData { get; private set; } = new PlayerSaveData(); // 초기데이터 세팅 그릇
    public CharacterData CurrentStatus => CurrentSaveData.status;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadDefaultData();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadDefaultData()
    {
        if (defaultPlayerDataAsset == null)
        {
            Debug.LogError("기본 플레이어 에셋이 연결 안됨");
        }
        try
        {
            JToken root = JToken.Parse(defaultPlayerDataAsset.text);
            JArray playerArray = root as JArray;

            if (playerArray != null && playerArray.Count > 0)
            {
                CharacterData defaultStatus = playerArray[0].ToObject<CharacterData>();
                CurrentSaveData.status = defaultStatus;
                //Debug.Log("기본 아이템을 가지고 시작합니다"); // 초기 아이템 있다면 추가
                Debug.Log("디폴트 플레이어로 시작합니다");
            }
        }
        catch (Exception)
        {
            Debug.Log("기본 플레이어 데이터 로드 및 파싱 오류");
        }
        OnPlayerStatusChanged?.Invoke();
        OnPlayerInvChanged?.Invoke(); // 초기 로드 후 UI에 알려줌
    }


    public void EquipItem(EquipmentItemData data) // 나중에 itemdata 받아서 다시 수정
    {
        string slotKey;
        if (data.itemType.ToString() == "Equipment")
        {
            slotKey = "weapon"; //외피는 하나만 장착 가능하기때문에 한개의 슬롯키(weapon)만 생성하여 하나만 장착 가능하게 함.
        }
        else
        {
            // 장착 불가능한 타입은 여기서 종료
            Debug.LogWarning($"{data.itemName}은(는) 장착 가능한 타입이 아닙니다.");
            return;
        }

        if (EquippedItems.ContainsKey(slotKey)) // 이미 장착된 아이템 있으면
        {
            EquipmentItemData oldItem = EquippedItems[slotKey]; // 기존 아이템을 되돌림
            EquippedItems.Remove(slotKey);
            Debug.Log($"{oldItem.itemName}을 해제합니다");
        }
        EquippedItems.Add(slotKey, data);
        Debug.Log($"{data.itemName} 장착 완료. 상태 업데이트.");
        Debug.Log($"{TotalAttack()}공격력 적용");
        OnPlayerInvChanged?.Invoke();// 나중에 가져가서 구독하세요
        OnPlayerStatusChanged?.Invoke();// 나중에 가져가서 구독하세요
    }

    public float TotalAttack()
    {
        float totalAttack = CurrentStatus.ATK;
        float itemAtkValue = Consts.none;
        foreach (var item in EquippedItems.Values)
        {
            totalAttack += totalAttack * item.atkValue; // 추후 강화value도 추가하자.
            itemAtkValue += item.atkValue;
        }
        GameManager.Instance.PlayerUi.SetAtkStat(itemAtkValue);
        return totalAttack;
    }

    public float TotalDef()
    {
        float totalDef = CurrentStatus.DEF;
        float itemDefValue = Consts.none;
        foreach (var item in EquippedItems.Values)
        {
            totalDef += totalDef * item.defValue; // 추후 강화value도 추가하자.
            itemDefValue += item.defValue;
        }
        GameManager.Instance.PlayerUi.SetSkinStat(itemDefValue);
        return totalDef;
    }

    public float TotalHP()
    {
        float totalHP = CurrentStatus.HP;
        float itemHpValue = Consts.none;
        foreach (var item in EquippedItems.Values)
        {
            totalHP += totalHP * item.defValue; // 추후 강화value도 추가하자.
            itemHpValue += item.hpValue;
        }
        GameManager.Instance.PlayerUi.SetHpStat(itemHpValue);
        return totalHP;

    }

    public float TotalSpeed()
    {
        float totalSpeed = CurrentStatus.SPEED;
        float itemSpeedValue = Consts.none;
        foreach (var item in EquippedItems.Values)
        {
            totalSpeed += totalSpeed * item.spdValue; // 추후 강화value도 추가하자.
            itemSpeedValue += item.spdValue;
        }
        GameManager.Instance.PlayerUi.SetHpStat(itemSpeedValue);
        return totalSpeed;

    }
    public void Invincibility()
    {
        // 추후 takedamage에 무적이면 무시 로직 추가
        StartCoroutine(InvincibilityCoroutine());
    }
    private IEnumerator InvincibilityCoroutine()
    {
        Debug.Log("무적 시작");
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        Debug.Log("무적 종료");
    }
    /// <summary>
    /// 시작시 스텟 삽입
    /// </summary>
    private void Start()
    {
        float itemAtkValue = Consts.none;
        float itemDefValue = Consts.none;
        float itemSpdValue = Consts.none;
        float itemHpValue = Consts.none;
        foreach (var item in EquippedItems.Values)
        {
            itemAtkValue += item.atkValue;
            itemDefValue += item.defValue;
            itemSpdValue += item.spdValue;
            itemHpValue += item.hpValue;
        }
        GameManager.Instance.PlayerUi.SetStat(itemAtkValue,itemDefValue,itemSpdValue,itemHpValue);
    }

}
