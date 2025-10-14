using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedImageDialog : MonoBehaviour
{
    [Header("对话框设置")]
    public GameObject dialogPanel;
    public Image dialogImage;
    public Text dialogText;
    public CanvasGroup canvasGroup;  // 用于淡入淡出

    [Header("内容设置")]
    public Sprite dialogSprite;
    [TextArea(3, 10)]
    public string dialogContent;

    [Header("动画设置")]
    public float fadeSpeed = 2f;

    private Animator animator;
    private bool isDialogOpen = false;
    private bool isAnimating = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (dialogPanel != null)
        {
            dialogPanel.SetActive(false);
        }

        // 如果没有CanvasGroup，自动添加
        if (canvasGroup == null && dialogPanel != null)
        {
            canvasGroup = dialogPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = dialogPanel.AddComponent<CanvasGroup>();
            }
        }
    }

    void OnMouseDown()
    {
        if (!isAnimating)
        {
            StartCoroutine(ToggleDialogCoroutine());
        }
    }

    IEnumerator ToggleDialogCoroutine()
    {
        isAnimating = true;
        isDialogOpen = !isDialogOpen;

        if (isDialogOpen)
        {
            yield return StartCoroutine(OpenDialogCoroutine());
        }
        else
        {
            yield return StartCoroutine(CloseDialogCoroutine());
        }

        isAnimating = false;
    }

    IEnumerator OpenDialogCoroutine()
    {
        // 暂停动画
        if (animator != null)
        {
            animator.speed = 0f;
        }

        // 设置内容
        if (dialogImage != null && dialogSprite != null)
        {
            dialogImage.sprite = dialogSprite;
        }
        if (dialogText != null)
        {
            dialogText.text = dialogContent;
        }

        // 显示并淡入
        dialogPanel.SetActive(true);
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = alpha;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    IEnumerator CloseDialogCoroutine()
    {
        // 淡出
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = alpha;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // 隐藏
        dialogPanel.SetActive(false);

        // 恢复动画
        if (animator != null)
        {
            animator.speed = 1f;
        }
    }
}