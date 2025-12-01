
using UnityEngine;

public class EnhanceNPC : MonoBehaviour
{
    public string npcName;  // 혹시 여러 NPC 구분용

    bool isPlayerInRange = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: 플레이어 들어왔는지 체크
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // TODO: 플레이어 나갔는지 체크
    }

    private void Update()
    {
        // TODO: 플레이어가 범위 안에 있을 때 상호작용 키 입력 받기
    }

    void OpenEnhanceUI()
    {
        // TODO: 강화/진화 UI 열기
    }

    public void TryEnhance()
    {
        // TODO: 재화 체크 + 강화 처리
    }

    public void TryEvolve()
    {
        // TODO: 재화 체크 + 진화 처리
    }
}
