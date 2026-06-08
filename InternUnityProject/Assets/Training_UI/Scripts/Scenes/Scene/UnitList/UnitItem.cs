using System;
using UnityEngine;
using UnityEngine.UI;

public class UnitItem : MonoBehaviour
{

    [SerializeField] private Text nameText;
    [SerializeField] private Text parameterText;
    [SerializeField] private Text powerText;
    [SerializeField] private Button clickButton;

    /// <summary>
    /// データを受け取ってUIを更新するメソッド
    /// </summary>
    /// <param name="arg_data"></param>
    /// <param name="sortType"></param>
    public void Setup(ActorData arg_data, SortModel.SortType sortType, Action<ActorData> onSelect)
    {
        nameText.text = arg_data.Name;

        // ソート種別に応じて表示内容を切り替える
        UpdateParameterText(arg_data,sortType);

        // ボタンイベントの登録
        clickButton.onClick.RemoveAllListeners();
        clickButton.onClick.AddListener(() => {
            onSelect?.Invoke(arg_data);
        });
    }

    /// <summary>
    /// 引数のタイプに応じてソートする
    /// </summary>
    /// <param name="sortType"></param>
    private void UpdateParameterText(ActorData arg_data,SortModel.SortType sortType)
    {
        switch (sortType)
        {
            case SortModel.SortType.Atk:
                parameterText.text = "Atk";
                powerText.text = arg_data.Atk.ToString();
                break;
            case SortModel.SortType.Def:
                parameterText.text = "Def";
                powerText.text = arg_data.Def.ToString();
                break;
            case SortModel.SortType.Hp:
                parameterText.text = "Hp";
                powerText.text = arg_data.Hp.ToString();
                break;
            case SortModel.SortType.Id:
                parameterText.text = "ID";
                powerText.text = arg_data.Id.ToString();
                break;
            default: // Strength
                parameterText.text = "強さ";
                powerText.text = (arg_data.Atk + arg_data.Def).ToString();
                break;
        }
    }
}


