using UnityEngine;
using UnityEngine.UI;

public class EnterGate : MonoBehaviour
{
    public GameObject blackPanel;       // 关掉它
    public GameObject clickAnywhereGO;  // Enter 后打开它

    public void OnEnter()
    {
        if (blackPanel) blackPanel.SetActive(false);
        if (clickAnywhereGO) clickAnywhereGO.SetActive(true);
    }
}
