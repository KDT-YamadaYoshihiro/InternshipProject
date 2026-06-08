using UnityEngine;
using Battle;
/// <summary>
/// バトルのモデルを扱うコントローラー
/// プレイヤーと敵の攻撃、勝敗などのメソッドをここで行う
/// </summary>
public class BattleModelController
{

    /// <summary>
    /// BattleModelControllerのコンストラクタ
    /// </summary>
    /// <param name="model"></param>
    public BattleModelController(BattleModel model)
    {
        this.player = new BattleCharaModelController(model.Player);
        this.enemy = new BattleCharaModelController(model.Enemy);
    }

    private BattleCharaModelController player = null;
    public BattleCharaModelController Player => player;
    private BattleCharaModelController enemy = null;
    public BattleCharaModelController Enemy => enemy;

    /// <summary>
    /// バトルが終了しているかを返す
    /// </summary>
    /// <returns></returns>
    public bool IsDoneBattle()
    {
        return !player.IsAlive() || !enemy.IsAlive();
    }

    /// <summary>
    /// 敵のアクション（手）をランダムに決定する
    /// </summary>
    public int GetEnemyAction()
    {
        return UnityEngine.Random.Range(0, (int)Battle.ActionType.Max); //
    }

    /// <summary>
    /// ジャンケンの判定
    /// 0: あいこ, 1: プレイヤーの勝ち, 2: 敵の勝ち
    /// </summary>
    public BattleResult Judge(int playerAction, int enemyAction)
    {
        if (playerAction == enemyAction) { return BattleResult.Draw; }

        // Rock(0), Scissor(1), Paper(2) の定義に基づいた判定
        if ((playerAction == (int)Battle.ActionType.Rock && enemyAction == (int)Battle.ActionType.Scissor) ||
            (playerAction == (int)Battle.ActionType.Paper && enemyAction == (int)Battle.ActionType.Rock) || 
            (playerAction == (int)Battle.ActionType.Scissor && enemyAction == (int)Battle.ActionType.Paper))

        {
            // プレイヤー勝利
            return BattleResult.Win; 
        }
        // 敵勝利
        return BattleResult.Loss; 
    }

    /// <summary>
    /// 計算でジャンケンの勝敗判定
    /// </summary>
    /// <param name="playerAction"></param>
    /// <param name="enemyAction"></param>
    /// <returns></returns>
    public BattleResult Janken(int playerAction, int enemyAction)
    {
        // 剰余演算を用いた勝敗判定
        // 結果 0: あいこ, 1: プレイヤー勝利, 2: 敵の勝利
        int result = (playerAction - enemyAction + 3) % 3;

        return (BattleResult)result;
    }

    /// <summary>
    /// 勝敗結果に基づいて攻撃を実行する
    /// result: 1(Player勝), 2(Enemy勝), 0(あいこ)
    /// </summary>
    public void ExecuteResult(BattleResult arg_result)
    {
        switch (arg_result)
        {
            case BattleResult.Win:
                // 倍率変更
                player.Model.AddMultiplier(0.5f);
                enemy.Model.ResetMultiplier();
                // 攻撃
                Attack(player, enemy, player.Model.GetMultiplier());
                break;
            case BattleResult.Loss:
                // 倍率変更
                player.Model.ResetMultiplier();
                enemy.Model.AddMultiplier(0.5f);
                // 攻撃
                Attack(enemy, player, enemy.Model.GetMultiplier());
                break;
            default:
                // あいこの場合は双方倍率を少し上げる
                player.Model.AddMultiplier(0.2f);
                enemy.Model.AddMultiplier(0.2f);
                break;

        }
    }

    /// <summary>
    /// 攻撃処理
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    private void Attack(BattleCharaModelController attacker, BattleCharaModelController defender, float arg_multiplier)
    {
        int damage =Mathf.CeilToInt(attacker.Model.GetAtk() * arg_multiplier);
        defender.Model.Damage(damage);

        Debug.Log($"{attacker.Model.GetName()}の攻撃！倍率:{arg_multiplier}x 総ダメージ:{damage}");
    }
}
