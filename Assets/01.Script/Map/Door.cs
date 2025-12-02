using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("문에서 이동할 씬의 이름.")]
    /// <summary>
    /// 이동할 씬
    /// </summary>
    [SerializeField] string whichScene;
    /// <summary>
    /// 작동하는 문인지 판단하는 불값
    /// </summary>
    [SerializeField] bool isWorking;
    /// <summary>
    /// 플레이어가 문과 충돌하면 문 매니저에 씬 정보를 보낸다.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isWorking)
                DoorManager.Instance.DoorInfo(whichScene);
            else
                DoorManager.Instance.NotDoor();
        }
    }
}
