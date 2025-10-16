using UnityEngine;

public class ImageDialogController : MonoBehaviour
{
    [Header("对话框设置")]
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

        Debug.Log("脚本已启动！");
    }

    // 直接用OnMouseDown，不需要Update
    void OnMouseDown()
    {
        Debug.Log("点击到了图片！");
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
        Debug.Log("打开对话框");

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
        Debug.Log("关闭对话框");

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