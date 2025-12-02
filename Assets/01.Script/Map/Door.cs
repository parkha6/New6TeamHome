using UnityEngine;

public class Door : MonoBehaviour
{
    /// <summary>
    /// 이동할 씬
    /// </summary>
    [SerializeField] string whichScene;
    /// <summary>
    /// 작동하는 문인지 판단하는 불값
    /// </summary>
    [SerializeField] bool isWorking;
    /// <summary>
    /// 문 작동
    /// </summary>
    void EnterRoom()
    {
        if (!isWorking)
        { GameManager.Instance.ChangeScene(whichScene); }
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
}
