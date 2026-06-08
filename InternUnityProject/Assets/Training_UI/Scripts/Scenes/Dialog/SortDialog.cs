using System;
using UnityEngine;

public class SortDialog : DialogBase
{
    public override SceneTypes SceneType => SceneTypes.Dialog;
    // ソートの種類と順序を選択するUIを直接参照
    [SerializeField] private SortDialogView view;

    // 初期状態を保持する変数
    private SortModel.SortState initialState;                  
    public SortModel.SortState ResultState { get; private set; }

    /// <summary>
    /// シーンを開く処理
    /// </summary>
    /// <param name="onFinished"></param>
    public override void OnSceneOpening(Action onFinished)
    {
        // UIの初期化
        view.InitializeTexts();
        view.SetToggleStates(initialState.Type, initialState.Order);
        view.OnSetButtonClicked += HandleSetButton;

        base.OnSceneOpening(onFinished);
    }

    /// <summary>
    /// シーンを閉じる処理
    /// </summary>
    /// <param name="onFinished"></param>
    public override void OnSceneClosing(Action onFinished)
    {
        view.OnSetButtonClicked -= HandleSetButton;
        base.OnSceneClosing(onFinished);
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="arg_currentState"></param>
    public void Setup(SortModel.SortState arg_currentState)
    {
        ResultState = default;
        initialState = arg_currentState;
    }

    /// <summary>
    /// 「設定」ボタンが押されたときの処理
    /// </summary>
    private void HandleSetButton()
    {
        // 状態を確定させる
        ResultState = new SortModel.SortState(GetCurrentSortType(), GetCurrentOrderType());

        SceneTranslator.Instance.CloseDialog();
    }


    /// <summary>
    /// 現在のソートの種類を取得する処理
    /// </summary>
    /// <returns></returns>
    private SortModel.SortType GetCurrentSortType()
    {
        if (view.toggleId.isOn)
        {
            return SortModel.SortType.Id;
        }
        if (view.toggleAtk.isOn)
        {
            return SortModel.SortType.Atk;
        }
        if (view.toggleDef.isOn)
        {
            return SortModel.SortType.Def;
        }
        if (view.toggleHp.isOn)
        {
            return SortModel.SortType.Hp;
        }
        return SortModel.SortType.Strength;
    }

    /// <summary>
    /// 現在のオーダータイプを取得する処理
    /// </summary>
    /// <returns></returns>
    private SortModel.OrderType GetCurrentOrderType()
    {
        return view.toggleDescending.isOn ? SortModel.OrderType.Descending : SortModel.OrderType.Ascending;
    }

}
