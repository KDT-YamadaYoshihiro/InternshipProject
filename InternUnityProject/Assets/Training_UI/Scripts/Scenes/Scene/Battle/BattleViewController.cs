using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルのビューを扱うコントローラー
/// </summary>
public class BattleViewController : MonoBehaviour
{
    public Text TurnText = null;
    public CharaActionUI PlayerActionUI =null;
    public CharaInfoUI PlayerInfoUI = null;
    public CharaActionUI EnemyActionUI = null;
    public CharaInfoUI EnemyInfoUI = null;
    public BattleActionButton ActionButton = null;
    public TurnResultUI TurnResultUI = null;
    public BattleResultUI BattleResultUI = null;

    /// <summary>
    /// ターン数のテキストの表示・非表示を切り替える
    /// </summary>
    /// <param name="isActive"></param>
    public void SetActiveTurnText(bool isActive)
    {
        TurnText.gameObject.SetActive(isActive);
    }

    /// <summary>
    /// ターン数のテキストを設定する
    /// </summary>
    /// <param name="text"></param>
    public void SetTurnText(string text)
    {
        TurnText.text = text;
    }

    /// <summary>
    /// 全てのバトルUIを初期状態にする
    /// </summary>
    public void SetupAllUI()
    {
        TurnResultUI.Setup();
        BattleResultUI.Setup();

        // プレイヤーとエネミーの情報を初期化
        PlayerInfoUI.Setup();
        EnemyInfoUI.Setup();

        // 既存の要素も必要に応じて
        PlayerActionUI.ClearActionText();
        EnemyActionUI.ClearActionText();
        SetActiveTurnText(false);
    }
}
