using UnityEngine;

public class PanelSwitcherOnLastText : MonoBehaviour
{
    [Header("���ֿ��ƽű�")]
    public UITextFadeSwitcher textSwitcher;  // �����ԭUITextFadeSwitcher�ű�
    [Header("Ҫ�л������")]
    public GameObject panelS;     // ��ǰҪ�رյ����
    public GameObject blackPanel; // Ҫ�򿪵����

    public void TrySwitchPanel()
    {
        if (textSwitcher == null)
        {
            Debug.LogError("[PanelSwitcherOnLastText] ��� UITextFadeSwitcher �ű��ϵ� textSwitcher��");
            return;
        }

        // ��������Ѿ���ʾ�����һ��
        if (textSwitcher != null && textSwitcherIsLast())
        {
            if (panelS) panelS.SetActive(false);
            if (blackPanel) blackPanel.SetActive(true);
        }
    }

    private bool textSwitcherIsLast()
    {
        // �÷������˽�� index����Ϊ���ǲ���ԭ�ű�
        var type = typeof(UITextFadeSwitcher);
        var indexField = type.GetField("index", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var textLinesField = type.GetField("textLines", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (indexField == null || textLinesField == null) return false;

        int index = (int)indexField.GetValue(textSwitcher);
        string[] textLines = (string[])textLinesField.GetValue(textSwitcher);

        return textLines != null && index >= textLines.Length - 1;
    }
}
