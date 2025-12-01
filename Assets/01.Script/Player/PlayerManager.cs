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
                
                //Debug.Log("기본 아이템을 가지고 시작합니다");
            }
        }
        catch (Exception)
        {
            Debug.Log("기본 플레이어 데이터 로드 및 파싱 오류");
        }
        OnPlayerStatusChanged?.Invoke();
        OnPlayerInvChanged?.Invoke(); // 초기 로드 후 UI에 알려줌
    }
}
