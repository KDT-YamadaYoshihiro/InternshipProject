using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルのアクションボタン
/// </summary>
public class BattleActionButton : MonoBehaviour
{
    // グーボタンテキスト
    [SerializeField]
    private Text btnRockText = null;
    // パーボタンテキスト
    [SerializeField]
    private Text btnPaperText = null;
    // チョキボタンテキスト
    [SerializeField]
    private Text btnScissorText = null;
    // グーボタン
    [SerializeField]
    private Button btnRock = null;
    // パーボタン
    [SerializeField]
    private Button btnPaper = null;
    // チョキボタン
    [SerializeField]
    private Button btnScissor = null;

    public Button.ButtonClickedEvent OnClickBtnRock => btnRock.onClick;
    public Button.ButtonClickedEvent OnClickBtnPaper => btnPaper.onClick;
    public Button.ButtonClickedEvent OnClickBtnScissor => btnScissor.onClick;

    /// <summary>
    /// テキスト初期化
    /// </summary>
    public void Initialize()
    {
        btnRockText.text = "グー";
        btnPaperText.text = "パー";
        btnScissorText.text = "チョキ";
        SetInteractable(false);
    }

    /// <summary>
    /// ボタンの有効無効設定
    /// </summary>
    /// <param name="isInteractable"></param>
    public void SetInteractable(bool isInteractable)
    {
        btnRock.interactable = isInteractable;
        btnPaper.interactable = isInteractable;
        btnScissor.interactable = isInteractable;
    }

    /// <summary>
    /// 全ボタンの取得
    /// </summary>
    /// <returns></returns>
    public List<Button> GetBtns()
    {
        return new List<Button>()
        {
            btnRock,
            btnPaper,
            btnScissor
        };
    }

}
