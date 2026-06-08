using Battle;

/// <summary>
/// バトルのプレゼンター
/// モデルとビューを繋ぐ
/// </summary>
public class BattlePresenter
{
    public BattlePresenter(BattleModelController modelController, BattleViewController viewController)
    {
        this.modelController = modelController;
        this.viewController = viewController;
    }

    private BattleModelController modelController = null;
    private BattleViewController viewController = null;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        // プレイヤー情報の初期化
        var playerModel = modelController.Player.Model;
        // 名前をセット
        viewController.PlayerInfoUI.SetCharaName(playerModel.GetName());
        // HPバーをフル（1.0）の状態にリセット
        viewController.PlayerInfoUI.ResetHpBar();

        // 敵情報の初期化
        var enemyModel = modelController.Enemy.Model;
        // 名前をセット
        viewController.EnemyInfoUI.SetCharaName(enemyModel.GetName());
        // HPバーをリセット
        viewController.EnemyInfoUI.ResetHpBar();

        // その他の表示初期化
        // ターン数の表示設定
        viewController.SetTurnText("1 ターン目");
        viewController.SetActiveTurnText(true);

        // アクションボタン（グー・チョキ・パー）のテキストセット
        viewController.ActionButton.Initialize();

        // イベントの紐付け
        modelController.Player.Model.OnHpChanged.AddListener(viewController.PlayerInfoUI.UpdateHpBar);
        modelController.Enemy.Model.OnHpChanged.AddListener(viewController.EnemyInfoUI.UpdateHpBar);

        // アクションボタンのリスナーを一度だけ登録
        var actionButton = viewController.ActionButton;
        actionButton.OnClickBtnRock.AddListener(() => OnActionClicked((int)ActionType.Rock));
        actionButton.OnClickBtnPaper.AddListener(() => OnActionClicked((int)ActionType.Paper));
        actionButton.OnClickBtnScissor.AddListener(() => OnActionClicked((int)ActionType.Scissor));
    }

    /// <summary>
    /// モデルの値を更新するだけのメソッド
    /// </summary>
    private void OnActionClicked(int index)
    {
        modelController.Player.Model.SetSelectedAction(index);
    }
    /// <summary>
    /// ボタンとリスナーも解除
    /// </summary>
    public void Cleanup()
    {
        modelController.Player.Model.OnHpChanged.RemoveAllListeners();
        modelController.Enemy.Model.OnHpChanged.RemoveAllListeners();
    }

}
