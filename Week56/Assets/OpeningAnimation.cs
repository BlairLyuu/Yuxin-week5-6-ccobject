using UnityEngine;
using UnityEngine.InputSystem;

public class OpeningAnimation : MonoBehaviour
{
    [Header("ͼƬ����")]
    public GameObject image1;              // ��һ��ͼƬ
    public GameObject image2;              // �ڶ���ͼƬ

    [Header("�Ƴ�����")]
    public float moveOutSpeed = 5f;        // �Ƴ��ٶ�
    public float moveOutDistance = 2000f;  // �Ƴ����루UI��λ��
    public string idleAnimationName = "Idle"; // Animator�������ƶ�����������

    [Header("��ѡ��������������")]
    public MonoBehaviour[] scriptsToEnableAfter; // ����������õĽű�����������ƣ�

    private bool isPlaying = true;         // �Ƿ����ڲ��ſ�������
    private bool isMovingOut = false;      // �Ƿ������Ƴ�
    private Vector3 image1StartPos;
    private Vector3 image2StartPos;
    private Animator animator1;
    private Animator animator2;
    private Mouse mouse;

    void Start()
    {
        mouse = Mouse.current;

        // �����ʼλ��
        if (image1 != null)
        {
            image1StartPos = image1.transform.localPosition;
            animator1 = image1.GetComponent<Animator>();
        }

        if (image2 != null)
        {
            image2StartPos = image2.transform.localPosition;
            animator2 = image2.GetComponent<Animator>();
        }

        // ���������ű�
        foreach (var script in scriptsToEnableAfter)
        {
            if (script != null)
            {
                script.enabled = false;
            }
        }
    }

    void Update()
    {
        if (!isPlaying) return;

        // ��������
        if (!isMovingOut && mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            StartMoveOut();
        }

        // ִ���Ƴ�����
        if (isMovingOut)
        {
            bool finished1 = MoveImageOut(image1, image1StartPos, true);
            bool finished2 = MoveImageOut(image2, image2StartPos, false);

            // ����ͼ���Ƴ���Ļ��
            if (finished1 && finished2)
            {
                FinishOpening();
            }
        }
    }

    void StartMoveOut()
    {
        isMovingOut = true;

        // ֹͣ�����ƶ��������л�����ֹ״̬
        if (animator1 != null)
        {
            animator1.enabled = false;
        }
        if (animator2 != null)
        {
            animator2.enabled = false;
        }
    }

    bool MoveImageOut(GameObject image, Vector3 startPos, bool moveUp)
    {
        if (image == null) return true;

        // ���ϻ������ƶ�
        float direction = moveUp ? 1f : -1f;
        image.transform.localPosition += Vector3.up * direction * moveOutSpeed * Time.deltaTime * 100f;

        // ����Ƿ��Ƴ��㹻Զ
        float distance = Mathf.Abs(image.transform.localPosition.y - startPos.y);
        return distance >= moveOutDistance;
    }

    void FinishOpening()
    {
        isPlaying = false;

        // ���ػ�����ͼƬ
        if (image1 != null) image1.SetActive(false);
        if (image2 != null) image2.SetActive(false);

        // ���������ű�
        foreach (var script in scriptsToEnableAfter)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }

        Debug.Log("��������������");
    }

    // ��ѡ���������ͼƬ���ʱ����
    public void OnImageClicked()
    {
        if (!isMovingOut)
        {
            StartMoveOut();
        }
    }
}