
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;

    public ItemType itemType;    // Currency / Equipment 구분
    public SkinType skinType;    // 어떤 외피인지 구분
}
