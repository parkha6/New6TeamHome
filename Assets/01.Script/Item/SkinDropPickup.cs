
using UnityEngine;

public class SkinDropPickup : MonoBehaviour
{
    public SkinType skinType;   // 이 드롭이 어떤 외피인지 (사마귀/메뚜기/바퀴)

    bool isPlayerInRange = false;
    GameObject player;

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

    void Update()
    {
        // 예: 공격 키(J키)로 줍기
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.X))
        {
            TryPickup();
        }
    }

    void TryPickup()
    {
        if (player == null) return;

        ItemManager itemManager = player.GetComponent<ItemManager>();
        if (itemManager != null)
        {
            itemManager.OnSkinItemPickedUp(skinType);
        }

        Destroy(gameObject);
    }
}
