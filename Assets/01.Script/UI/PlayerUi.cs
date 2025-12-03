using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 플레이어 UI관리용
/// </summary>
public class PlayerUi : MonoSingleton<PlayerUi>
{
    /// <summary>
    /// 플레이어 화면표시 UI
    /// </summary>
    [SerializeField] GameObject playerScreenUi;
    /// <summary>
    /// 일시정지시 띄우는 창
    /// </summary>
    [SerializeField] GameObject pauseUi;
    [Header("화면에 뜨는 UI")]
    /// <summary>
    /// hp바 이미지
    /// </summary>
    [SerializeField] Image hpBar;
    /// <summary>
    /// 외피조각 스코어
    /// </summary>
    [SerializeField] Text skinPieceText;
    /// <summary>
    /// 날개조각 스코어
    /// </summary>
    [SerializeField] Text wingPieceText;
    /// <summary>
    /// 골드 스코어
    /// </summary>
    [SerializeField] Text goldPieceText;
    [Header("일시정지 UI")]
    /// <summary>
    /// 일시정지 창 닫기 버튼
    /// </summary>
    [SerializeField] Button pauseUiEndButton;
    /// <summary>
    /// 턱 스텟 텍스트
    /// </summary>
    [SerializeField] Text atkStatText;
    /// <summary>
    /// 갑각 스텟 텍스트
    /// </summary>
    [SerializeField] Text skinStatText;
    /// <summary>
    /// 날개 스텟 텍스트
    /// </summary>
    [SerializeField] Text wingStatText;
    /// <summary>
    /// 생명력 스텟 텍스트
    /// </summary>
    [SerializeField] Text hpStatText;
    /// <summary>
    /// 일시정지 UI 활성& 비활성
    /// </summary>
    /// <param name="isSet"></param>
    internal void SetPauseUi(bool isSet)
    {
        if (pauseUi != null)
            pauseUi.SetActive(isSet);
    }
    /// <summary>
    /// 배틀씬에서 화면표시 UI만 끄기
    /// </summary>
    /// <param name="isSet"></param>
    internal void SetPlayerScreenUi(bool isSet)
    {
        if (playerScreenUi != null)
            playerScreenUi.SetActive(isSet);
    }
    /// <summary>
    /// 생성시 버튼 연결
    /// </summary>
    private void Start()
    { pauseUiEndButton.onClick.AddListener(GameManager.Instance.EndPause); }
    /// <summary>
    /// 파괴시 버튼 구독 해제
    /// </summary>
    private void OnDestroy()
    { pauseUiEndButton.onClick.RemoveAllListeners(); }
}
