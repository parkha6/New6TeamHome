using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Default Data & Config")]
    [SerializeField]
    private TextAsset defaultEnemyDataAsset;
    public EnemySaveData CurrentSaveData { get; private set; } = new EnemySaveData(); // 초기데이터 세팅 그릇
    public CharacterData CurrentStatus => CurrentSaveData.status;

    private void Awake()
    {
        LoadDefaultData();
    }
    private void LoadDefaultData()
    {
        if (defaultEnemyDataAsset == null)
        {
            Debug.LogError("기본 Enemy 에셋이 연결 안됨");
        }
        try
        {
            JToken root = JToken.Parse(defaultEnemyDataAsset.text);
            JArray enemyArray = root as JArray;

            if (enemyArray != null && enemyArray.Count > 0)
            {
                CharacterData defaultStatus = enemyArray[1].ToObject<CharacterData>();
                CurrentSaveData.status = defaultStatus;
            }
        }
        catch (Exception)
        {
            Debug.Log("기본 Enemy 데이터 로드 및 파싱 오류");
        }
        //OnPlayerStatusChanged?.Invoke();
        //OnPlayerInvChanged?.Invoke(); // 초기 로드 후 UI에 알려줌
    }
    public void TakePhisicalDamage(float amount)
    {
        //amount = PlayerManager.Instance.TotalAttack();
        float hp = CurrentStatus.HP;
        hp -= amount;
        if (hp <= 0)
        {
            hp = Mathf.Max(hp, 0);
        }
        Debug.Log($"{hp}");
        Die();
    }

    private void Die()
    {
        Destroy(gameObject);
        // 재화
    }
}
