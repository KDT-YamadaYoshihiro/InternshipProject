using Battle;
using UnityEngine;
using UnityEngine.UI;

public class TurnResultUI : MonoBehaviour
{
    
    [SerializeField] private Text resultText = null;

    /// <summary>
    /// バトル開始時に呼ばれる初期化
    /// </summary>
    public void Setup()
    {
        if (resultText != null) { resultText.text = string.Empty; }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 勝敗判定の表示
    /// </summary>
    /// <param name="arg_result"></param>
    public void ShowResult(BattleResult arg_result)
    {
        gameObject.SetActive(true);
        switch (arg_result)
        {
            case BattleResult.Draw: resultText.text = "DRAW"; break;
            case BattleResult.Win: resultText.text = "Player Win!"; break;
            case BattleResult.Loss: resultText.text = "Player LOSE..."; break;
        }

    }

    /// <summary>
    /// 非表示にする
    /// </summary>
    /// <returns></returns>
    public void Hide()
    {
        gameObject.SetActive(false);
        if (resultText != null) { resultText.text = string.Empty; }
    }
}