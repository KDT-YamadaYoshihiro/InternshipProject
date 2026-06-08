using Cysharp.Threading.Tasks;
using GameCommon;
using System;
using System.Threading;
using UnityEngine;

public abstract class DialogBase : SceneBase
{
    public override SceneTypes SceneType => SceneTypes.Dialog;

    [Header("Dialog Animation Settings")]
    // 背景・全体の透明度用
    [SerializeField] protected CanvasGroup bgCanvasGroup;
    // スケールをかける対象
    [SerializeField] protected RectTransform contentRoot;
    // 背景の最大Alpha値
    [SerializeField] protected float maxBgAlpha = 1.0f;

    // アニメーション時間の定数定義
    // フェード全体の時間
    private const float FadeDuration = 0.6f;
    // スケール自体の時間
    private const float ScaleDuration = 0.3f;

    /// <summary>
    /// 明示的なセットアップメソッド
    /// 制御クラスから生成時またはシーン開始時に呼ばれることを想定
    /// </summary>
    /// <remarks>
    /// Active直後での実行を想定しているため、AwakeやStartではなくこのメソッドで初期化処理を行う。
    /// </remarks>
    public override void OnPreparationScene()
    {
        ResetToInitialState();
    }

    /// <summary>
    /// 初期化状態にリセットするメソッド
    /// </summary>
    private void ResetToInitialState()
    {
        if (contentRoot != null)
        {
            contentRoot.localScale = Vector3.zero;
        }
        if (bgCanvasGroup != null)
        {
            bgCanvasGroup.alpha = 0.0f;
        }
    }

    /// <summary>
    /// アニメーションの再生を一括管理するメソッド
    /// </summary>
    /// <param name="isOpen"></param>
    /// <param name="onFinished"></param>
    protected void PlayAnimation(bool isOpen, Action onFinished)
    {
        ExecuteAnimationAsync(isOpen, onFinished, sceneCts.Token).Forget();
    }

    /// <summary>
    ///　アニメーションの実行と完了通知を管理する非同期メソッド
    /// </summary>
    /// <param name="isOpen"></param>
    /// <param name="onFinished"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    private async UniTaskVoid ExecuteAnimationAsync(bool isOpen, Action onFinished, CancellationToken ct)
    {
        await DoAnimation(isOpen, ct);
        
        // キャンセルされていなければ完了通知
        if (!ct.IsCancellationRequested)
        {
            onFinished?.Invoke();
        }
    }

    /// <summary>
    /// 開閉アニメーションの実装
    /// </summary>
    /// <param name="isOpen"></param>
    /// <param name="onFinished"></param>
    /// <returns></returns>
    private async UniTask DoAnimation(bool isOpen, CancellationToken ct)
    {
        float targetAlpha = isOpen ? maxBgAlpha : 0f;
        Vector3 targetScale = isOpen ? Vector3.one : Vector3.zero;

        if (isOpen)
        {
            await UniTask.WhenAll(
                bgCanvasGroup.FadeRoutine(targetAlpha, FadeDuration,ct),
                OpenDelayAndScale(targetScale, ct)
                );
        }
        else
        {
            await UniTask.WhenAll(
                contentRoot.ScaleRoutine(targetScale, ScaleDuration, ct),
                bgCanvasGroup.FadeRoutine(targetAlpha, FadeDuration, ct)
            );
        }
    }

    /// <summary>
    /// 開く際のディレイを伴うスケールアニメーション
    /// </summary>
    private async UniTask OpenDelayAndScale(Vector3 targetScale, CancellationToken ct)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(WaitDuration.DialogAnimationDelay), cancellationToken: ct);
        await contentRoot.ScaleRoutine(targetScale, ScaleDuration, ct);
    }

    /// <summary>
    /// シーンの開閉イベントでアニメーションを再生
    /// </summary>
    /// <param name="onFinished"></param>
    public override void OnSceneOpening(Action onFinished) => PlayAnimation(true, onFinished);
    public override void OnSceneClosing(Action onFinished) => PlayAnimation(false, onFinished);

}