using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterectingUi : MonoBehaviour
{
    /// <summary>
    /// F키를 안내할 UI
    /// </summary>
    [SerializeField] GameObject fkeyUi;
    /// <summary>
    /// 플레이어가 외피와 충돌하면 UI를 띄운다
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fkeyUi != null)
            {
                    fkeyUi.SetActive(true);
            }
            else
                Debug.Log("F키 UI가 없음");
        }
        else
            Debug.Log("플레이어가 아님");
        Debug.Log("충돌인식은 되나?");
    }
    /// <summary>
    /// 나갈시 UI꺼짐
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fkeyUi != null)
            {
                fkeyUi.SetActive(false);
            }
            else
                Debug.Log("F키 UI가 없음");
        }
        else
            Debug.Log("플레이어가 아님");
        Debug.Log("충돌인식은 되나?");
    }
}
