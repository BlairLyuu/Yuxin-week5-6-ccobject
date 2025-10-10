using UnityEngine;
using TMPro;
using System.Collections;

public class UITextFadeSwitcher : MonoBehaviour
{
    [TextArea] public string[] textLines;  // 三段文字
    public TMP_Text textDisplay;           // 拖入 Text (TMP)
    public float fadeDuration = 0.5f;      // 淡入淡出时间

    private int index = 0;
    private bool isFading = false;

    void Start()
    {
        if (textLines.Length > 0 && textDisplay != null)
            textDisplay.text = textLines[0];
    }

    public void NextText()
    {
        if (isFading || textLines.Length == 0) return;

        index++;
        if (index >= textLines.Length) index = textLines.Length - 1; // 停在最后一句

        StartCoroutine(FadeSwitch(textLines[index]));
    }

    public void PrevText()
    {
        if (isFading || textLines.Length == 0) return;

        index--;
        if (index < 0) index = 0; // 停在第一句

        StartCoroutine(FadeSwitch(textLines[index]));
    }

    private IEnumerator FadeSwitch(string newText)
    {
        isFading = true;
        // 淡出
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            textDisplay.alpha = alpha;
            yield return null;
        }

        textDisplay.alpha = 0f;
        textDisplay.text = newText;

        // 淡入
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            textDisplay.alpha = alpha;
            yield return null;
        }

        textDisplay.alpha = 1f;
        isFading = false;
    }
}
