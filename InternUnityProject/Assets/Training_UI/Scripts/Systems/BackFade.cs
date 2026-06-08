using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BackFade : MonoBehaviour, ISceneFade
{
    [SerializeField] private Image barrierImage;

    public IEnumerator FadeOut(float arg_duration)
    {
        yield return Fade(1.0f, arg_duration);
    }

    public  IEnumerator FadeIn(float arg_duration)
    {
        yield return Fade(0.0f, arg_duration);
    }

    private IEnumerator Fade(float arg_targetAlpha, float arg_duration)
    {

        float startAlpha = barrierImage.color.a;
        float time = 0;
        // barrierImageを表示状態にする
        barrierImage.gameObject.SetActive(true);

        while (time < arg_duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, arg_targetAlpha, time / arg_duration);
            // 透明度の更新
            Color color = barrierImage.color;
            color.a = alpha;
            barrierImage.color = color;
            yield return null;
        }
        Color finalColor = barrierImage.color;
        finalColor.a = arg_targetAlpha;
        barrierImage.color = finalColor;

        // 完全に透明になったら
        if (arg_targetAlpha <= 0)
        {
            barrierImage.gameObject.SetActive(false);
        }

    }
}
