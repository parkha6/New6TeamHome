using System.Collections;
using UnityEngine;
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
}
