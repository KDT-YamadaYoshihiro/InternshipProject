using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラ情報のUI
/// </summary>
public class CharaInfoUI : MonoBehaviour
{
    [SerializeField]
    private Text charaName = null;
    [SerializeField]
    private HpBar hpBar = null;
    [SerializeField]
    private Text multiplierText = null;

    /// <summary>
    /// UIの初期セットアップ（バトル開始時に呼ぶ）
    /// </summary>
    public void Setup()
    {
        // 開始時は1.0倍なので非表示にする
        if (multiplierText != null)
        {
            multiplierText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// キャラの名前を設定する
    /// </summary>
    /// <param name="text"></param>
    public void SetCharaName(string text)
    {
        charaName.text = text;
    }

    /// <summary>
    /// HPバーをリセットする
    /// </summary>
    public void ResetHpBar()
    {
        hpBar.SetRedGauge(1);
        hpBar.SetGreenGauge(1);
    }

    /// <summary>
    /// HPバーの緑のゲージを更新する
    /// </summary>
    /// <param name="fillAmount"></param>
    public void UpdateHpBar(float fillAmount)
    {
        hpBar.SetGreenGauge(fillAmount);
    }

    /// <summary>
    /// ダメージ倍率のテキストを更新する
    /// </summary>
    /// <param name="arg_multiplier"></param>
    public void UpdateMultiplier(float arg_multiplier)
    {
        if (multiplierText == null) return;

        // 1.0倍より大きい時だけ表示
        bool isActive = arg_multiplier > 1.0f;
        multiplierText.gameObject.SetActive(isActive);

        if (isActive)
        {
            multiplierText.text = $"{arg_multiplier:F1}x";
        }
    }
}
