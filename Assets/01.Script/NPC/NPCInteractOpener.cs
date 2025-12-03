using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractOpener : MonoBehaviour
{
    public GameObject evolutionPanel; // 또는 skinEnhancePanel
    bool isPlayerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = false;
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            evolutionPanel.SetActive(true);
            // 여기서 UI 스크립트의 Refresh() 같은 것도 같이 호출 가능
        }
    }
}
