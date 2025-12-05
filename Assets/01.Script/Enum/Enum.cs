public enum SkinType
{
    Mantis, // 사마귀
    Grasshopper, // 메뚜기
    Cockroach // 바퀴벌레
}

public enum ItemType
{
    Currency,   // 재화 (골드, 조각들)
    Equipment   // 장비 (외피 같은 거)
}

public enum EvolutionStatType
{
    MaxHP,
    Attack,
    Defense,
    MoveSpeed
}
/// <summary>
/// 게임 상태 체크용
/// </summary>
public enum Phase
{
    Menu,//메인 매뉴
    Game,//게임 중
    Pause,//일시정지 중
}
/// <summary>
/// 이동시 씬 이름 처리용 enum
/// </summary>
public enum sceneName
{
    MainScene,
    StartLobby,
    Lobby,
    EasyStage,
    None,
}