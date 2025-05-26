using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GlitchTextPerCharacterTMP : MonoBehaviour
{
    public float glitchInterval = 2f;
    public float glitchDuration = 0.1f;
    public float positionOffset = 5f;
    public float colorChangeChance = 0.7f;

    public Color[] glitchColors = new Color[]
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.cyan,
        Color.magenta,
        Color.yellow
    };

    private TextMeshProUGUI tmpText;
    private TMP_TextInfo textInfo;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
        tmpText.ForceMeshUpdate();
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
        tmpText.ForceMeshUpdate();
        textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;
            Vector3[] vertices = textInfo.meshInfo[materialIndex].vertices;

            Vector3 offset = new Vector3(
                Random.Range(-positionOffset, positionOffset),
                Random.Range(-positionOffset, positionOffset),
                0
            );

            // Move cada um dos 4 vértices do caractere
            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j] += offset;
            }

            // Muda a cor do caractere
            Color32[] colors = textInfo.meshInfo[materialIndex].colors32;
            Color32 randomColor = glitchColors[Random.Range(0, glitchColors.Length)];

            if (Random.value < colorChangeChance)
            {
                for (int j = 0; j < 4; j++)
                {
                    colors[vertexIndex + j] = randomColor;
                }
            }
        }

        // Atualiza o mesh
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
            tmpText.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }

        yield return new WaitForSeconds(glitchDuration);
        tmpText.ForceMeshUpdate(); // Restaura o original
    }
}
