using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GlitchTextLegacy : MonoBehaviour
{
    public Text uiText;
    public float glitchInterval = 3f;
    public float glitchDuration = 0.2f;
    public string[] glitchChars = new string[] { "#", "%", "&", "$", "@", "!", "*", "?", "/", "+", "█" };

    private string originalText;

    void Start()
    {
        if (uiText == null) uiText = GetComponent<Text>();
        originalText = uiText.text;
        StartCoroutine(GlitchLoop());
    }

    IEnumerator GlitchLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(glitchInterval - 1f, glitchInterval + 1f));
            StartCoroutine(DoGlitch());
        }
    }

    IEnumerator DoGlitch()
    {
        string glitchedText = "";
        for (int i = 0; i < originalText.Length; i++)
        {
            if (Random.value > 0.7f)
                glitchedText += glitchChars[Random.Range(0, glitchChars.Length)];
            else
                glitchedText += originalText[i];
        }

        uiText.text = glitchedText;
        yield return new WaitForSeconds(glitchDuration);
        uiText.text = originalText;
    }
}
