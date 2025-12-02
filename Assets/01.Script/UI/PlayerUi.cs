using UnityEngine;
/// <summary>
/// 플레이어 UI관리용
/// </summary>
public class PlayerUi : MonoBehaviour
{
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
    /// 생성시 플레이어UI에 자신을 넣는다.
    /// </summary>
    private void Awake()
    { GameManager.Instance.PutPlayerUi(this); }
}
