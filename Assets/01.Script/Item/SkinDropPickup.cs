using UnityEngine;

public class SkinDropPickup : MonoBehaviour, IInteractable
{

    public SkinType skinType;   // 이 드롭이 어떤 외피인지 (사마귀/메뚜기/바퀴)
    public EquipmentItemData skinData;   // 이 드롭이 주는 외피 장비 SO

    bool isPlayerInRange = false;
    ItemManager playerItemManager;
    GameObject player;

    public void OnInteraction()
    {
 

        Debug.Log("OnInteraction 작동");
        if (isPlayerInRange)
        {
            if (playerItemManager != null)
            {
                TryPickup();
                // PlayerManager.Instance.EquipItem(EquipmentItemData.);
            }
        }
    }

    void TryPickup()
    {
        if (playerItemManager == null || skinData == null)
            return;

        // EquipSkin은 SkinType을 인자로 받으므로 skinData.skinType을 전달해야 합니다.
        playerItemManager.OnSkinItemPickedUp(skinData);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            playerItemManager = other.GetComponent<ItemManager>();
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            player = null;
        }
    } 
}

