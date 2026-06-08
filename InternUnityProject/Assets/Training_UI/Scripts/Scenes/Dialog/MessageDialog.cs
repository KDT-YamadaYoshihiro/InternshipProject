using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : DialogBase
{
    public override SceneTypes SceneType => SceneTypes.Dialog;

    [Header("UI References")]
    [SerializeField] private Text messageText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;
    // Yesボタンのテキスト
    [SerializeField] private Text yesButtonText;
    // Noボタンのテキスト
    [SerializeField] private Text noButtonText;
    private Action onYesCallback; // 呼び出し元から渡される処理を保持

    /// <summary>
    /// 初期化処理
    /// </summary>
    protected override void Awake()
    {
        // 親クラスのAwakeを呼び出す
        base.Awake();

        // ボタンのクリックイベントにリスナーを追加
        yesButton.onClick.AddListener(() => {
            SceneTranslator.Instance.CloseDialog();
            onYesCallback?.Invoke();
        });
        noButton.onClick.AddListener(() => SceneTranslator.Instance.CloseDialog());
    }

    /// <summary>
    /// 初期化メソッド
    /// </summary>
    /// <param name="arg_onYes"></param>
    /// <param name="arg_message"></param>
    /// <param name="arg_yesText"></param>
    /// <param name="arg_noText"></param>
    public void Setup(Action arg_onYes, string arg_message, string arg_yesText = "はい", string arg_noText = "いいえ")
    {
        onYesCallback = arg_onYes;
        messageText.text = arg_message;
        yesButtonText.text = arg_yesText;
        noButtonText.text = arg_noText;
    }

}
