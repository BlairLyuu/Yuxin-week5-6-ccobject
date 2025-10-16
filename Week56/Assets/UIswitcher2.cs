using UnityEngine;
using TMPro;
using System.Collections;

public class UITextFadeSwitcher : MonoBehaviour
{
    [Header("��������")]
    [TextArea] public string[] textLines;  // ��������
    public TMP_Text textDisplay;           // ���� Text (TMP)
    public float fadeDuration = 0.5f;      // ���뵭��ʱ��

    [Header("Panel�л�����")]
    public GameObject currentPanel;        // ���� PanelS
    public GameObject nextPanel;           // ���� BlackPanel

    private int index = 0;
    private bool isFading = false;

    void Start()
    {
        if (textLines.Length > 0 && textDisplay != null)
        {
            textDisplay.text = textLines[0];
        }

        // ȷ����һ��Panel��ʼ�����ص�
        if (nextPanel != null)
        {
            nextPanel.SetActive(false);
        }
    }

    public void NextText()
    {
        if (isFading || textLines.Length == 0) return;

        index++;

        // ������������������л�Panel
        if (index >= textLines.Length)
        {
            SwitchPanel();
        }
        else
        {
            // ������ʾ��һ��
            StartCoroutine(FadeSwitch(textLines[index]));
        }
    }

    public void PrevText()
    {
        if (isFading || textLines.Length == 0) return;
        index--;
        if (index < 0) index = 0; // ͣ�ڵ�һ��
        StartCoroutine(FadeSwitch(textLines[index]));
    }

    private IEnumerator FadeSwitch(string newText)
    {
        isFading = true;

        // ����
        float timer = 0f;
        Color startColor = textDisplay.color;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            textDisplay.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        // ��������
        textDisplay.text = newText;

        // ����
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
        Debug.Log("������ʾ��ϣ��л�Panel");

        // �رյ�ǰPanel
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        // ����һ��Panel
        if (nextPanel != null)
        {
            nextPanel.SetActive(true);
        }
    }
}