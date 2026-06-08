using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitView : MonoBehaviour
{

    [SerializeField] private Transform content;
    [SerializeField] private UnitItem unitPrefab; 

    private List<UnitItem> pooledItems = new List<UnitItem>();

    // 外部からデータを取得して表示を更新
    public void UpdateUnitDisplay(List<ActorData> arg_dataList, SortModel.SortType sortType, Action<ActorData> onItemSelected)
    {
        // 表示
        for (int i = 0; i < arg_dataList.Count; i++)
        {
            var item = GetOrCreateItem(i);
            item.gameObject.SetActive(true);
            item.Setup(arg_dataList[i], sortType, onItemSelected);
        }

        // 余っているプールアイテムを非表示にする
        for (int i = arg_dataList.Count; i < pooledItems.Count; i++)
        {
            pooledItems[i].gameObject.SetActive(false);
        }
    }

    // クローンを取得または作成する
    private UnitItem GetOrCreateItem(int arg_index)
    {
        if (arg_index < pooledItems.Count)
        {
            return pooledItems[arg_index];
        }

        var newItem = Instantiate(unitPrefab, content);
        pooledItems.Add(newItem);
        return newItem;
    }

}
