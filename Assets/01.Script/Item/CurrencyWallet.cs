using UnityEngine;

public class CurrencyWallet : MonoBehaviour
{
    public int gold;
    public int skin_piece;
    public int wing_piece;

    // 각각 어떤 재화인지 연결해둘 SO
    public ItemData goldItem;
    public ItemData skin_pieceItem;
    public ItemData wing_pieceItem;

    public void AddCurrency(ItemData item, int amount)
    {
        // TODO: 들어온 item이 어떤 재화인지 비교해서 알맞은 값에 더하기
        if (item == goldItem)
        {
            gold += amount;
            GameManager.Instance.PlayerUi.SetGoldScore(gold);
        }
        else if (item == skin_pieceItem)
        {
            skin_piece += amount;
            GameManager.Instance.PlayerUi.SetSkinScore(skin_piece);
        }
        else if (item == wing_pieceItem)
        {
            wing_piece += amount;
            GameManager.Instance.PlayerUi.SetWingScore(wing_piece);
        }
    }

    public bool TrySpendCurrency(ItemData item, int amount)
    {
        // 1) 해당 재화를 얼마나 갖고 있는지 확인
        // 2) 부족하면 false 반환
        // 3) 충분하면 빼고 true 반환
        if (item == goldItem)
        {
            if (gold >= amount)
            {
                gold -= amount;
                GameManager.Instance.PlayerUi.SetGoldScore(gold);
                return true;
            }
            return false;
        }
        else if (item == skin_pieceItem)
        {
            if (skin_piece >= amount)
            {
                skin_piece -= amount;
                GameManager.Instance.PlayerUi.SetSkinScore(skin_piece);
                return true;
            }
            return false;
        }
        else if (item == wing_pieceItem)
        {
            if (wing_piece >= amount)
            {
                wing_piece -= amount;
                GameManager.Instance.PlayerUi.SetWingScore(wing_piece);
                return true;
            }
            return false;
        }
        // 모든 경로에서 값을 반환하도록 수정
        return false;
    }

    public bool HasCurrency(ItemData item, int amount)
    {
        if (item == goldItem)
        {
            return gold >= amount;
        }
        else if (item == skin_pieceItem)
        {
            return skin_piece >= amount;
        }
        else if (item == wing_pieceItem)
        {
            return wing_piece >= amount;
        }

        // 어느 것도 해당 안 되면 false
        return false;
    }
    /// <summary>
    /// 시작 함수에서 스코어 입력
    /// </summary>
    private void Start()
    {
        Debug.Log("스코어 삽입");
        GameManager.Instance.PlayerUi.SetScoreText(wing_piece, skin_piece, gold);
    }
}

