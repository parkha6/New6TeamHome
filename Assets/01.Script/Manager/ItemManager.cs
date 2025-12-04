using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // 외피 ScriptableObject
    public EquipmentItemData currentskin;

    // 현재 외피 타입
    public SkinType currentSkin;

    // (이미 만들었던 외피 강화 레벨 정보)
    public SkinUpdateState skinState;

    public SpriteRenderer bodyRenderer;     // 플레이어 오브젝트의 SpriteRenderer

    // 🔹 SO를 받아서 둘 다 세팅하는 버전
    public void EquipSkin(EquipmentItemData newSkinData)
    {
        // SO 그대로 저장
        currentskin = newSkinData;

        // SO 안에 어떤 외피 타입인지 들어있다고 가정 (EquipmentItemData.skinType)
        currentSkin = newSkinData.skinType;

        GameManager.Instance.PlayerUi.SkinNSkillIcon(currentskin.icon, currentskin.skillIcon1, currentskin.skillIcon2);
        // 스프라이트 교체
        if (bodyRenderer != null && newSkinData.icon != null)
        {
            bodyRenderer.sprite = newSkinData.icon;
        }

        Debug.Log($"외피 교체: {newSkinData.itemName} / 타입: {currentSkin}");
        PlayerSkillController.Instance.SetState(currentSkin);
    }

    public void OnSkinItemPickedUp(EquipmentItemData skinData)
    {
        // 나중에 여기서 "교체할 때 효과" 같은 것도 추가 가능
        EquipSkin(skinData);
        PlayerManager.Instance.EquipItem(skinData);

    }

    private void Start()
    {
        GameManager.Instance.PlayerUi.SkinNSkillIcon(currentskin.icon, currentskin.skillIcon1, currentskin.skillIcon2);
    }
}
