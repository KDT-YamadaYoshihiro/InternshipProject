using UnityEngine;
using UnityEngine.UI;
using System;

public class SortDialogView : MonoBehaviour
{
    [Header("Toggles")]
    [SerializeField] public Toggle toggleStrength, toggleId, toggleAtk, toggleDef, toggleHp;
    [SerializeField] public Toggle toggleAscending, toggleDescending;

    [Header("Labels (Text)")]
    [SerializeField] private Text labelStrength;
    [SerializeField] private Text labelId, labelAtk, labelDef, labelHp;
    [SerializeField] private Text labelAscending, labelDescending;
    [SerializeField] private Text titleSort, titleOrder, btnTextDecide;

    [Header("Buttons")]
    [SerializeField] public Button setButton;

    public Action OnSetButtonClicked;

    private void Awake()
    {
        setButton.onClick.AddListener(() => OnSetButtonClicked?.Invoke());
    }

    // テキストの初期化
    public void InitializeTexts()
    {
        titleSort.text = "ソート";
        titleOrder.text = "順序";
        btnTextDecide.text = "設定";
        labelStrength.text = "強さ";
        labelId.text = "Id";
        labelAtk.text = "Atk";
        labelDef.text = "Def";
        labelHp.text = "Hp";
        labelAscending.text = "昇順";
        labelDescending.text = "降順";
    }

    // トグルの状態をモデルの値に基づいて更新
    public void SetToggleStates(SortModel.SortType sort, SortModel.OrderType order)
    {
        toggleStrength.isOn = (sort == SortModel.SortType.Strength);
        toggleId.isOn = (sort == SortModel.SortType.Id);
        toggleAtk.isOn = (sort == SortModel.SortType.Atk);
        toggleDef.isOn = (sort == SortModel.SortType.Def);
        toggleHp.isOn = (sort == SortModel.SortType.Hp);
        toggleAscending.isOn = (order == SortModel.OrderType.Ascending);
        toggleDescending.isOn = (order == SortModel.OrderType.Descending);
    }

}