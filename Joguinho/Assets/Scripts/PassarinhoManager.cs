using UnityEngine;
using TMPro;

public class PassarinhoManager : MonoBehaviour
{
public string nomeDaRacao = "Racao"; // nome do objeto da ração
public string teclaInteracao = "e";
public float distanciaInteracao = 2f;
public TextMeshProUGUI legenda;
public float tempoLegenda = 4f;

private bool racaoEntregue = false;

void Update()
{
    if (racaoEntregue) return;

    GameObject jogador = GameObject.FindGameObjectWithTag("Player");
    if (jogador == null) return;

    float distancia = Vector3.Distance(transform.position, jogador.transform.position);
    if (distancia <= distanciaInteracao && Input.GetKeyDown(teclaInteracao))
    {
        Transform itemNaMao = jogador.transform.Find("ItemNaMao");
        if (itemNaMao != null && itemNaMao.childCount > 0)
        {
            GameObject item = itemNaMao.GetChild(0).gameObject;
            if (item.name.Contains(nomeDaRacao))
            {
                Destroy(item); // Remove a ração da mão
                racaoEntregue = true;
                MostrarLegenda("Merda... preciso comprar comida, me lembro que abriu um mercado aqui perto.");
            }
        }
    }
}

void MostrarLegenda(string texto)
{
    if (legenda != null)
    {
        legenda.text = texto;
        legenda.gameObject.SetActive(true);
        CancelInvoke(nameof(EsconderLegenda));
        Invoke(nameof(EsconderLegenda), tempoLegenda);
    }
}

void EsconderLegenda()
{
    if (legenda != null)
    {
        legenda.gameObject.SetActive(false);
    }
}
}