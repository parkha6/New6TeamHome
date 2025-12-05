
using UnityEngine;

[CreateAssetMenu(menuName = "Data/EquipmentItem")]
public class EquipmentItemData : ScriptableObject
{
    [Header("기본 정보")]
    public int id;             // 20004 ~ 20009 같은 ID
    public string itemName;    // mantis_weapon 등
    public Sprite icon;        // 인게임 표시용
    public Sprite skillIcon1; // 스킬 아이콘1
    public Sprite skillIcon2; // 스킬 아이콘2
    public Sprite playerSkin;

    public ItemType itemType;    // Currency / Equipment 구분
    public SkinType skinType;    // 어떤 외피인지 구분

    [Header("능력치 보정 값")]
    public float atkValue;     // Atk_Value
    public float defValue;     // Def_Value
    public float spdValue;     // Spd_Value
    public float hpValue;      // HP_Value
}
