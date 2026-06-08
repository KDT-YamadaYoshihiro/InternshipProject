using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * テストで使ってたスクリプトです
 * 今回は使用しません
 */
public class TestScript : MonoBehaviour
{
	[SerializeField]
	string testText = null;

	private void Awake()
	{

	}
	private void Update()
	{

	}
	private void LateUpdate()
	{

	}
	private void FixedUpdate()
	{

	}

    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadSceneMode)
    {
    }
}
