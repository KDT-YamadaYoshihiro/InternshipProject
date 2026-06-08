using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Hpバー
/// </summary>
public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Image redGauge = null;
    [SerializeField]
    private Image greenGauge = null;

    [Header("Production Settings")]
    // 赤いゲージの待機時間
    [SerializeField]
    private float delayTime = 0.5f;
    // 赤いゲージの減少速度
    [SerializeField]
    private float shrinkSpeed = 2.0f;

    private Coroutine shrinkCoroutine;

    public float RedGaugeFillAmount => redGauge.fillAmount;
    public float GreenGaugeFillAmount => greenGauge.fillAmount;

    /// <summary>
    /// Hpバーの設定
    /// </summary>
    /// <param name="diff"></param>
    public void SetRedGauge(float diff)
    {
        redGauge.fillAmount = diff;
    }

    /// <summary>
    /// Hpバーの設定
    /// </summary>
    /// <param name="diff"></param>
    public void SetGreenGauge(float diff)
    {
        // 緑ゲージは即座に反映
        greenGauge.fillAmount = diff;

        // 赤ゲージの追従アニメーションを開始（動いている最中なら一度止める）
        if (shrinkCoroutine != null) StopCoroutine(shrinkCoroutine);
        shrinkCoroutine = StartCoroutine(ShrinkProcess(diff));
    }

    /// <summary>
    /// 指定時間速度でバー（赤）を減らす
    /// </summary>
    /// <param name="targetAmount"></param>
    /// <returns></returns>
    private IEnumerator ShrinkProcess(float targetAmount)
    {
        yield return new WaitForSeconds(delayTime);

        while (redGauge.fillAmount > targetAmount)
        {
            redGauge.fillAmount -= Time.deltaTime * shrinkSpeed;
            yield return null;
        }
        redGauge.fillAmount = targetAmount;
    }
}
