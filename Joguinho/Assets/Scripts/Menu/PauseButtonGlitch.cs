using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Image))]
public class PuaseButtonGlitch : MonoBehaviour
{
    public float glitchInterval = 2f;
    public float glitchDuration = 0.1f;
    public float positionOffset = 5f;
    public Color[] glitchColors;

    private Image imageComponent;
    private Text legacyText;
    private Vector3 originalPosition;
    private Color originalImageColor;
    private Color originalTextColor;
    private Coroutine glitchCoroutine;

    void Awake()
    {
        imageComponent = GetComponent<Image>();
        legacyText = GetComponentInChildren<Text>(); // Pega o texto do botão
        originalPosition = transform.localPosition;
        originalImageColor = imageComponent.color;
        if (legacyText != null) originalTextColor = legacyText.color;

        if (glitchColors == null || glitchColors.Length == 0)
        {
            glitchColors = new Color[]
            {
                Color.red,
                Color.green,
                Color.blue,
                Color.magenta,
                Color.yellow
            };
        }
    }

    void OnEnable()
    {
        if (glitchCoroutine != null)
            StopCoroutine(glitchCoroutine);

        glitchCoroutine = StartCoroutine(GlitchLoop());
    }

    void OnDisable()
    {
        if (glitchCoroutine != null)
            StopCoroutine(glitchCoroutine);
    }

    IEnumerator GlitchLoop()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(Random.Range(glitchInterval - 0.3f, glitchInterval + 0.5f));
            StartCoroutine(ExecutarGlitch());
        }
    }

    IEnumerator ExecutarGlitch()
    {
        Vector3 offset = new Vector3(
            Random.Range(-positionOffset, positionOffset),
            Random.Range(-positionOffset, positionOffset),
            0f
        );

        Color newColor = glitchColors[Random.Range(0, glitchColors.Length)];
        imageComponent.color = newColor;
        if (legacyText != null)
            legacyText.color = newColor;

        transform.localPosition = originalPosition + offset;

        yield return new WaitForSecondsRealtime(glitchDuration);

        transform.localPosition = originalPosition;
        imageComponent.color = originalImageColor;
        if (legacyText != null)
            legacyText.color = originalTextColor;
    }
    
    public void ReiniciarGlitch()
    {
        if (glitchCoroutine != null)
            StopCoroutine(glitchCoroutine);

        glitchCoroutine = StartCoroutine(GlitchLoop());
    }
}
