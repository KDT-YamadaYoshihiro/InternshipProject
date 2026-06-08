using Battle;
using GameCommon;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// バトルフロー
/// </summary>
public class BattleFlow
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="modelController"></param>
    /// <param name="viewController"></param>
    public BattleFlow(BattleModelController modelController, BattleViewController viewController)
    {
        this.modelController = modelController;
        this.viewController = viewController;
    }

    /// <summary>
    /// ターンの種類
    /// </summary>
    private enum TurnType
    {
        Start = 0,
        Action,
        End,
    };

    private TurnType turnType;
    private BattleModelController modelController = null;
    private BattleViewController viewController = null;
    private int turnCount = 0;

    /// <summary>
    /// バトルのコルーチン
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    public IEnumerator BattleCoroutine(UnityAction callback = null)
    {
        // Viewの初期化
        viewController.SetupAllUI();

        while (!modelController.IsDoneBattle())
        {
            switch (turnType)
            {
                case TurnType.Start:
                    yield return TurnStart();
                    break;
                case TurnType.Action:
                    yield return TurnAction();
                    break;
                case TurnType.End:
                    yield return TurnEnd();
                    break;
            }
        }

        // バトル終了時
        bool isPlayerWin = modelController.Player.IsAlive();
        viewController.BattleResultUI.ShowBattleResult(isPlayerWin);

        callback?.Invoke();
        yield break;
    }

    /// <summary>
    /// ターン開始の処理
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator TurnStart(UnityAction callback = null)
    {
        // ターン数を表記
        Debug.Log("==========");
        Debug.Log($"{turnCount + 1} ターン目");
        viewController.SetTurnText($"{turnCount + 1} ターン目");
        viewController.SetActiveTurnText(true);

        // 待機時間
        yield return new WaitForSeconds(WaitDuration.Normal);

        // 次のアクションへ
        viewController.SetActiveTurnText(false);
        turnType = TurnType.Action;
        yield break;
    }

    /// <summary>
    /// アクションターンの処理
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator TurnAction(UnityAction callback = null)
    {
        // 入力待機
        viewController.ActionButton.SetInteractable(true);
        yield return InputManager.WaitClickBtn(viewController.ActionButton.GetBtns(), () => viewController.ActionButton.SetInteractable(false));

        // 選択処理
        // プレイヤーの選択を表示
        int playerSelectIndex = modelController.Player.Model.GetSelectedActionIndex();
        viewController.PlayerActionUI.SetActionText(playerSelectIndex);

        // 敵のアクションを選択・表示
        int enemySelectIndex = modelController.GetEnemyAction();
        yield return new WaitForSeconds(WaitDuration.Quick);
        viewController.EnemyActionUI.SetActionText(enemySelectIndex);

        // 待機時間
        yield return new WaitForSeconds(WaitDuration.Quick);

        // 勝敗判定・結果表示
        BattleResult result = modelController.Janken(playerSelectIndex, enemySelectIndex);
        string[] resultMsgs = { "あいこ", "プレイヤーの勝利！", "敵の勝利..." };
        Debug.Log($"結果: {resultMsgs[(int)result]}");
        viewController.TurnResultUI.ShowResult(result);

        // 待機時間
        yield return new WaitForSeconds(WaitDuration.Quick);

        // 表示内容を消す
        viewController.TurnResultUI.Hide();

        // アクション
        // ダメージ計算実行
        modelController.ExecuteResult(result);

        viewController.PlayerInfoUI.UpdateMultiplier(modelController.Player.Model.GetMultiplier());
        viewController.EnemyInfoUI.UpdateMultiplier(modelController.Enemy.Model.GetMultiplier());

        // HPバーの更新
        UpdateAllHpBars();

        // 待機時間
        yield return new WaitForSeconds(WaitDuration.Quick);

        // 次のアクションへ
        turnType = TurnType.End;
        yield break;
    }

    /// <summary>
    /// ターン終了の処理
    /// </summary>
    /// <param name="callback"></param>
    /// <returns></returns>
    private IEnumerator TurnEnd(UnityAction callback = null)
    {
        // 選択テキストをクリア
        viewController.PlayerActionUI.ClearActionText();
        viewController.EnemyActionUI.ClearActionText();

        // 待機時間
        yield return new WaitForSeconds(WaitDuration.Quick);

        // 次のアクションへ
        turnType = TurnType.Start;
        turnCount++;
        yield break;
    }

    /// <summary>
    /// 両者のHPバーを最新の状態に更新する
    /// </summary>
    private void UpdateAllHpBars()
    {
        // プレイヤー
        var pModel = modelController.Player.Model;
        float pRatio = (float)pModel.GetHp() / pModel.GetMaxHp();
        viewController.PlayerInfoUI.UpdateHpBar(pRatio);

        // 敵
        var eModel = modelController.Enemy.Model;
        float eRatio = (float)eModel.GetHp() / eModel.GetMaxHp();
        viewController.EnemyInfoUI.UpdateHpBar(eRatio);
    }

}
