/// <summary>
/// バトルのモデル
/// </summary>
public class BattleModel
{
    public BattleModel(ActorData player, ActorData enemy)
    {
        this.player = new BattleCharaModel(player);
        this.enemy = new BattleCharaModel(enemy);
    }

    private BattleCharaModel player = null;
    public BattleCharaModel Player => player;
    private BattleCharaModel enemy = null;
    public BattleCharaModel Enemy => enemy;
}
