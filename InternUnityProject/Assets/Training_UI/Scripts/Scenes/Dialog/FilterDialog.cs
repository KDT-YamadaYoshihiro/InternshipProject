using System;
using UnityEngine;
using UnityEngine.UI;

public class FilterDialog : DialogBase
{
    // スライダーエリアのViewを直接参照
    [SerializeField] private SliderArea sliderArea = null;
    // タイトルテキスト
    [SerializeField] private Text titleText = null;
    // 設定ボタンを直接参照
    [SerializeField] private Button applyButton = null;
    // 設定ボタンのテキスト
    [SerializeField] private Text decideText = null;        

    private SliderAreaModel sliderAreaModel = null;
    private SliderAreaPresenter sliderAreaPresenter = null;

    // 結果を保持
    public int ResultCount { get; private set; }

    public override SceneTypes SceneType => SceneTypes.Dialog;

    /// <summary>
    /// シーンを開くときの処理
    /// </summary>
    /// <param name="onFinished"></param>
    public override void OnSceneOpening(Action onFinished)
    {
        applyButton.onClick.RemoveAllListeners();
        applyButton.onClick.AddListener(HandleApplyButton);
        
        base.OnSceneOpening(onFinished);
    }

    /// <summary>
    /// シーンを閉じるときの処理
    /// </summary>
    /// <param name="onFinished"></param>
    public override void OnSceneClosing(Action onFinished)
    {
        applyButton.onClick.RemoveAllListeners();
        sliderAreaPresenter?.Release();
        base.OnSceneClosing(onFinished);
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="arg_currentCount">表示件数</param>
    /// <param name="arg_titleText">タイトルテキスト</param>
    /// <param name="arg_decideText">決定ボタンテキスト</param>
    /// <param name="arg_leftBtnText">左ボタンテキスト</param>
    /// <param name="arg_rightBtnText">右ボタンテキスト</param>
    public void Setup(int arg_currentCount, string arg_titleText, string arg_decideText, string arg_leftBtnText, string arg_rightBtnText)
    {
        ClearResult();

        // スライダーエリアのModelとPresenterを初期化
        sliderAreaModel = new SliderAreaModel(arg_currentCount, 1, ActorData.TestData.Count);
        sliderAreaPresenter = new SliderAreaPresenter(sliderAreaModel, sliderArea);

        // テキストの設定
        this.titleText.text = arg_titleText;
        this.decideText.text = arg_decideText;
        sliderArea.SetLeftBtnText(arg_leftBtnText);
        sliderArea.SetRightBtnText(arg_rightBtnText);

        // スライダーエリアのPresenterを初期化
        sliderAreaPresenter.Initialize();
    }

    /// <summary>
    /// 「設定」ボタンがクリックされたときの処理
    /// </summary>
    private void HandleApplyButton()
    {
        // 状態を確定させてから閉じる。更新処理は呼ばない。
        ResultCount = sliderAreaModel.SliderCount;

        SceneTranslator.Instance.CloseDialog();
    }

    /// <summary>
    /// 結果をクリアする処理
    /// </summary>
    public void ClearResult()
    {
        ResultCount = 0;
    }
}