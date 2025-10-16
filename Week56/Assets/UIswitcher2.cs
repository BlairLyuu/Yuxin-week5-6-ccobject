using UnityEngine;
using TMPro;
using System.Collections;

public class UITextFadeSwitcher : MonoBehaviour
{
    [Header("文字设置")]
    [TextArea] public string[] textLines;  // 三段文字
    public TMP_Text textDisplay;           // 拖入 Text (TMP)
    public float fadeDuration = 0.5f;      // 淡入淡出时间

    [Header("Panel切换设置")]
    public GameObject currentPanel;        // 拖入 PanelS
    public GameObject nextPanel;           // 拖入 BlackPanel

    private int index = 0;
    private bool isFading = false;

    void Start()
    {
        if (textLines.Length > 0 && textDisplay != null)
        {
            textDisplay.text = textLines[0];
        }

        // 确保下一个Panel初始是隐藏的
        if (nextPanel != null)
        {
            nextPanel.SetActive(false);
        }
    }

    public void NextText()
    {
        if (isFading || textLines.Length == 0) return;

        index++;

        // 如果超过文字数量，切换Panel
        if (index >= textLines.Length)
        {
            SwitchPanel();
        }
        else
        {
            // 继续显示下一句
            StartCoroutine(FadeSwitch(textLines[index]));
        }
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
        float timer = 0f;
        Color startColor = textDisplay.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            textDisplay.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        // 更换文字
        textDisplay.text = newText;

        // 淡入
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            textDisplay.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        textDisplay.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        isFading = false;
    }

    private void SwitchPanel()
    {
        Debug.Log("文字显示完毕，切换Panel");

        // 关闭当前Panel
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        // 打开下一个Panel
        if (nextPanel != null)
        {
            nextPanel.SetActive(true);
        }
    }
}