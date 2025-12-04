using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/HitBox")]
public class SkillHitBoxData : ScriptableObject
{
    [Header("HitBox Prefab")]
    public GameObject hitBoxPrefab;
}
