
using UnityEngine;

public class SkinDropPickup : MonoBehaviour, IInteractable
{

    public SkinType skinType;   // 이 드롭이 어떤 외피인지 (사마귀/메뚜기/바퀴)

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
                Debug.Log("Player가 근처에 있습니다.");
                playerItemManager.OnSkinItemPickedUp(skinType);
                // PlayerManager.Instance.EquipItem(EquipmentItemData.);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
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

