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

        if (hit != null)
        {
            if (hit.TryGetComponent<IInteractable>(out IInteractable interactableObject))
            {
                interactableObject.OnInteraction();
                Debug.Log($"{hit.gameObject.name} 와 상호작용했습니다.");
            }
        }
    }
}
