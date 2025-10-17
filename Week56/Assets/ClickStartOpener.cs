using UnityEngine;
using System.Reflection;

public class ClickStartOpener : MonoBehaviour
{
    public MonoBehaviour openingManager;
    public string methodName = "Play"; // �������㶯���ű��ﲥ�ŷ���������

    public void OnClicked()
    {
        if (openingManager == null)
        {
            Debug.LogError("[ClickStartOpener] openingManager δ��ֵ");
            return;
        }

        var m = openingManager.GetType().GetMethod(
            methodName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
        );

        if (m != null) m.Invoke(openingManager, null);
        else Debug.LogError($"[ClickStartOpener] ���� {methodName} �������� {openingManager.GetType().Name}");

        gameObject.SetActive(false);
    }
}
