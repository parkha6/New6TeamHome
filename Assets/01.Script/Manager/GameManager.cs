using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 게임매니저 순서관리
/// </summary>
public class GameManager : MonoSingleton<GameManager>
{
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
    { StopTime(); }
    internal void EndPause()
    { RunTime(); }
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
}
