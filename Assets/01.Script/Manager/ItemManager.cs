using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public EquipmentItemData currentskin;
    public SkinType currentSkin;          // 지금 장착된 외피
    public SkinUpdateState skinState;     // 각 외피 강화 레벨 정보 (이미 만들었음)


    public void EquipSkin(SkinType newSkin)
    {
        // 1) currentSkin 바꾸고
        // 2) 외형/능력치 스크립트에 알려주기 (TODO: 팀원 코드랑 연결)
        currentSkin = newSkin;
        Debug.Log($"외피 교체: {newSkin}");
    }

    public void OnSkinItemPickedUp(SkinType skinType)
    {
        // 나중에 여기서 "교체할 때 효과" 같은 것도 추가 가능
        EquipSkin(skinType);
    }
}
