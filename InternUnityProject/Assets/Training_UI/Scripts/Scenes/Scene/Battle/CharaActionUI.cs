using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラのアクションのUI
/// </summary>
public class CharaActionUI : MonoBehaviour
{
    [SerializeField]
    private Text actionText = null;

    /// <summary>
    /// 選択したアクションのテキストを設定する
    /// </summary>
    /// <param name="index"></param>
    public void SetActionText(int index)
    {
        string text = string.Empty;
        switch((Battle.ActionType)index)
        {
            case Battle.ActionType.Rock:
                text = "グー";
                break;
            case Battle.ActionType.Paper:
                text = "パー";
                break;
            case Battle.ActionType.Scissor:
                text = "チョキ";
                break;
        }
        actionText.text = text;
    }

    /// <summary>
    /// 選択したアクションのテキストをクリアする
    /// </summary>
    public void ClearActionText()
    {
        actionText.text = string.Empty;
    }
}
