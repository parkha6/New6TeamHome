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
    public event Action OnPlayerStatusChanged;
    public event Action OnPlayerInvChanged;

    [Header("Default Data & Config")]
    [SerializeField]
    private TextAsset defaultPlayerDataAsset; // 디폴트데이터(json)

    public PlayerSaveData CurrentSaveData { get; private set; } = new PlayerSaveData(); // 초기데이터 세팅 그릇

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
            JArray playerArray = (JArray)root["players"];

            if (playerArray != null && playerArray.Count > 0)
            {
                CharacterData defaultStatus = playerArray[0].ToObject<CharacterData>();
                CurrentSaveData.status = defaultStatus;
                //CurrentSaveData.inventoryStacks = new Dictionary<int, int> //추후 인벤토리 제작 후 주석해제

                //Debug.Log("기본 아이템을 가지고 시작합니다"); // 초기 아이템 있다면 추가
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

    //public void EquipItem(ItemData data) // 나중에 ItemDatas 구조체 따로 만들어두자.....
    //{
    //    string slotKey;
    //    if (data.type.ToLower() == "weapon")
    //    {
    //        slotKey = "weapon"; // 무기는 'weapon' 키 사용
    //    }
    //    else
    //    {
    //        // 장착 불가능한 타입은 여기서 종료
    //        Debug.LogWarning($"{data.name}은(는) 장착 가능한 타입이 아닙니다.");
    //        return;
    //    }
    //    
    //    if (EquippedItems.ContainsKey(slotKey)) // 이미 장착된 아이템 있으면
    //    {
    //        ItemData oldItem = EquippedItems[slotKey]; // 기존 아이템을 되돌림
    //        EquippedItems.Remove(slotKey);
    //    }
    //    EquippedItems.Add(slotKey, data);
    //    Debug.Log($"{data.name} 장착 완료. 상태 업데이트.");
    //    OnPlayerInvChanged?.Invoke();
    //    OnPlayerStatusChanged?.Invoke();
    //}

    //public void UnEquipItem(ItemData data)
    //{
    //    string slotKey;
    //    if (data.type.ToLower() == "weapon")
    //    {
    //        slotKey = "weapon";
    //    }
    //    else
    //    {
    //        return;
    //    }
    //    //string type = data.type.ToLower();
    //    if (EquippedItems.ContainsKey(slotKey) && EquippedItems[slotKey].id == data.id)
    //    {
    //        EquippedItems.Remove(slotKey);
    //        Debug.Log($"{data.name} 해제 완료. 상태 업데이트.");
    //        OnPlayerInvChanged?.Invoke();
    //        OnPlayerStatusChanged?.Invoke();
    //    }
    //}

    //public int TotalAttack()
    //{

    //}

    //public int TotalDef()
    //{

    //}

    //public int TotalHP()
    //{

    //}

    //public int TotalSpeed()
    //{

    //}
}
