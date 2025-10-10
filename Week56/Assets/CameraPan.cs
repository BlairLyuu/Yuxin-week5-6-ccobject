// CameraPan.cs  (Input System �汾)
using UnityEngine;
using UnityEngine.InputSystem; // �� ����

public class CameraPan : MonoBehaviour
{
    [Header("�ƶ�")]
    public float speed = 8f;
    public bool smooth = true;
    public float smoothTime = 0.08f;

    [Header("X ��߽�")]
    public float minX = -10f;
    public float maxX = 10f;

    [Header("��ѡ�����ݱ����Զ�����߽磨2D��")]
    public SpriteRenderer background; // ����2D�������Ͻ������Զ���߽�

    float velX;

    void Start()
    {
        // �Զ�����߽磺��������� + �ṩ�˱���ʱ��Ч
        var cam = Camera.main;
        if (background && cam && cam.orthographic)
        {
            float halfCamW = cam.orthographicSize * cam.aspect;
            minX = background.bounds.min.x + halfCamW;
            maxX = background.bounds.max.x - halfCamW;
        }
    }

    void Update()
    {
        float input = 0f;
        var kb = Keyboard.current;
        if (kb != null)
        {
            if (kb.leftArrowKey.isPressed || kb.aKey.isPressed) input -= 1f;
            if (kb.rightArrowKey.isPressed || kb.dKey.isPressed) input += 1f;
        }

        // Ҳ��֧���ֱ�ʮ�ּ�/ҡ�ˣ���ѡ��
        var gp = Gamepad.current;
        if (gp != null)
        {
            float pad = Mathf.Abs(gp.leftStick.x.ReadValue()) > 0.1f ? gp.leftStick.x.ReadValue()
                       : (gp.dpad.left.isPressed ? -1f : gp.dpad.right.isPressed ? 1f : 0f);
            if (Mathf.Abs(pad) > Mathf.Abs(input)) input = pad;
        }

        float targetX = Mathf.Clamp(transform.position.x + input * speed * Time.deltaTime, minX, maxX);

        if (smooth)
        {
            float x = Mathf.SmoothDamp(transform.position.x, targetX, ref velX, smoothTime);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
    }
}
