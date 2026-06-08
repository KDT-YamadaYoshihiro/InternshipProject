using System;
using System.Collections; // コルーチンに必要
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnitListScene : SceneBase
{
    // ユニットの表示を担当するView
    [SerializeField] private UnitView unitView;
    // バナーテキスト
    [SerializeField] private Text bannerText;
    // 表示するユニットの数を制御するフィルタの値
    [SerializeField] private int displayCount = 10; 

    /// <summary>
    /// 現在のソートの状態を保持するフィールド
    /// </summary>
    private SortModel.SortState currentSortState = new SortModel.SortState(SortModel.SortType.Strength, SortModel.OrderType.Ascending);

    /// <summary>
    /// ソートの種類と順序を保持するモデル
    /// </summary>
    /// <param name="onFinished"></param>
    public override void OnSceneOpening(Action onFinished)
    {
        bannerText.text = "使用するユニットを選択してください。";
        RefreshList();
        onFinished?.Invoke();
    }

    /// <summary>
    /// データをソートしてViewに渡す処理
    /// </summary>
    private void RefreshList()
    {
        var data = ActorData.TestData.AsEnumerable();

        // 昇順・降順の判定フラグ
        bool isAscending = currentSortState.Order == SortModel.OrderType.Ascending;

        // 第1ソート: ソートの種類に応じてデータを並び替える
        Func<ActorData, int> sortFunc = currentSortState.Type switch
        {
            SortModel.SortType.Id => x => x.Id,
            SortModel.SortType.Atk => x => x.Atk,
            SortModel.SortType.Def => x => x.Def,
            SortModel.SortType.Hp => x => x.Hp,
            _ => x => x.Atk + x.Def // Strength
        };

        // 昇順・降順に応じてデータを並び替える
        IOrderedEnumerable<ActorData> orderedData = isAscending
                ? data.OrderBy(sortFunc) : data.OrderByDescending(sortFunc);

        // 第2ソート: Idで昇順に並び替える
        var finalData = orderedData.ThenBy(x => x.Id).Take(displayCount).ToList();

        // Viewにデータを渡して表示を更新
        unitView.UpdateUnitDisplay(finalData, currentSortState.Type, (selectedData) =>
        {
            StartCoroutine(UnitSelectionFlow(selectedData));
        });
    }


    /// <summary>
    /// 戻るボタンが押されたときの処理
    /// </summary>
    public void OnBackButtonClicked()
    {
        StartCoroutine(BackToTitleFlow());
    }

    /// <summary>
    /// 画面遷移のフローを管理するコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator BackToTitleFlow()
    {
        bool isConfirmed = false;
        bool isClosed = false;

        SceneTranslator.Instance.OpenDialog<MessageDialog>(onWillOpen: (dialog) =>
        {
            dialog.Setup(
                arg_onYes: () => isConfirmed = true,
                "タイトルに戻りますか？", "OK", "キャンセル"
            );
        }, onDidClose: () => isClosed = true);

        // ダイアログが閉じるまで待機
        while (!isClosed) { yield return null; }

        if (isConfirmed)
        {
            SceneTranslator.Instance.OpenScene<TitleScene>(onWillOpen: (scene) =>
            {
                scene.ReceiveParam.titleText = TitleScene.GenerateRandomTitle();
            });
        }
    }

    /// <summary>
    /// ソートボタンが押されたときの処理
    /// </summary>
    public void OnSortButtonClicked()
    {
        StartCoroutine(SortFlow());
    }

    /// <summary>
    /// ソートのフローを管理するコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator SortFlow()
    {
        SortModel.SortState resultState = default;
        bool isClosed = false;

        SceneTranslator.Instance.OpenDialog<SortDialog>(
                onWillOpen: (dialog) =>
                {
                    dialog.Setup(currentSortState);
                },
                onWillClose: (dialog) =>
                {
                    resultState = dialog.ResultState;
                },
                onDidClose: () => isClosed = true
                
        ); 
        
        // ダイアログが閉じるまで待機
        while (!isClosed) { yield return null; }

        //　ダイアログの結果を確認し、変更がある場合のみ更新
        if (!resultState.Equals(currentSortState))
        {
            currentSortState = resultState;
            RefreshList();
        }
        
    }

    /// <summary>
    /// フィルタボタンが押されたときの処理
    /// </summary>
    public void OnFilterButtonClicked()
    {
        StartCoroutine(FilterFlow());
    }

    /// <summary>
    /// フィルタのフローを管理するコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator FilterFlow()
    {
        int resultCount = 0;
        bool isClosed = false;

        // ダイアログを開く
        SceneTranslator.Instance.OpenDialog<FilterDialog>(
            onWillOpen: (dialog) =>
            {
                dialog.Setup(displayCount, "フィルタ", "設定", "-", "+");
            },
            onWillClose: (dialog) =>
            {
                resultCount = dialog.ResultCount;
            },
            onDidClose: () => isClosed = true 
        );

        // ダイアログが閉じるまで待機
        while (!isClosed) { yield return null; }

        // ダイアログの結果を確認し、変更がある場合のみ更新
        if (resultCount != displayCount)
        {
            displayCount = resultCount;
            RefreshList();
        }
    }

    /// <summary>
    /// バトル開始前の確認から遷移までのフロー
    /// </summary>
    private IEnumerator UnitSelectionFlow(ActorData data)
    {
        bool isConfirmed = false;
        bool isClosed = false;

        // ダイアログを開く
        SceneTranslator.Instance.OpenDialog<MessageDialog>(onWillOpen: (dialog) =>
        {
            dialog.Setup(
                arg_onYes: () => isConfirmed = true,
                $"「{data.Name}」で\nバトルを開始しますか？", "はい", "いいえ"
            );
        }, onDidClose: () => isClosed = true);

        // ダイアログが閉じるまで待機
        while (!isClosed) yield return null;

        // 判断に基づき、BattleSceneへ遷移
        if (isConfirmed)
        {
            SceneTranslator.Instance.OpenScene<BattleScene>(onWillOpen: (scene) =>
            {
                // BattleSceneへデータを渡す
                scene.SetBattleData(data);
            });
        }
    }


}