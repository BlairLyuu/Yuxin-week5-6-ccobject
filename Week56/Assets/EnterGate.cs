using UnityEngine;
using UnityEngine.UI;

public class EnterGate : MonoBehaviour
{
    public GameObject blackPanel;       // �ص���
    public GameObject clickAnywhereGO;  // Enter �����

    public void OnEnter()
    {
        if (blackPanel) blackPanel.SetActive(false);
        if (clickAnywhereGO) clickAnywhereGO.SetActive(true);
    }
}
