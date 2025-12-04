using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 3f; // 상호작용 거리
    public LayerMask interactableLayer; // 상호작용 가능한 오브젝트 레이어
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TryInteract();
        }
    }

    private void TryInteract()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            transform.position,
            interactionRange,
            interactableLayer
        );
        if (hit == null)
        {
            Debug.Log("OverlapCircle: 아무것도 감지되지 않음.");
            return;
        }

        if (hit != null)
        {
            if (hit.TryGetComponent<IInteractable>(out IInteractable interactableObject))
            {
                interactableObject.OnInteraction();
                Debug.Log($"상호작용 성공: {hit.gameObject.name} 와 상호작용했습니다.");
            }
            else
            {
                //콜라이더는 찾았지만, IInteractable이 없음
                Debug.LogWarning($"콜라이더는 찾았지만, {hit.gameObject.name}에 IInteractable 인터페이스가 없습니다.");
            }
        }
    }
}
