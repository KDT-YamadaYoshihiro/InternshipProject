using GameCommon;
using System;
using System.Collections;
using UnityEngine;

public class BattleScene : SceneBase
{

    [SerializeField]
    private BattleViewController viewController = null;

    private BattlePresenter presenter = null;
    private BattleFlow battleFlow = null;

    private ActorData playerUnitData;

    /// <summary>
    /// 遷移時に前のシーンから呼ばれるデータセット用メソッド
    /// </summary>
    public void SetBattleData(ActorData data)
    {
        playerUnitData = data;
    }

    /// <summary>
    /// シーンを開くときの処理
    /// </summary>
    /// <param name="onFinished"></param>
    public override void OnSceneOpening(Action onFinished)
    {
        // プレイヤーデータ
        var playerUnit = playerUnitData ?? ActorData.TestData[0];

        // 敵データのランダム設定
        var enemyList = ActorData.TestData;
        int randomIndex = UnityEngine.Random.Range(0, enemyList.Count);
        var randomEnemy = enemyList[randomIndex];

        // バトルのモデル
        var model = new BattleModel(playerUnit, randomEnemy);
        // バトルのモデルコントローラー
        var modelController = new BattleModelController(model);

        // バトルのプレゼンター
        presenter = new BattlePresenter(modelController, viewController);
        presenter.Initialize();

        // バトルフロー
        battleFlow = new BattleFlow(modelController, viewController);
        StartCoroutine(BattleSequence());

        onFinished?.Invoke();
    }

    /// <summary>
    /// シーンを閉じるときの処理
    /// </summary>
    /// <param name="onFinished"></param>
	public override void OnSceneClosing(Action onFinished)
	{
        // リスナーの解除
        presenter?.Cleanup();
        onFinished?.Invoke();
    }

    /// <summary>
    /// バトルの実行から終了後の遷移までを管理するフロー
    /// </summary>
    private IEnumerator BattleSequence()
    {
        // バトルフロー処理
        yield return battleFlow.BattleCoroutine();

        // 待機
        yield return new WaitForSeconds(WaitDuration.Dramatic);

        // シーン遷移を実行
        SceneTranslator.Instance.OpenScene<TitleScene>(onWillOpen: (scene) =>
        {
            // タイトルを設定
            scene.ReceiveParam.titleText = TitleScene.GenerateRandomTitle();
        });

    }
}
