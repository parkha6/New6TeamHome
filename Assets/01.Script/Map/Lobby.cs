using UnityEngine;
/// <summary>
/// 플레이어 UI세팅용 클래스
/// </summary>
public class Lobby : MonoBehaviour
{
    /// <summary>
    /// 로비에 들어오면 플레이어 UI를 세팅한다.
    /// </summary>
    private void Start()
    { 
       GameManager.Instance.PlayerUi.SetPlayerScreenUi(true); 
    }
}
