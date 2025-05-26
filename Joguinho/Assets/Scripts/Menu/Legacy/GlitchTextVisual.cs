using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GlitchTextVisual : MonoBehaviour
{
    public Text uiText;
    public float glitchInterval = 2f;
    public float glitchDuration = 0.15f;
    public float positionOffset = 20f;

    private string originalText;
    private Vector3 originalPosition;
    private Color originalColor;

    private Color[] glitchColors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.cyan,
        Color.magenta,
        Color.yellow
    };

    void Start()
    {
        if (uiText == null)
            uiText = GetComponent<Text>();

        originalText = uiText.text;
        originalPosition = uiText.rectTransform.anchoredPosition;
        originalColor = uiText.color;

        StartCoroutine(GlitchLoop());
    }

    IEnumerator GlitchLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(glitchInterval - 0.5f, glitchInterval + 1f));
            StartCoroutine(DoGlitch());
        }
    }

    IEnumerator DoGlitch()
    {
        // Pequeno deslocamento e cor glitch
        Vector2 offset = new Vector2(
            Random.Range(-positionOffset, positionOffset),
            Random.Range(-positionOffset, positionOffset)
        );

        uiText.rectTransform.anchoredPosition = originalPosition + (Vector3)offset;
        uiText.color = glitchColors[Random.Range(0, glitchColors.Length)];

        yield return new WaitForSeconds(glitchDuration);

        // Volta ao normal
        uiText.rectTransform.anchoredPosition = originalPosition;
        uiText.color = originalColor;
    }
}
