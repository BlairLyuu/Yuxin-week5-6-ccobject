using UnityEngine;

public class PanelSwitcherOnLastText : MonoBehaviour
{
    [Header("文字控制脚本")]
    public UITextFadeSwitcher textSwitcher;  // 拖你的原UITextFadeSwitcher脚本
    [Header("要切换的面板")]
    public GameObject panelS;     // 当前要关闭的面板
    public GameObject blackPanel; // 要打开的面板

    public void TrySwitchPanel()
    {
        if (textSwitcher == null)
        {
            Debug.LogError("[PanelSwitcherOnLastText] 请把 UITextFadeSwitcher 脚本拖到 textSwitcher。");
            return;
        }

        // 如果文字已经显示到最后一段
        if (textSwitcher != null && textSwitcherIsLast())
        {
            if (panelS) panelS.SetActive(false);
            if (blackPanel) blackPanel.SetActive(true);
        }
    }

    private bool textSwitcherIsLast()
    {
        // 用反射访问私有 index，因为我们不改原脚本
        var type = typeof(UITextFadeSwitcher);
        var indexField = type.GetField("index", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var textLinesField = type.GetField("textLines", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (indexField == null || textLinesField == null) return false;

        int index = (int)indexField.GetValue(textSwitcher);
        string[] textLines = (string[])textLinesField.GetValue(textSwitcher);

        return textLines != null && index >= textLines.Length - 1;
    }
}
