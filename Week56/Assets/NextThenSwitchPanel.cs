using UnityEngine;

public class NextThenSwitchPanel : MonoBehaviour
{
    [Header("文字控制")]
    public UITextFadeSwitcher textSwitcher;  // 拖：挂了你原脚本的对象（通常是 TextDisplay）

    [Header("面板切换")]
    public GameObject panelS;      // 当前面板（要关）
    public GameObject blackPanel;  // 目标面板（要开）

    // 绑定到 Next 按钮的 OnClick
    public void OnNextClicked()
    {
        if (textSwitcher == null)
        {
            Debug.LogError("[NextThenSwitchPanel] textSwitcher 未赋值");
            return;
        }

        // 判定“是否已经在最后一段”
        // 不去读私有 index；直接比较“当前显示的文字 == 数组最后一条”
        var lines = textSwitcher.textLines;           // public
        var label = textSwitcher.textDisplay;         // public
        if (lines == null || lines.Length == 0 || label == null)
        {
            Debug.LogError("[NextThenSwitchPanel] textLines 或 textDisplay 未赋值");
            return;
        }

        string last = lines[lines.Length - 1];
        bool atLast = label.text == last;

        if (atLast)
        {
            // 切面板
            if (panelS) panelS.SetActive(false);
            if (blackPanel) blackPanel.SetActive(true);
        }
        else
        {
            // 还没到最后一段 -> 正常切下一段
            textSwitcher.NextText();
        }
    }
}
