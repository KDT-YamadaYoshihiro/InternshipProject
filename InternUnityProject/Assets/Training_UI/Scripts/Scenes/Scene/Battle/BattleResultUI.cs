using UnityEngine;
using UnityEngine.UI;

public class BattleResultUI : MonoBehaviour
{
    [SerializeField] private Text finalResultMessage = null;

    /// <summary>
    /// バトル開始時に呼ばれる初期化
    /// </summary>
    public void Setup()
    {
        if (finalResultMessage != null) { finalResultMessage.text = string.Empty; }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 勝敗結果のテキスト設定と表示
    /// </summary>
    /// <param name="isPlayerWin"></param>
    public void ShowBattleResult(bool isPlayerWin)
    {
        gameObject.SetActive(true);
        finalResultMessage.text = isPlayerWin ? "Player VICTORY" : "Player DEFEAT";
    }
}