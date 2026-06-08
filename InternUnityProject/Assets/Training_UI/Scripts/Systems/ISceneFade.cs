using System.Collections;
public interface ISceneFade
{
    // 暗くする（引数は目標の透明度と時間）
    IEnumerator FadeOut(float arg_duration);
    // 明るくする
    IEnumerator FadeIn(float arg_duration);
}
