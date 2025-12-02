using UnityEngine;
/// <summary>
/// 문의 이동장소를 관리하는 클래스
/// </summary>
public class DoorManager : MonoSingleton<DoorManager>
{
    /// <summary>
    /// 문을 열면 이동하는 씬
    /// </summary>
    string nextRoom;
    /// <summary>
    /// 문인가?
    /// </summary>
    bool isDoor;
    /// <summary>
    /// 문에 들어가는 함수
    /// </summary>
    internal void IntoDoor()
    {
        if (isDoor)
            GameManager.Instance.ChangeScene(nextRoom);
    }
    /// <summary>
    /// 문에서 다음 씬을 입력하기 위한 함수
    /// </summary>
    /// <param name="inputNextRoom"></param>
    internal void DoorInfo(string inputNextRoom)
    {
        isDoor = true;
        nextRoom = inputNextRoom;
    }
    /// <summary>
    /// 문이 아닐 시 작동
    /// </summary>
    internal void NotDoor()
    { isDoor = false; }
}
