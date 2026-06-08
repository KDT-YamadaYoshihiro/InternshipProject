using UnityEngine;

/// <summary>
/// スライダーエリアのPresenter
/// ModelとViewの仲介、互いに影響するイベント設定などのみ行う
/// </summary>
public class SliderAreaPresenter
{
    // コンストラクタでModelとViewを受け取る
    public SliderAreaPresenter(SliderAreaModel model, SliderArea view)
    {
        this.model = model;
        this.view = view;
    }

    private SliderAreaModel model = null;
    private SliderArea view = null;

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        // Viewの初期範囲設定
        view.SetupSliderRange(model.Min, model.Max, model.SliderCount);
        view.SetValueText(model.SliderCount.ToString());

        model.OnSliderCountChanged = HandleModelChanged;

        // Viewのイベント登録
        view.OnSliderChangedEvent.AddListener(HandleViewSliderChanged);
        view.OnLeftBtnClick.AddListener(HandleLeftBtnClick);
        view.OnRightBtnClick.AddListener(HandleRightBtnClick);
    }

    /// <summary>
    /// 解放処理
    /// </summary>
    public void Release()
    {
        model.OnSliderCountChanged = null;
        view.OnSliderChangedEvent.RemoveListener(HandleViewSliderChanged);
        view.OnLeftBtnClick.RemoveListener(HandleLeftBtnClick);
        view.OnRightBtnClick.RemoveListener(HandleRightBtnClick);
    }

    /// <summary>
    /// Modelの値が変わったときの処理
    /// </summary>
    /// <param name="count"></param>
    private void HandleModelChanged(int count)
    {
        view.SetValueText(count.ToString());
        if (Mathf.RoundToInt(view.SliderValue) != count)
        {
            view.SliderValue = count;
        }
    }

    /// <summary>
    /// Viewのスライダーの値が変わったときの処理
    /// </summary>
    /// <param name="val"></param>
    private void HandleViewSliderChanged(float val) => model.SetSliderCount(Mathf.RoundToInt(val));
    private void HandleLeftBtnClick() => model.AddCount(-1);
    private void HandleRightBtnClick() => model.AddCount(1);
}
