using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class TransicaoNarrativa : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Image telaPreta;
    [SerializeField] private TextMeshProUGUI textoNarrativa;

    [Header("Configurações")]
    [SerializeField] private float duracaoFadeIn = 2f;
    [SerializeField] private float duracaoFadeOut = 2f;
    [SerializeField] private float tempoDigitacao = 20f;
    [SerializeField] private float tempoExibicaoTexto = 10f;
    [SerializeField] private string proximaCena = "NomeDaProximaCena";
    [SerializeField][TextArea] private string textoCompleto;

    private void Start()
    {
        // Garante que a tela está preta no início
        telaPreta.color = Color.black;
        textoNarrativa.text = "";

        // Inicia a sequência de transição
        StartCoroutine(SequenciaTransicao());
    }

    private IEnumerator SequenciaTransicao()
    {
        // Fade In (tela preta para transparente)
        yield return StartCoroutine(FadeTela(1f, 0f, duracaoFadeIn));

        // Digitando o texto
        yield return StartCoroutine(DigitarTexto());

        // Tempo com texto completo na tela
        yield return new WaitForSeconds(tempoExibicaoTexto);

        // Fade Out (transparente para tela preta)
        yield return StartCoroutine(FadeTela(0f, 1f, duracaoFadeOut));

        // Carrega a próxima cena
        SceneManager.LoadScene(proximaCena);
    }

    private IEnumerator FadeTela(float inicio, float fim, float duracao)
    {
        float tempo = 0f;
        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            float alpha = Mathf.Lerp(inicio, fim, tempo / duracao);
            telaPreta.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    private IEnumerator DigitarTexto()
    {
        int caracteresTotais = textoCompleto.Length;
        float tempoPorCaractere = tempoDigitacao / caracteresTotais;

        for (int i = 0; i <= caracteresTotais; i++)
        {
            textoNarrativa.text = textoCompleto.Substring(0, i);
            yield return new WaitForSeconds(tempoPorCaractere);
        }
    }
}