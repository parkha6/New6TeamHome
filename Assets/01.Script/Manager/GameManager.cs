using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 게임매니저 순서관리
/// </summary>
public class GameManager : MonoSingleton<GameManager>
{
    /// <summary>
    /// 현재 페이즈 변수
    /// </summary>
    internal Phase systemPhase = Phase.Menu;
    /// <summary>
    /// 플레이어 UI변수
    /// </summary>
    private PlayerUi playerUi;
    /// <summary>
    /// 플레이어 Ui를 외부에서 넣기.
    /// </summary>
    /// <param name="userUi"></param>
    internal void PutPlayerUi(PlayerUi userUi)
    {
        if (userUi != null)
            playerUi = userUi;
    }
    /// <summary>
    /// 일시정지 체크용 업데이트
    /// </summary>
    private void Update()
    {
        if (systemPhase == Phase.Game)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            { Pause(); }
        }
        else if (systemPhase == Phase.Pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            { EndPause(); }
        }
    }
    /// <summary>
    /// sceneName에 씬 이름을 입력하면 해당씬으로 이동.
    /// </summary>
    /// <param name="sceneName"></param>
    internal void ChangeScene(string sceneName)
    { SceneManager.LoadScene(sceneName); }
    /// <summary>
    /// 일시정지시 UI띄우기
    /// </summary>
    internal void Pause()
    {
        StopTime();
        if (playerUi != null)
            playerUi.SetPauseUi(true);
        systemPhase = Phase.Pause;
    }
    /// <summary>
    /// 종료시 시간 흐르게 하기
    /// </summary>
    internal void EndPause()
    {
        if (playerUi != null)
            playerUi.SetPauseUi(false);
        RunTime();
        systemPhase = Phase.Game;
    }
    /// <summary>
    /// 시간을 멈춘다.
    /// </summary>
    void StopTime()
    { Time.timeScale = Consts.none; }
    /// <summary>
    /// 시간을 다시 움직인다.
    /// </summary>
    void RunTime()
    { Time.timeScale = Consts.minValue; }
    /// <summary>
    /// 게임 종료
    /// </summary>
    internal void EndGame()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        { EditorApplication.ExitPlaymode(); }
#endif
        Application.Quit();
    }
}
