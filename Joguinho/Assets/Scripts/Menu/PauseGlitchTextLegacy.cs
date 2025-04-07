using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class PauseGlitchTextLegacy : MonoBehaviour
{
    public float glitchInterval = 2f;
    public float glitchDuration = 0.1f;
    public float positionOffset = 20f;
    public Color[] glitchColors;

    private Text textComponent;
    private Vector3 originalPosition;
    private Color originalColor;

    void Start()
    {
        textComponent = GetComponent<Text>();
        originalPosition = transform.localPosition;
        originalColor = textComponent.color;

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

        StartCoroutine(GlitchLoop());
    }

    IEnumerator GlitchLoop()
    {
        while (true)
        {
            yield return WaitForSecondsUnscaled(Random.Range(glitchInterval - 0.3f, glitchInterval + 0.5f));
            StartCoroutine(DoGlitch());
        }
    }

    IEnumerator DoGlitch()
    {
        Vector3 offset = new Vector3(
            Random.Range(-positionOffset, positionOffset),
            Random.Range(-positionOffset, positionOffset),
            0f
        );

        textComponent.color = glitchColors[Random.Range(0, glitchColors.Length)];
        transform.localPosition = originalPosition + offset;

        yield return WaitForSecondsUnscaled(glitchDuration);

        transform.localPosition = originalPosition;
        textComponent.color = originalColor;
    }

    WaitForSecondsRealtime WaitForSecondsUnscaled(float seconds)
    {
        return new WaitForSecondsRealtime(seconds);
    }
}
