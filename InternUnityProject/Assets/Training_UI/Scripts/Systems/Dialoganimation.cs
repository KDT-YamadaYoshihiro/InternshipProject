using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;


public static class DialogAnimation
{

    /// <summary>
    /// CanvasGroup用のフェードアニメーション
    /// </summary>
    /// <param name="arg_canvasGroup">対象キャンパスグループ</param>
    /// <param name="arg_targetAlpha">目標α値</param>
    /// <param name="arg_duration">経過時間</param>
    /// <param name="onFinished"></param>
    /// <returns></returns>
    public static async UniTask FadeRoutine(this CanvasGroup arg_canvasGroup, float arg_targetAlpha, float arg_duration, CancellationToken arg_ct)
    {
        if (arg_canvasGroup == null) { return; }

        float startAlpha = arg_canvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < arg_duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, Mathf.Clamp01(elapsed / arg_duration));
            arg_canvasGroup.alpha = Mathf.Lerp(startAlpha, arg_targetAlpha, t);
            await UniTask.Yield(PlayerLoopTiming.Update, arg_ct);
        }

        arg_canvasGroup.alpha = arg_targetAlpha;
    }

    /// <summary>
    /// RectTransform用のスケールアニメーション
    /// </summary>
    /// <param name="arg_rectTransform">対象rectTransform</param>
    /// <param name="arg_targetScale">目標スケール</param>
    /// <param name="arg_duration">時間</param>
    /// <param name="onFinished"></param>
    /// <returns></returns>
    public static async UniTask ScaleRoutine(this RectTransform arg_rectTransform, Vector3 arg_targetScale, float arg_duration, CancellationToken arg_ct)
    {
        if (arg_rectTransform == null) { return; }

        Vector3 startScale = arg_rectTransform.localScale;
        float elapsed = 0f;

        while (elapsed < arg_duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, Mathf.Clamp01(elapsed / arg_duration));
            arg_rectTransform.localScale = Vector3.Lerp(startScale, arg_targetScale, t);
            await UniTask.Yield(PlayerLoopTiming.Update,arg_ct);
        }

        arg_rectTransform.localScale = arg_targetScale;
    }
}