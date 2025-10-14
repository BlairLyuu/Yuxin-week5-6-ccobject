using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AdvancedImageDialog : MonoBehaviour
{
    [Header("�Ի�������")]
    public GameObject dialogPanel;
    public Image dialogImage;
    public Text dialogText;
    public CanvasGroup canvasGroup;  // ���ڵ��뵭��

    [Header("��������")]
    public Sprite dialogSprite;
    [TextArea(3, 10)]
    public string dialogContent;

    [Header("��������")]
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

        // ���û��CanvasGroup���Զ����
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
        // ��ͣ����
        if (animator != null)
        {
            animator.speed = 0f;
        }

        // ��������
        if (dialogImage != null && dialogSprite != null)
        {
            dialogImage.sprite = dialogSprite;
        }
        if (dialogText != null)
        {
            dialogText.text = dialogContent;
        }

        // ��ʾ������
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
        // ����
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            canvasGroup.alpha = alpha;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        // ����
        dialogPanel.SetActive(false);

        // �ָ�����
        if (animator != null)
        {
            animator.speed = 1f;
        }
    }
}