using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private PlayerManager playerManager;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    private void Start()
    {
        playerManager = PlayerManager.Instance;
        if (playerManager == null)
        {
            Debug.LogError("플레이어 매니저 연결 안됨");
        }
    }
    public void TakePhisicalDamage(float amount) // 나중에 float로 바꾸는게 편할듯. player능력치가 다 float임
    {
        if (playerManager.isInvincible)
        {
            Debug.Log("플레이어가 무적 상태입니다");
            return;
        }
        //float totalAtk = playerManager.TotalAttack();
        float totalDef = playerManager.TotalDef();
        //float totalHP = playerManager.TotalHP();

        float finalDamage = amount - totalDef;
        if (finalDamage < 0) finalDamage = 0;
        Debug.Log($"{finalDamage}만큼의 피해를 입었습니다.");
    }
}

