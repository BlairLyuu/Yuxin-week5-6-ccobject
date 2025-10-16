using UnityEngine;

public class ImageDialogController : MonoBehaviour
{
    [Header("�Ի�������")]
    public GameObject dialogPanel;

    private Animator animator;
    private bool isDialogOpen = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }

        Debug.Log("�ű���������");
    }

    // ֱ����OnMouseDown������ҪUpdate
    void OnMouseDown()
    {
        Debug.Log("�������ͼƬ��");
        ToggleDialog();
    }

    void ToggleDialog()
    {
        isDialogOpen = !isDialogOpen;

        if (isDialogOpen)
        {
            OpenDialog();
        }
        else
        {
            CloseDialog();
        }
    }

    void OpenDialog()
    {
        Debug.Log("�򿪶Ի���");

        if (animator != null)
        {
            animator.speed = 0f;
        }

        if (dialogPanel != null)
        {
            dialogPanel.SetActive(true);
        }
    }

    void CloseDialog()
    {
        Debug.Log("�رնԻ���");

        if (animator != null)
        {
            animator.speed = 1f;
        }

        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }
    }
}