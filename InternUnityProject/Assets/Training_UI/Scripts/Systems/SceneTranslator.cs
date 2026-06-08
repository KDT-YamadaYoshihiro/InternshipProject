using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// シーンの切り替えを管理するクラス
/// </summary>
public static class GameObjectExtensions
{
	public class GameObjectActiveScope : IDisposable
	{
		GameObject gameObject = null;
		public GameObjectActiveScope(GameObject gameObject)
		{
			this.gameObject = gameObject;
			this.gameObject.SetActive(true);
		}
		public void Dispose()
		{
			this.gameObject.SetActive(false);
		}
	}
	public static GameObjectActiveScope ActiveScope(this GameObject gameObject)
	{
		return new GameObjectActiveScope(gameObject);
	}
}

/// <summary>
/// IListをスタックのように扱うための拡張メソッド
/// </summary>
public static class ListExtensions
{
	public static void Push<T>(this IList<T> self, T item)
	{
		self.Add(item);
	}
	public static T Pop<T>(this IList<T> self)
	{
		var result = self.LastOrDefault();
        self.Remove(result);
		return result;
	}
	public static T Peek<T>(this IList<T> self)
	{
		return self.FirstOrDefault();
	}
}

/// <summary>
/// シーンの切り替えを管理するクラス
/// </summary>
public class SceneTranslator : MonoBehaviour
{
    /// <summary>
	/// シーンのデータを保持するクラス
	/// </summary>
    class SceneData
	{
		public SceneBase Scene { get; }
		public Scene UnityScene { get; }

		public SceneData(SceneBase scene, Scene unityScene)
		{
			this.Scene = scene;
			this.UnityScene = unityScene;
		}
	}

    /// <summary>
	/// シーンの履歴を管理するインターフェースとクラス
	/// </summary>
    interface ISceneHistory
	{
		SceneData SceneData { get; }
		void OnWillClose();
		void OnDidClose();
	}

    /// <summary>
	/// ジェネリッククラスでシーンの履歴を管理
	/// </summary>
	/// <typeparam name="T"></typeparam>
    class SceneHistory<T> : ISceneHistory where T : SceneBase
	{
		public SceneData SceneData { get; } = null;
		T scene = null;
		Action<T> onWillClose = null;
		Action onDidClose = null;

		public SceneHistory(SceneData sceneData, T scene, Action<T> onWillClose, Action onDidClose)
		{
			this.SceneData = sceneData;
			this.scene = scene;
			this.onWillClose = onWillClose;
			this.onDidClose = onDidClose;
		}
		public void OnWillClose()
		{
			onWillClose?.Invoke(scene);
		}
		public void OnDidClose()
		{
			onDidClose?.Invoke();
		}
	}

    [SerializeField] Image barrierImage = null;

    private ISceneFade sceneFadeElement;
    private ISceneFade SceneFade => sceneFadeElement;

    Dictionary<string, SceneData> loadedScenes = new Dictionary<string, SceneData>();
	List<ISceneHistory> sceneHistories = new List<ISceneHistory>();

    /// <summary>
	/// シーンを開く
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="onWillOpen"></param>
	/// <param name="onWillClose"></param>
	/// <param name="onDidClose"></param>
    public void OpenScene<T>(Action<T> onWillOpen = null, Action<T> onWillClose = null, Action onDidClose = null) where T : SceneBase
	{
		IEnumerator function()
		{
			yield return CloseSceneCoroutine();
			yield return OpenSceneCoroutine(onWillOpen, onWillClose, onDidClose);
		}
		StartCoroutine(Loading(function));
	}

    /// <summary>
	/// シーンを閉じる
	/// </summary>
    public void CloseScene()
	{
		StartCoroutine(Loading(CloseSceneCoroutine));
	}

    /// <summary>
	/// ダイアログを開く
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="onWillOpen"></param>
	/// <param name="onWillClose"></param>
	/// <param name="onDidClose"></param>
    public void OpenDialog<T>(Action<T> onWillOpen = null, Action<T> onWillClose = null, Action onDidClose = null) where T : SceneBase
	{
		StartCoroutine( OpenSceneCoroutine(onWillOpen, onWillClose, onDidClose));
	}

    /// <summary>
	/// ダイアログを閉じる
	/// </summary>
	/// <param name="onDidClose"></param>
    public void CloseDialog(Action onDidClose = null)
	{
        StartCoroutine(CloseDialogCoroutine(onDidClose));
    }

    /// <summary>
	/// ダイアログを閉じる共通処理
	/// </summary>
	/// <param name="onDidClose"></param>
	/// <returns></returns>
    private IEnumerator CloseDialogCoroutine(Action onDidClose)
    {
        yield return CloseSceneCoroutine();
        onDidClose?.Invoke();
    }

    /// <summary>
	/// シーンの切り替えを行う共通処理
	/// </summary>
	/// <param name="action"></param>
	/// <returns></returns>
    IEnumerator Loading(Func<IEnumerator> action)
	{

        using (barrierImage.gameObject.ActiveScope())
		{
			if (SceneFade != null)
			{
				yield return StartCoroutine(SceneFade.FadeOut(0.5f));
			}

			yield return action.Invoke();

			if (SceneFade != null)
			{
				yield return StartCoroutine(SceneFade.FadeIn(0.5f));
			}
		}
	}

    /// <summary>
	/// シーンを開く共通処理
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="onWillOpen"></param>
	/// <param name="onWillClose"></param>
	/// <param name="onDidClose"></param>
	/// <returns></returns>
    IEnumerator OpenSceneCoroutine<T>(Action<T> onWillOpen = null, Action<T> onWillClose = null, Action onDidClose = null) where T : SceneBase
	{
		string sceneName = typeof(T).Name;
		if (sceneHistories.Any(d => d.SceneData.UnityScene.name == sceneName))
		{
			yield break;
		}

		T scene = null;
		if (!loadedScenes.TryGetValue(sceneName, out var sceneData))
		{
			yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			var unityScene = SceneManager.GetSceneByName(sceneName);
			var gameObject = unityScene.GetRootGameObjects()[0];
			scene = gameObject.GetComponent<T>();
            sceneData = new SceneData(scene, unityScene);
			sceneData.Scene.SetSceneActive(false);
			loadedScenes.Add(sceneName, sceneData);
		}
		else
		{
			scene = sceneData.Scene as T;
        }

		sceneHistories.Push(new SceneHistory<T>(sceneData, scene, onWillClose, onDidClose));

		sceneData.Scene.SetSceneActive(true);
        SceneManager.SetActiveScene(sceneData.UnityScene);
        sceneData.Scene.OnPreparationScene();
        onWillOpen?.Invoke(scene);
		{
			bool isFinished = false;
			sceneData.Scene.OnSceneOpening(() => isFinished = true);
			while (!isFinished)
			{
				yield return null;
			}
		}
	}

    /// <summary>
	/// シーンを閉じる共通処理
	/// </summary>
	/// <returns></returns>
    IEnumerator CloseSceneCoroutine()
	{
		if (sceneHistories.Count <= 0)
		{
			yield break;
		}

		var sceneHistory = sceneHistories.Pop();
		sceneHistory.OnWillClose();

		var sceneData = sceneHistory.SceneData;
		if (sceneData != null)
		{
			var scene = sceneData.Scene;

			bool isFinished = false;
			scene.OnSceneClosing(() => isFinished = true);
			while (!isFinished)
			{
				yield return null;
			}

			if (!scene.ShouldPool)
			{
				var sceneName = sceneData.Scene.GetType().Name;
				if (loadedScenes.ContainsKey(sceneName))
				{
					loadedScenes.Remove(sceneName);
				}
				var beforeScene = sceneHistories.LastOrDefault();
				SceneManager.SetActiveScene(beforeScene != null ? beforeScene.SceneData.UnityScene : gameObject.scene);
				yield return SceneManager.UnloadSceneAsync(sceneData.UnityScene);
			}
			else
			{
				scene.SetSceneActive(false);
			}
		}
		sceneHistory.OnDidClose();
	}

	static SceneTranslator instance = null;

    /// <summary>
	/// シングルトンインスタンスへのアクセス
	/// </summary>
    public static SceneTranslator Instance
	{
		get {
			if (instance == null)
			{
				var type = typeof(SceneTranslator);
				instance = (SceneTranslator)FindObjectOfType(type);
				if (instance == null)
				{
					Debug.LogError($"{type} is unattached. create {type}");
					var gameObject = new GameObject(nameof(SceneTranslator));
					instance = gameObject.AddComponent<SceneTranslator>();
				}
			}
			return instance;
		}
	}

    /// <summary>
	/// インスタンスの初期化と重複チェック
	/// </summary>
    private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}


    }

	/// <summary>
	/// 初期化
	/// </summary>
    private void Start()
	{
		barrierImage.gameObject.SetActive(false);
        sceneFadeElement = GetComponent<ISceneFade>();
    }

    /// <summary>
	/// インスタンスのクリーンアップ
	/// </summary>
    private void OnDestroy()
	{
		instance = null;
	}

}
