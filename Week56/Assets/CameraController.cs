using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;          // 移动速度
    public float minX = -10f;             // 最小X位置（左边界）
    public float maxX = 10f;              // 最大X位置（右边界）

    private Vector3 targetPosition;
    private Keyboard keyboard;

    void Start()
    {
        targetPosition = transform.position;
        keyboard = Keyboard.current;

        if (keyboard == null)
        {
            Debug.LogWarning("未检测到键盘输入设备");
        }
    }

    void Update()
    {
        if (keyboard == null) return;

        // 获取键盘输入（使用新Input System）
        float horizontalInput = 0f;

        if (keyboard.leftArrowKey.isPressed)
        {
            horizontalInput = -1f;
        }
        else if (keyboard.rightArrowKey.isPressed)
        {
            horizontalInput = 1f;
        }

        // 计算新位置
        if (horizontalInput != 0)
        {
            targetPosition.x += horizontalInput * moveSpeed * Time.deltaTime;

            // 限制在范围内
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

            // 应用位置
            transform.position = targetPosition;
        }
    }
}