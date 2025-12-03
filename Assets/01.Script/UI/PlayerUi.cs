using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 플레이어 UI관리용
/// </summary>
public class PlayerUi : MonoBehaviour
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
    /// 날개조각 스코어
    /// </summary>
    [SerializeField] TMP_Text wingPieceText;
    /// <summary>
    /// 외피조각 스코어
    /// </summary>
    [SerializeField] TMP_Text skinPieceText;
    /// <summary>
    /// 골드 스코어
    /// </summary>
    [SerializeField] TMP_Text goldPieceText;
    [Header("일시정지 UI")]
    /// <summary>
    /// 일시정지 창 닫기 버튼
    /// </summary>
    [SerializeField] Button pauseUiEndButton;
    /// <summary>
    /// 턱 스텟 텍스트
    /// </summary>
    [SerializeField] TMP_Text atkStatText;
    /// <summary>
    /// 갑각 스텟 텍스트
    /// </summary>
    [SerializeField] TMP_Text skinStatText;
    /// <summary>
    /// 날개 스텟 텍스트
    /// </summary>
    [SerializeField] TMP_Text wingStatText;
    /// <summary>
    /// 생명력 스텟 텍스트
    /// </summary>
    [SerializeField] TMP_Text hpStatText;
    /// <summary>
    /// 외피 아이콘
    /// </summary>
    [SerializeField] Image skinIcon;
    /// <summary>
    /// 스킬 1 아이콘 
    /// </summary>
    [SerializeField] Image skillIcon1;
    /// <summary>
    /// 스킬 2 아이콘
    /// </summary>
    [SerializeField] Image skillIcon2;
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
    /// 전부 한번에 세팅
    /// </summary>
    /// <param name="currentHp"></param>
    /// <param name="wholeHp"></param>
    /// <param name="wing"></param>
    /// <param name="skin"></param>
    /// <param name="gold"></param>
    /// <param name="atkStat"></param>
    /// <param name="skinStat"></param>
    /// <param name="wingStat"></param>
    /// <param name="hpStat"></param>
    internal void SetEverything(float currentHp, float wholeHp, int wing, int skin, int gold, float atkStat, float skinStat, float wingStat, float hpStat)
    {
        SetPlayerHp(currentHp, wholeHp);//원래는 여기에 세팅해야 됨.
        SetScoreText(wing, skin, gold);
        SetStat(atkStat, skinStat, wingStat, hpStat);
    }

    /// <summary>
    /// HP를 입력하면 바에 표시 됨.
    /// </summary>
    /// <param name="currentHp"></param>
    /// <param name="wholeHp"></param>
    internal void SetPlayerHp(float currentHp, float wholeHp)
    {
        if (hpBar != null)
            hpBar.fillAmount = currentHp / wholeHp;
    }
    /// <summary>
    /// 날개,외피,골드 스코어 한번에 세팅
    /// </summary>
    /// <param name="wing"></param>
    /// <param name="skin"></param>
    /// <param name="gold"></param>
    internal void SetScoreText(int wing, int skin, int gold)
    {
        SetWingScore(wing);
        SetSkinScore(skin);
        SetGoldScore(gold);
    }
    /// <summary>
    /// 날개 조각을 스코어에 표시
    /// </summary>
    /// <param name="wing"></param>
    internal void SetWingScore(int wing)
    {
        if (wingPieceText != null)
            wingPieceText.text = wing.ToString();
    }
    /// <summary>
    /// 외피 조각을 스코어에 표시
    /// </summary>
    /// <param name="skin"></param>
    internal void SetSkinScore(int skin)
    {
        if (skinPieceText != null)
            skinPieceText.text = skin.ToString();
    }
    /// <summary>
    /// 골드를 스코어에 표시
    /// </summary>
    /// <param name="gold"></param>
    internal void SetGoldScore(int gold)
    {
        if (goldPieceText != null)
            goldPieceText.text = gold.ToString();
    }
    /// <summary>
    /// 스텟 한번에 세팅
    /// </summary>
    internal void SetStat(float atkStat, float skinStat, float wingStat, float hpStat)
    {
        SetAtkStat(atkStat);
        SetSkinStat(skinStat);
        SetWingStat(wingStat);
        SetHpStat(hpStat);
    }
    /// <summary>
    /// 턱 스텟 세팅
    /// </summary>
    /// <param name="atkStat"></param>
    internal void SetAtkStat(float atkStat)
    {
        if (atkStatText != null)
            atkStatText.text = $"턱 : +{atkStat * 100}%";
    }
    /// <summary>
    /// 갑각 스텟 세팅
    /// </summary>
    /// <param name="skinStat"></param>
    internal void SetSkinStat(float skinStat)
    {
        if (skinStatText != null)
            skinStatText.text = $"갑각 : +{skinStat * 100}%";
    }
    /// <summary>
    /// 날개 스텟 세팅
    /// </summary>
    /// <param name="wingStat"></param>
    internal void SetWingStat(float wingStat)
    {
        if (wingStatText != null)
            wingStatText.text = $"날개 : +{wingStat * 100}%";
    }
    /// <summary>
    /// 생명력 스텟 세팅
    /// </summary>
    /// <param name="hpStat"></param>
    internal void SetHpStat(float hpStat)
    {
        if (hpStatText != null)
            hpStatText.text = $"생명력 : +{hpStat * 100}%";
    }
    /// <summary>
    /// 외피&스킬 아이콘 전부 세팅
    /// </summary>
    /// <param name="skin"></param>
    /// <param name="skill1"></param>
    /// <param name="skill2"></param>
    internal void SkinNSkillIcon(Sprite skin, Sprite skill1, Sprite skill2)
    {
        SetSkinIcon(skin);
        SetSkillIcon(skill1);
        SetSkillIcon(skill2);
    }
    /// <summary>
    /// 외피 아이콘 세팅
    /// </summary>
    /// <param name="icon"></param>
    internal void SetSkinIcon(Sprite icon)
    {
        if (skinIcon != null)
            skinIcon.sprite = icon;
    }
    /// <summary>
    /// 스킬1 아이콘 세팅
    /// </summary>
    /// <param name="icon"></param>
    internal void SetSkillIcon(Sprite icon)
    {
        if (skillIcon1 != null)
            skillIcon1.sprite = icon;
    }
    /// <summary>
    /// 스킬2 아이콘 세팅
    /// </summary>
    /// <param name="icon"></param>
    internal void SetSkillIcon2(Sprite icon)
    {
        if (skillIcon2 != null)
            skillIcon2.sprite = icon;
    }
    /// <summary>
    /// 생성시 버튼 연결
    /// </summary>
    private void Start()
    {
        pauseUiEndButton.onClick.AddListener(GameManager.Instance.EndPause);
    }
    /// <summary>
    /// 파괴시 버튼 구독 해제
    /// </summary>
    private void OnDestroy()
    {
        pauseUiEndButton.onClick.RemoveAllListeners();
    }
}
