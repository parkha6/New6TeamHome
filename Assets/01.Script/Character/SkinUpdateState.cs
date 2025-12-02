using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinUpdateState : MonoBehaviour
{
    public int mantisLevel;
    public int grasshopperLevel;
    public int cockroachLevel;

    public void IncreaseLevel(SkinType skinType)
    {
        // switch나 if로 분기해서 해당 레벨만 올려주기
        if (skinType == SkinType.Mantis)
            mantisLevel++;
        else if (skinType == SkinType.Grasshopper)
            grasshopperLevel++;
        else if (skinType == SkinType.Cockroach)
            cockroachLevel++;
    }
}
