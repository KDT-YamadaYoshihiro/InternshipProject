using System.Collections;
using UnityEngine;

public class InitialScene : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return null;
		SceneTranslator.Instance.OpenScene<TitleScene>(
			(scene) =>
			{
                scene.ReceiveParam.titleText = TitleScene.GenerateRandomTitle();
            });
		Destroy(gameObject);
	}

	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("RootScene");
	}
}
