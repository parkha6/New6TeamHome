using UnityEngine;
/// <summary>
/// 스테이지 랜덤배치용
/// </summary>
public class EasyRandom : MonoBehaviour
{
    /// <summary>
    /// 입구 패턴
    /// </summary>
    [SerializeField] GameObject[] enters;
    /// <summary>
    /// 전투구역 좌측 패턴 배열
    /// </summary>
    [SerializeField] GameObject[] leftPatterns;
    /// <summary>
    /// 전투구역 우측 패턴 배열
    /// </summary>
    [SerializeField] GameObject[] rightPatterns;
    /// <summary>
    /// 출구 패턴
    /// </summary>
    [SerializeField] GameObject[] exits;
    /// <summary>
    /// 보상 아이템
    /// </summary>
    [SerializeField] GameObject[] rewardSkin;
    /// <summary>
    /// 생성시 스테이지 랜덤배치
    /// </summary>
    private void Awake()
    {
        ArrRandom(enters);
        ArrRandom(leftPatterns);
        ArrRandom(rightPatterns);
        ArrRandom(exits);
        ArrRandom(rewardSkin);
        GameManager.Instance.PlayerUi.SetPlayerScreenUi(false);
    }
    /// <summary>
    /// 지정된 게임오브젝트 배열중 하나만 활성화
    /// </summary>
    void ArrRandom(GameObject[] targetObjs)
    {
        int whichOne = Random.Range(Consts.none, targetObjs.Length);
        for (int i = Consts.none; i < targetObjs.Length; ++i)
        {
            if (i == whichOne)
            { 
                targetObjs[i].SetActive(true); 
            }
            else
            { 
                targetObjs[i].SetActive(false); 
            }
        }
    }
}
