using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// スライダーエリアのModel
/// スライダーに必要なデータを持つ（Viewに依存しない）
/// </summary>
public class SliderAreaModel
{
    // コンストラクタで初期値を設定
    public SliderAreaModel(int count, int min, int max)
    {
        this.sliderCount = Mathf.Clamp(count, min, max); ;
        this.min = min;
        this.max = max;
    }

    // スライダーの値と範囲
    private int sliderCount = 0;
    public int SliderCount => sliderCount;
    private int min = 0;
    public int Min => min;
    private int max = 0;
    public int Max => max;

    public UnityAction<int> OnSliderCountChanged = null;

    /// <summary>
    /// スライダーの値を増減させるメソッド
    /// </summary>
    /// <param name="amount"></param>
    public void AddCount(int amount)
    {
        // 自身の持つMin/Maxでクランプする
        int nextCount = Mathf.Clamp(sliderCount + amount, min, max);
        if (sliderCount != nextCount)
        {
            sliderCount = nextCount;
            OnSliderCountChanged?.Invoke(sliderCount);
        }
    }

    /// <summary>
    /// スライダーの値を直接設定するメソッド
    /// </summary>
    /// <param name="count"></param>
    public void SetSliderCount(int count)
    {
        int nextCount = Mathf.Clamp(count, min, max);
        if (sliderCount != nextCount)
        {
            sliderCount = nextCount;
            OnSliderCountChanged?.Invoke(sliderCount);
        }
    }
}
