using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TransitionManager : MonoBehaviour
{
    [Header("UI References")]
    public Image blackPanel;
    public GameObject nextButton;
    public GameObject textDisplay;
    public TextMeshProUGUI displayText;
    public GameObject enterButton;

    [Header("Transition Settings")]
    public float fadeSpeed = 1f;
    public string textToShow = "";

    [Header("Opening Animation")]
    public OpeningAnimation openingAnimation;

    private bool isTransitioning = false;

    void Start()
    {
        Debug.Log("TransitionManager Started");

        if (blackPanel != null)
        {
            SetPanelAlpha(0f);
        }

        if (textDisplay != null)
        {
            textDisplay.SetActive(false);
        }

        if (enterButton != null)
        {
            enterButton.SetActive(false);
        }
    }

    public void OnNextButtonClicked()
    {
        Debug.Log("Next Button Clicked!");

        if (!isTransitioning)
        {
            StartCoroutine(FadeToBlackSequence());
        }
    }

    public void OnEnterButtonClicked()
    {
        Debug.Log("Enter Button Clicked!");

        if (!isTransitioning)
        {
            StartCoroutine(StartOpeningSequence());
        }
    }

    IEnumerator FadeToBlackSequence()
    {
        isTransitioning = true;
        Debug.Log("Starting fade to black");

        if (nextButton != null)
        {
            nextButton.SetActive(false);
        }

        yield return StartCoroutine(FadePanelTo(1f));

        yield return new WaitForSeconds(0.5f);

        if (textDisplay != null)
        {
            textDisplay.SetActive(true);

            if (displayText != null && !string.IsNullOrEmpty(textToShow))
            {
                displayText.text = textToShow;
            }
        }

        if (enterButton != null)
        {
            enterButton.SetActive(true);
        }

        isTransitioning = false;
    }

    IEnumerator StartOpeningSequence()
    {
        isTransitioning = true;
        Debug.Log("Starting opening sequence");

        if (textDisplay != null)
        {
            textDisplay.SetActive(false);
        }

        if (enterButton != null)
        {
            enterButton.SetActive(false);
        }

        yield return new WaitForSeconds(0.3f);

        yield return StartCoroutine(FadePanelTo(0f));

        if (openingAnimation != null)
        {
            openingAnimation.enabled = true;
        }

        isTransitioning = false;
        this.enabled = false;
    }

    IEnumerator FadePanelTo(float targetAlpha)
    {
        if (blackPanel == null) yield break;

        float currentAlpha = blackPanel.color.a;
        float elapsedTime = 0f;
        float duration = 1f / fadeSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / duration);
            SetPanelAlpha(newAlpha);
            yield return null;
        }

        SetPanelAlpha(targetAlpha);
    }

    void SetPanelAlpha(float alpha)
    {
        if (blackPanel != null)
        {
            Color color = blackPanel.color;
            color.a = alpha;
            blackPanel.color = color;
        }
    }
}