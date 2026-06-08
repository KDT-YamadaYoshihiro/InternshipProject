using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スライダーエリアのView
/// スライダーに必要なGUIを持つ、GUIの制御を行う（Modelに依存しない）
/// </summary>
public class SliderArea : MonoBehaviour
{
    // スライダーの値を表示するテキスト
    [SerializeField]
    private Text countText = null;
    // 左ボタンのテキスト
    [SerializeField]
    private Text leftBtnText = null;
    // 左ボタン
    [SerializeField]
    private Button leftBtn = null;
    // 右ボタンのテキスト
    [SerializeField]
    private Text rightBtnText = null;
    // 右ボタン
    [SerializeField]
    private Button rightBtn = null;
    // スライダー
    [SerializeField]
    private Slider slider = null;       

    // スライダーの値の取得と設定
    public float SliderValue
    {
        get => slider.value;
        set => slider.value = value;
    }

    /// <summary>
    /// テキストの設定
    /// </summary>
    /// <param name="arg_text"></param>
    public void SetValueText(string arg_text) => countText.text = arg_text;
    public void SetLeftBtnText(string arg_text) => leftBtnText.text = arg_text;
    public void SetRightBtnText(string arg_text) => rightBtnText.text = arg_text;

    /// <summary>
    /// スライダーの範囲と初期値の設定
    /// </summary>
    /// <param name="arg_min"></param>
    /// <param name="arg_max"></param>
    /// <param name="arg_current"></param>
    public void SetupSliderRange(int arg_min, int arg_max, int arg_current)
    {
        slider.minValue = arg_min;
        slider.maxValue = arg_max;
        slider.value = arg_current;
    }

    // イベントの公開
    public Button.ButtonClickedEvent OnLeftBtnClick => leftBtn.onClick;
    public Button.ButtonClickedEvent OnRightBtnClick => rightBtn.onClick;
    public Slider.SliderEvent OnSliderChangedEvent => slider.onValueChanged;
}
