using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public static class InputManager
{
    public static IEnumerator WaitClickBtn(List<Button> btnList, UnityAction onClicked = null)
    {
        bool isDone = false;
        UnityAction onClick = () => isDone = true;

        foreach (var btn in btnList)
        {
            btn.onClick.AddListener(onClick);
        }

        while (!isDone) yield return null;

        foreach (var btn in btnList)
        {
            btn.onClick.RemoveListener(onClick);
        }

        onClicked?.Invoke();
    }
}