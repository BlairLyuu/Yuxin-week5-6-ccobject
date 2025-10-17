using UnityEngine;
using System.Reflection;

public class ClickStartOpener : MonoBehaviour
{
    public MonoBehaviour openingManager;
    public string methodName = "Play"; // 这里填你动画脚本里播放方法的名字

    public void OnClicked()
    {
        if (openingManager == null)
        {
            Debug.LogError("[ClickStartOpener] openingManager 未赋值");
            return;
        }

        var m = openingManager.GetType().GetMethod(
            methodName,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
        );

        if (m != null) m.Invoke(openingManager, null);
        else Debug.LogError($"[ClickStartOpener] 方法 {methodName} 不存在于 {openingManager.GetType().Name}");

        gameObject.SetActive(false);
    }
}
