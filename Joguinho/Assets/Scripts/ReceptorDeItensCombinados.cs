using UnityEngine;
using System.Collections.Generic;

public class ReceptorDeItensCombinados : MonoBehaviour
{
    [Header("IDs dos Itens Requeridos")]
    public List<int> idsRequeridos; // IDs dos itens necessários para formar o item final
    private List<int> idsEntregues = new List<int>();

    [Header("Objeto que será ativado após entrega")]
    public GameObject itemFinalParaColetar;

    [Header("Legendas e Feedback")]
    public GameObject legendaFeedback;
    public string textoQuandoCompleto = "Algo foi criado!";

    void Start()
    {
        if (itemFinalParaColetar != null)
            itemFinalParaColetar.SetActive(false);

        if (legendaFeedback != null)
            legendaFeedback.SetActive(false);
    }

    public void EntregarItem(int idItem)
    {
        if (idsEntregues.Contains(idItem)) return;

        if (idsRequeridos.Contains(idItem))
        {
            idsEntregues.Add(idItem);
            VerificarCombinacao();
        }
    }

    void VerificarCombinacao()
    {
        if (idsEntregues.Count == idsRequeridos.Count)
        {
            bool todosPresentes = true;
            foreach (int id in idsRequeridos)
            {
                if (!idsEntregues.Contains(id))
                {
                    todosPresentes = false;
                    break;
                }
            }

            if (todosPresentes)
                CriarItemFinal();
        }
    }

    void CriarItemFinal()
    {
        if (itemFinalParaColetar != null)
            itemFinalParaColetar.SetActive(true);

        if (legendaFeedback != null)
        {
            legendaFeedback.SetActive(true);
            legendaFeedback.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = textoQuandoCompleto;
            StartCoroutine(EsconderLegenda());
        }
    }

    System.Collections.IEnumerator EsconderLegenda()
    {
        yield return new WaitForSeconds(3f);
        if (legendaFeedback != null)
            legendaFeedback.SetActive(false);
    }
}