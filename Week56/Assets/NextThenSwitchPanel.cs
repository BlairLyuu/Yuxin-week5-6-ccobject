using UnityEngine;

public class NextThenSwitchPanel : MonoBehaviour
{
    [Header("���ֿ���")]
    public UITextFadeSwitcher textSwitcher;  // �ϣ�������ԭ�ű��Ķ���ͨ���� TextDisplay��

    [Header("����л�")]
    public GameObject panelS;      // ��ǰ��壨Ҫ�أ�
    public GameObject blackPanel;  // Ŀ����壨Ҫ����

    // �󶨵� Next ��ť�� OnClick
    public void OnNextClicked()
    {
        if (textSwitcher == null)
        {
            Debug.LogError("[NextThenSwitchPanel] textSwitcher δ��ֵ");
            return;
        }

        // �ж����Ƿ��Ѿ������һ�Ρ�
        // ��ȥ��˽�� index��ֱ�ӱȽϡ���ǰ��ʾ������ == �������һ����
        var lines = textSwitcher.textLines;           // public
        var label = textSwitcher.textDisplay;         // public
        if (lines == null || lines.Length == 0 || label == null)
        {
            Debug.LogError("[NextThenSwitchPanel] textLines �� textDisplay δ��ֵ");
            return;
        }

        string last = lines[lines.Length - 1];
        bool atLast = label.text == last;

        if (atLast)
        {
            // �����
            if (panelS) panelS.SetActive(false);
            if (blackPanel) blackPanel.SetActive(true);
        }
        else
        {
            // ��û�����һ�� -> ��������һ��
            textSwitcher.NextText();
        }
    }
}
