using UnityEngine;
public class Door : MonoBehaviour, IInteractable
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
    /// 플레이어가 문과 충돌하면 문에 UI를 띄운다
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    { //UI띄우기
    }
    /// <summary>
    /// 작동하는 문이면 씬을 이동한다.
    /// </summary>
    public void OnInteraction()
    {
        if (isWorking)
            GameManager.Instance.ChangeScene(whichScene);
    }
}
