using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : SceneBase
{
	[SerializeField]
	CanvasGroup canvasGroup = null;
	[SerializeField]
	Text titleText = null;

    [Serializable]
	public class ReceiveParameter
	{
		public string titleText = null;
	}
	[Space(10)]
	[SerializeField]
	public ReceiveParameter ReceiveParam = null;

    /// <summary>
    /// タイトル生成
    /// </summary>
    /// <returns></returns>
    public static string GenerateRandomTitle()
    {
        var numbers = new string[] { "Ⅰ", "Ⅱ", "Ⅲ", "Ⅳ", "Ⅴ", "Ⅵ", "Ⅶ", "Ⅷ", "Ⅸ", "Ⅹ" };
        return $"最終幻想.{numbers[UnityEngine.Random.Range(0, numbers.Length)]}";
    }

    /// <summary>
    /// 表示非表示と操作権限を切り替える。
    /// </summary>
    /// <param name="value"></param>
    public override void SetSceneActive(bool value)
	{
		canvasGroup.alpha = value ? 1f : 0f;
		canvasGroup.interactable = value;
		canvasGroup.blocksRaycasts = value;
	}

    /// <summary>
    /// シーンを開くとき
    /// </summary>
    /// <param name="onFinished"></param>
	public override void OnSceneOpening(Action onFinished)
	{
        if (titleText != null && ReceiveParam != null)
        {
            // パラメータが空ならデフォルトのタイトルを生成して表示
            titleText.text = string.IsNullOrEmpty(ReceiveParam.titleText)
                ? GenerateRandomTitle()
                : ReceiveParam.titleText;
        }
        onFinished?.Invoke();
	}

    /// <summary>
    /// シーンを閉じるとき
    /// </summary>
    /// <param name="onFinished"></param>
	public override void OnSceneClosing(Action onFinished)
	{
		onFinished?.Invoke();
	}

	/// <summary>
    /// シーン切り替えメソッド
    /// </summary>
	public void OnSceneTapped()
	{
        StartCoroutine(ToUnitListFlow());
	}

    /// <summary>
    /// 画面遷移のフローを管理するコルーチン
    /// </summary>
    /// <returns></returns>
	private IEnumerator ToUnitListFlow()
    {
        bool isConfirmed = false;
        bool isClosed = false;

        SceneTranslator.Instance.OpenDialog<MessageDialog>(onWillOpen: (dialog) =>
        {
            dialog.Setup(
                arg_onYes: () => isConfirmed = true,
                 "ゲームを開始しますか？", "はい", "いいえ"
            );
        }, onDidClose: () => isClosed = true);

        // ダイアログが完全に閉じるまで待機
        while (!isClosed) yield return null;

        if (isConfirmed)
        {
            SceneTranslator.Instance.OpenScene<UnitListScene>();
        }
    }

}
