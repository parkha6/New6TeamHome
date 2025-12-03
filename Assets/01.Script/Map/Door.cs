using UnityEngine;
/// <summary>
/// 문의 기능을 담당하는 클래스
/// </summary>
public class Door : MonoBehaviour, IInteractable
{
    [Header("문에서 이동할 씬의 이름.")]
    /// <summary>
    /// 이동할 씬
    /// </summary>
    [SerializeField] sceneName whichScene;
    [Header("메인매뉴 씬 이름")]
    [SerializeField] string mainscene;
    [Header("시작로비 씬 이름")]
    [SerializeField] string startLobby;
    [Header("로비 씬 이름")]
    [SerializeField] string lobby;
    [Header("쉬움 스테이지 씬 이름")]
    [SerializeField] string easyStage;
    /// <summary>
    /// 작동하는 문인지 판단하는 불값
    /// </summary>
    [SerializeField] bool isWorking;
    /// <summary>
    /// F키를 안내할 UI
    /// </summary>
    [SerializeField] GameObject fkeyUi;
    /// <summary>
    /// 플레이어가 문과 충돌하면 문에 UI를 띄운다
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (fkeyUi != null)
            {
                if (isWorking)
                    fkeyUi.SetActive(true);
                else
                    Debug.Log("작동하는 문이 아님");
            }
            else
                Debug.Log("F키 UI가 없음");
        }
        else
            Debug.Log("플레이어가 아님");
        Debug.Log("충돌인식은 되나?");
    }
    /// <summary>
    /// 나갈때 안내가 꺼짐
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (fkeyUi != null)
            { fkeyUi.SetActive(false); }
            else
                Debug.Log("F키 UI가 없음");
        }
        else
            Debug.Log("플레이어가 아님");
        Debug.Log("충돌인식은 되나?");
    }
    /// <summary>
    /// 작동하는 문이면 씬을 이동한다.
    /// </summary>
    public void OnInteraction()
    {
        if (isWorking)
        {
            switch (whichScene)
            {
                case sceneName.MainScene:
                    if (mainscene != null)
                        GameManager.Instance.ChangeScene(mainscene);
                    break;
                case sceneName.StartLobby:
                    if (startLobby != null)
                        GameManager.Instance.ChangeScene(startLobby);
                    break;
                case sceneName.Lobby:
                    if (lobby != null)
                        GameManager.Instance.ChangeScene(lobby);
                    break;
                case sceneName.EasyStage:
                    if (easyStage != null)
                        GameManager.Instance.ChangeScene(easyStage);
                    break;
                case sceneName.None:
                default:
                    break;
            }
        }
    }
}
