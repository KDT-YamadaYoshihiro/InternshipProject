/// <summary>
/// バトルキャラのモデルを扱うコントローラー
/// キャラのアクション、ステータスの更新などのメソッドをここで行う
/// </summary>
public class BattleCharaModelController
{

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="model"></param>
    public BattleCharaModelController(BattleCharaModel model)
    {
        this.model = model;
    }

    private BattleCharaModel model = null;
    public BattleCharaModel Model => model;

    /// <summary>
    /// 死亡判定
    /// </summary>
    /// <returns></returns>
    public bool IsAlive()
    {
        return model.GetHp() > 0;
    }
}
