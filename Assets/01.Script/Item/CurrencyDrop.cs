
using UnityEngine;

public class CurrencyDrop : MonoBehaviour
{
    public ItemData item;   // 골드/조각 SO
    public int amount = 1;  // 줍는 개수

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: 플레이어가 닿았을 때 지갑을 찾아서 AddCurrency 호출하고 자신 삭제
        if (other.CompareTag("Player"))
        {
            CurrencyWallet wallet = FindObjectOfType<CurrencyWallet>();
            if (wallet != null)
            {
                wallet.AddCurrency(item, amount);
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("CurrencyWallet을 씬에서 찾지 못했습니다!");
            }
        }
    }
}
