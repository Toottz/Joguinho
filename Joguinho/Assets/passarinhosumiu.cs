using UnityEngine;
using TMPro;
using System.Collections;

public class passarinhosumiu : MonoBehaviour
{
    [Header("Configurações")]
    [Tooltip("Valor de ansiedade que ativa o sistema")]
    public float limiteAnsiedade = 80f;

    [Header("Referências")]
    [Tooltip("Objeto TextMeshPro que será ativado temporariamente")]
    public TextMeshProUGUI textoMensagem;

    [Tooltip("Tempo que a mensagem ficará visível (segundos)")]
    public float tempoMensagem = 3f;

    [Tooltip("Tempo de espera antes de mostrar a mensagem (segundos)")]
    public float delayInicial = 10f;

    void Start()
    {
        // Verifica a ansiedade no início
        if (Estatico.Ansiedade >= limiteAnsiedade)
        {
            // Desativa todos os filhos imediatamente
            DesativarFilhos();

            // Inicia a corrotina para mostrar mensagem com delay
            if (textoMensagem != null)
            {
                StartCoroutine(MostrarMensagemComDelay());
            }
        }
    }

    void DesativarFilhos()
    {
        // Desativa todos os objetos filhos diretos
        foreach (Transform filho in transform)
        {
            filho.gameObject.SetActive(false);
        }
    }

    IEnumerator MostrarMensagemComDelay()
    {
        // Espera o delay inicial de 10 segundos
        yield return new WaitForSeconds(delayInicial);

        // Ativa o texto
        textoMensagem.gameObject.SetActive(true);

        // Espera o tempo de exibição
        yield return new WaitForSeconds(tempoMensagem);

        // Desativa o texto novamente
        textoMensagem.gameObject.SetActive(false);
    }
}