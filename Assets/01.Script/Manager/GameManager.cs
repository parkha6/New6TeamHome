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
    [SerializeField] internal Phase systemPhase = Phase.Menu;
    /// <summary>
    /// 비동기 처리용
    /// </summary>
    internal AsyncOperation async;
    [SerializeField] private GameObject playerUiPrefab; 
    private PlayerUi playerUi;
    public PlayerUi PlayerUi
    {
        get
        {
            if (playerUi == null)
            {
                Debug.Log("플레이어 UI생성");
                GameObject obj = Instantiate(playerUiPrefab);
                playerUi = obj.GetComponent<PlayerUi>();
            }
            return playerUi;
        }
    }
    /// <summary>
    /// 일시정지 체크용 업데이트
    /// </summary>
    private void Update()
    {
        if (systemPhase == Phase.Game)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
        else if (systemPhase == Phase.Pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndPause();
            }
        }
    }
    /// <summary>
    /// sceneName에 씬 이름을 입력하면 해당씬으로 이동.
    /// </summary>
    /// <param name="sceneName"></param>
    internal void ChangeScene(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);

        SceneManager.sceneLoaded += ResetPlayerPosition;
    }

    private void ResetPlayerPosition(Scene scene, LoadSceneMode mode)
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            player.transform.position = Vector3.zero;

        SceneManager.sceneLoaded -= ResetPlayerPosition;
    }
    /// <summary>
    /// 일시정지시 UI띄우기
    /// </summary>
    internal void Pause()
    {
        StopTime();
        PlayerUi.SetPauseUi(true);//여기서 UI가 생성되면서 문제가 생기는거 같음.
        systemPhase = Phase.Pause;
    }
    /// <summary>
    /// 종료시 시간 흐르게 하기
    /// </summary>
    internal void EndPause()
    {
        PlayerUi.SetPauseUi(false);//여기서 UI가 생성되면서 문제가 생기는거 같음.
        RunTime();
        systemPhase = Phase.Game;
    }
    /// <summary>
    /// 시간을 멈춘다.
    /// </summary>
    void StopTime()
    {
        Time.timeScale = Consts.none;
    }
    /// <summary>
    /// 시간을 다시 움직인다.
    /// </summary>
    void RunTime()
    {
        Time.timeScale = Consts.minValue;
    }
    /// <summary>
    /// 게임 종료
    /// </summary>
    internal void EndGame()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.ExitPlaymode();
        }
#endif
        Application.Quit();
    }
}
