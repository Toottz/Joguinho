using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGlitch : MonoBehaviour
{
    public float glitchInterval = 3f;
    public float glitchDuration = 0.15f;
    public float positionOffset = 10f;
    public Color[] glitchColors;

    private Vector3 originalPosition;
    private Color originalColor;
    private Image buttonImage;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        originalPosition = transform.localPosition;
        originalColor = buttonImage.color;

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
            yield return new WaitForSeconds(Random.Range(glitchInterval - 0.5f, glitchInterval + 1f));
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

        transform.localPosition = originalPosition + offset;
        buttonImage.color = glitchColors[Random.Range(0, glitchColors.Length)];

        yield return new WaitForSeconds(glitchDuration);

        transform.localPosition = originalPosition;
        buttonImage.color = originalColor;
    }
}
