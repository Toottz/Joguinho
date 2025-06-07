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

    void Start()
    {
        // Verifica a ansiedade no início
        if (Estatico.Ansiedade >= limiteAnsiedade)
        {
            // Desativa todos os filhos
            DesativarFilhos();

            // Ativa a mensagem se existir
            if (textoMensagem != null)
            {
                StartCoroutine(MostrarMensagemTemporaria());
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

    IEnumerator MostrarMensagemTemporaria()
    {
        // Ativa o texto
        textoMensagem.gameObject.SetActive(true);

        // Espera o tempo definido
        yield return new WaitForSeconds(tempoMensagem);

        // Desativa o texto novamente
        textoMensagem.gameObject.SetActive(false);
    }
}