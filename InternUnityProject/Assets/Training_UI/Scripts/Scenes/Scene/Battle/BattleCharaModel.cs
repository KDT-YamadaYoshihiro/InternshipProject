using UnityEditor;
using UnityEngine.Events;

/// <summary>
/// バトルキャラのモデル
/// キャラに必要な変数、更新する関数、イベントを持つ
/// </summary>
public class BattleCharaModel
{
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="data"></param>
    public BattleCharaModel(ActorData data)
    {
        param = new ActorData(data);
        defaultParam = new ActorData(data);
    }

    public UnityEvent<float> OnHpChanged = new UnityEvent<float>();

    private ActorData param = null;

    private readonly ActorData defaultParam = null;

    private int SelectedActionIndex = -1;

    private float currentMultiplier = 1.0f;

    /// <summary>
    /// キャラの名前、HP、攻撃力を取得する関数
    /// </summary>
    /// <returns></returns>
    public string GetName() => param.Name;
    public int GetHp() => param.Hp;
    public int GetAtk() => param.Atk;
    public int GetMaxHp() => defaultParam.Hp;
    public int GetSelectedActionIndex() => SelectedActionIndex;
    public float GetMultiplier() => currentMultiplier;

    /// <summary>
    /// 選択したアクションのインデックスを設定する
    /// </summary>
    public void SetSelectedAction(int index)
    {
        SelectedActionIndex = index;
    }

    /// <summary>
    /// ダメージ倍率を加算する
    /// </summary>
    /// <param name="value"></param>
    public void AddMultiplier(float value)
    {
        currentMultiplier += value;
    }

    /// <summary>
    /// ダメージ倍率をリセットする
    /// </summary>
    public void ResetMultiplier()
    {
        currentMultiplier = 1.0f;
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="amount"></param>
    public void Damage(int amount)
    {
        // HPの更新
        param.Hp -= amount;
        if (param.Hp < 0) { param.Hp = 0; }
        
        // イベント発火
        float ratio = (float)param.Hp / defaultParam.Hp;
        // nullチェック込み
        OnHpChanged?.Invoke(ratio);
    }

}