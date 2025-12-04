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
    [SerializeField] GameObject rewardSkin;
    /// <summary>
    /// 사마귀 프리팹
    /// </summary>
    [SerializeField] GameObject ManPrefab;
    /// <summary>
    /// 메뚜기 프리팹
    /// </summary>
    [SerializeField] GameObject GraPrefab;
    /// <summary>
    /// 작동시 스테이지 랜덤배치
    /// </summary>
    GameObject MantisSkin;
    GameObject GrassSkin;
    internal void SetStage()
    {
        ArrRandom(enters);
        ArrRandom(leftPatterns);
        ArrRandom(rightPatterns);
        ArrRandom(exits);
        ArrSkin();

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
    void ArrSkin()
    {
        int whichOne = Random.Range(Consts.none, Consts.dropSkinAmount);
        if (whichOne == Consts.none)
        {
            if (MantisSkin == null)
            {
                MantisSkin = Instantiate(ManPrefab);
                MantisSkin.transform.position = rewardSkin.transform.position;
            }
            else
            {
                MantisSkin.SetActive(true);
            }
            if (GrassSkin != null)
                GrassSkin.SetActive(false);
        }
        else if (whichOne == Consts.minValue)
        {
            if (GrassSkin == null)
            {
                GrassSkin = Instantiate(GraPrefab);
                GrassSkin.transform.position = rewardSkin.transform.position;
            }
            else
            {
                GrassSkin.SetActive(true);
            }
            if (MantisSkin != null)
                MantisSkin.SetActive(false);
        }
    }
}
