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
    public Dictionary<string, PlayerItemData> EquippedItems { get; private set; } = new Dictionary<string, PlayerItemData>();

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
            JArray playerArray = (JArray)root["Players"];

            if (playerArray != null && playerArray.Count > 0)
            {
                CharacterData defaultStatus = playerArray[0].ToObject<CharacterData>();
                CurrentSaveData.status = defaultStatus;
                //CurrentSaveData.inventoryStacks = new Dictionary<int, int> //추후 인벤토리 제작 후 주석해제

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
    //public void AddItem(int itemID) // 아이템 추가
    //{
    //    if (CurrentSaveData.inventoryStacks.ContainsKey(itemID))
    //    {
    //        CurrentSaveData.inventoryStacks[itemID]++; // 딕셔너리에 이미 있으면 수량 추가
    //    }
    //    else
    //    {
    //        CurrentSaveData.inventoryStacks.Add(itemID, 1); // 없으면 1개 새로 추가 
    //    }
    //    OnPlayerInvChanged?.Invoke(); // 인벤 바뀐거 알려주고 오토 세이브 
    //} //인벤토리 추가되면 주석 해제

    //public void RemoveItem(int itemID, int amount = 1) // 아이템 사용 이하는 additem이랑 거의 같음.
    //{
    //    if (CurrentSaveData.inventoryStacks.ContainsKey(itemID))
    //    {
    //        CurrentSaveData.inventoryStacks[itemID] -= amount;

    //        if (CurrentSaveData.inventoryStacks[itemID] < 0)
    //        {
    //            CurrentSaveData.inventoryStacks.Remove(itemID);
    //        }
    //        OnPlayerInvChanged?.Invoke();
    //    }
    //} 인벤토리 추가되먼 주석해제

    public void EquipItem(PlayerItemData data) // 나중에 itemdata 받아서 다시 수정
    {
        string slotKey;
        if (data.Type.ToLower() == "weapon")
        {
            slotKey = "weapon"; // 무기는 'weapon' 키 사용
        }
        else
        {
            // 장착 불가능한 타입은 여기서 종료
            Debug.LogWarning($"{data.Name}은(는) 장착 가능한 타입이 아닙니다.");
            return;
        }

        if (EquippedItems.ContainsKey(slotKey)) // 이미 장착된 아이템 있으면
        {
            PlayerItemData oldItem = EquippedItems[slotKey]; // 기존 아이템을 되돌림
            EquippedItems.Remove(slotKey);
        }
        EquippedItems.Add(slotKey, data);
        Debug.Log($"{data.Name} 장착 완료. 상태 업데이트.");
        OnPlayerInvChanged?.Invoke();
        OnPlayerStatusChanged?.Invoke();
    }

    public void UnEquipItem(PlayerItemData data)
    {
        string slotKey;
        if (data.Type.ToLower() == "weapon")
        {
            slotKey = "weapon";
        }
        else
        {
            return;
        }
        //string type = data.type.ToLower();
        if (EquippedItems.ContainsKey(slotKey) && EquippedItems[slotKey].ID == data.ID)
        {
            EquippedItems.Remove(slotKey);
            Debug.Log($"{data.Name} 해제 완료. 상태 업데이트.");
            OnPlayerInvChanged?.Invoke();
            OnPlayerStatusChanged?.Invoke();
        }
    }

    public float TotalAttack()
    {
        float totalAttack = CurrentStatus.ATK;
        foreach (var item in EquippedItems.Values)
        {
            totalAttack *= item.Atk_Value; // 추후 강화value도 추가하자.
        }
        return totalAttack;
    }

    public float TotalDef()
    {
        float totalDef = CurrentStatus.DEF;
        foreach (var item in EquippedItems.Values)
        {
            totalDef *= item.Def_Value; // 추후 강화value도 추가하자.
        }
        return totalDef;
    }

    //public int TotalHP()
    //{

    //}

    //public int TotalSpeed()
    //{

    //}
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

}
