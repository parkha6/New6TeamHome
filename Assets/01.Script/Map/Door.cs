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
    [Header("시작로비 씬 문짝")]
    [SerializeField] GameObject startLobby;
    [Header("로비 씬 문짝")]
    [SerializeField] GameObject lobby;
    [Header("쉬움 스테이지 씬 문짝")]
    [SerializeField] GameObject easyStage;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (fkeyUi != null)
            { 
                fkeyUi.SetActive(false); 
            }
        }
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
                    {
                        Debug.Log("메인 씬 이동");
                        GameManager.Instance.ChangeScene(mainscene);
                    }
                    break;
                case sceneName.StartLobby:
                    if (startLobby != null)
                    {
                        Debug.Log("시작로비 씬 이동");
                        GameManager.Instance.nextDoorPosition = startLobby.transform.position;
                        GameManager.Instance.PlayerUi.SetStartLobbyCircle();
                    }
                        break;
                case sceneName.Lobby:
                    if (lobby != null)
                    {
                        Debug.Log("로비 씬 이동");
                        GameManager.Instance.nextDoorPosition = lobby.transform.position;
                        GameManager.Instance.PlayerUi.SetLobbyCircle();
                    }
                        break;
                case sceneName.EasyStage:
                    if (easyStage != null)
                    {
                        Debug.Log("스테이지 씬 이동");
                        GameManager.Instance.EnterStage();
                        GameManager.Instance.nextDoorPosition = easyStage.transform.position;
                        GameManager.Instance.PlayerUi.SetStageCircle();
                    }
                        break;
                case sceneName.None:
                default:
                    break;
            }
        }
    }
}
