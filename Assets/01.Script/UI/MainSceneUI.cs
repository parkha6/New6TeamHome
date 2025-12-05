using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 메인 씬 UI처리
/// </summary>
public class MainSceneUI : MonoBehaviour
{
    /// <summary>
    /// 시작버튼
    /// </summary>
    [Tooltip("시작버튼")]
    [SerializeField] Button startButton;
    /// <summary>
    /// 옵션버튼
    /// </summary>
    [Tooltip("옵션버튼")]
    [SerializeField] Button optionButton;
    /// <summary>
    /// 옵션 UI창
    /// </summary>
    [Tooltip("옵션 UI창")]
    [SerializeField] GameObject optionUi;
    /// <summary>
    /// 게임 종료버튼
    /// </summary>
    [Tooltip("게임 종료버튼")]
    [SerializeField] Button endButton;
    [Header("시작 로비씬 이름")]
    /// <summary>
    /// 시작 로비씬 이름
    /// </summary>
    [Tooltip("시작 로비씬 이름")]
    [SerializeField] string startLobbyScene;
    /// <summary>
    /// 생성될때 버튼 연결
    /// </summary>
    private void Awake()
    {
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);
        if (optionButton != null)
            optionButton.onClick.AddListener(OptionOn);
        if (endButton != null)
            endButton.onClick.AddListener(GameManager.Instance.EndGame);
    }
    /// <summary>
    /// 게임 시작씬으로 넘어가기
    /// </summary>
    void StartGame()
    {
        if (startLobbyScene != null)
        {

            GameManager.Instance.systemPhase = Phase.Game;
            GameManager.Instance.ChangeScene(startLobbyScene);
            if (GameManager.Instance.async.isDone)
            {
                GameManager.Instance.PlayerUi.SetPauseUi(false);
                GameManager.Instance.PlayerUi.SetPlayerScreenUi(true);
            }
        }
    }
    /// <summary>
    /// 옵션 UI켜기
    /// </summary>
    void OptionOn()
    {
        if (optionUi != null)
            optionUi.SetActive(true);
    }
    /// <summary>
    /// 삭제되면 구독해지
    /// </summary>
    private void OnDestroy()
    {
        startButton.onClick.RemoveAllListeners();
        optionButton.onClick.RemoveAllListeners();
        endButton.onClick.RemoveAllListeners();
    }
}
