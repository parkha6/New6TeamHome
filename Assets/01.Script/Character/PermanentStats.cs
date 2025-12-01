
using UnityEngine;

public class PermanentStats : MonoBehaviour
{
    public int maxHpLevel;
    public int attackLevel;
    public int defenseLevel;
    public int moveSpeedLevel;

    public void IncreaseMaxHealthLevel()
    {
        maxHpLevel++;
    }

    public void IncreaseAttackLevel()
    {
        attackLevel++;
    }

    public void IncreaseDefenseLevel()
    {
        defenseLevel++;
    }

    public void IncreaseSpeedLevel()
    {
        moveSpeedLevel++;
    }
}
