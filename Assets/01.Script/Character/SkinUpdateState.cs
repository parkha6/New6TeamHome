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
        {
            mantisLevel++;
            GameManager.Instance.PlayerUi.SetMenLevel(mantisLevel);
        }
        else if (skinType == SkinType.Grasshopper)
        {
            grasshopperLevel++;
            GameManager.Instance.PlayerUi.SetGraLevel(grasshopperLevel);
        }
        else if (skinType == SkinType.Cockroach)
        {
            cockroachLevel++;
            GameManager.Instance.PlayerUi.SetCockLevel(cockroachLevel);
        }
    }

    public int GetLevel(SkinType skinType)
    {
        if (skinType == SkinType.Mantis)
            return mantisLevel;
        else if (skinType == SkinType.Grasshopper)
            return grasshopperLevel;
        else if (skinType == SkinType.Cockroach)
            return cockroachLevel;

        return 0;
    }

    public int GetLevel(SkinEnhanceData data)
    {
        return GetLevel(data.skinType);
    }
}
