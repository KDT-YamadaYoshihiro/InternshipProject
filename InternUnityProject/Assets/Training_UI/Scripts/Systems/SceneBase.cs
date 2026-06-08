using System;
using System.Threading;
using UnityEngine;

public class SceneBase : MonoBehaviour
{
    // シーンの種類を定義する列挙型
    public enum SceneTypes
	{
		Scene,
		Dialog,
	}

    /// <summary>
	/// シーンの種類を返すプロパティ
	/// </summary>
    public virtual SceneTypes SceneType { get { return SceneTypes.Scene; } }
    // プールするかどうかのプロパティ
    public virtual bool ShouldPool { get; private set; } = false;

    protected CancellationTokenSource sceneCts = new CancellationTokenSource();

    /// <summary>
    /// シーンの初期化・リセット処理
    /// </summary>
    /// <remarks>
    /// Active直後での実行を想定しているため、AwakeやStartではなくこのメソッドで初期化処理を行う。
    /// </remarks>
    public virtual void OnPreparationScene() { }

    /// <summary>
	/// シーンのアクティブ状態を設定するメソッド	
	/// </summary>
	/// <param name="value"></param>
    public virtual void SetSceneActive(bool value)
	{
		transform.GetChild(0).gameObject.SetActive(value);
	}

    /// <summary>
	/// シーンの開くときの処理を定義するメソッド
	/// </summary>
	/// <param name="onFinished"></param>
    public virtual void OnSceneOpening(Action onFinished)
	{
		onFinished?.Invoke();
	}
    /// <summary>
	/// シーンの閉じるときの処理を定義するメソッド
	/// </summary>
	/// <param name="onFinished"></param>
    public virtual void OnSceneClosing(Action onFinished)
	{
		onFinished?.Invoke();
	}

    /// <summary>
	/// シーンの初期化処理を定義するメソッド
	/// </summary>
    protected virtual void Awake()
	{
		SetSceneActive(false);
	}

    /// <summary>
    ///　シーンの破棄処理を定義するメソッド
    /// </summary>
    protected virtual void OnDestroy()
    {
        // シーン破棄時に一括キャンセル
        sceneCts?.Cancel();
        sceneCts?.Dispose();
    }
}
