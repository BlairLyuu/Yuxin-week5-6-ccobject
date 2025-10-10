using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float moveSpeed = 5f;          // �ƶ��ٶ�
    public float minX = -10f;             // ��СXλ�ã���߽磩
    public float maxX = 10f;              // ���Xλ�ã��ұ߽磩

    private Vector3 targetPosition;
    private Keyboard keyboard;

    void Start()
    {
        targetPosition = transform.position;
        keyboard = Keyboard.current;

        if (keyboard == null)
        {
            Debug.LogWarning("δ��⵽���������豸");
        }
    }

    void Update()
    {
        if (keyboard == null) return;

        // ��ȡ�������루ʹ����Input System��
        float horizontalInput = 0f;

        if (keyboard.leftArrowKey.isPressed)
        {
            horizontalInput = -1f;
        }
        else if (keyboard.rightArrowKey.isPressed)
        {
            horizontalInput = 1f;
        }

        // ������λ��
        if (horizontalInput != 0)
        {
            targetPosition.x += horizontalInput * moveSpeed * Time.deltaTime;

            // �����ڷ�Χ��
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);

            // Ӧ��λ��
            transform.position = targetPosition;
        }
    }
}