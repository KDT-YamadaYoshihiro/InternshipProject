
// 複数のスクリプトで使用されるenum群
namespace Battle
{
    /// <summary>
    /// キャラのアクションの種類
    /// </summary>
    public enum ActionType
    {
        Rock,
        Paper,
        Scissor,
        Max
    }

    /// <summary>
    /// ジャンケンの勝敗結界（プレイヤー視点）
    /// </summary>
    public enum BattleResult
    {
        Draw,
        Win,
        Loss,
    }
}
