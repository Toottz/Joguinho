/*using System.Collections;
using System.Collections;
using UnityEngine;
using TMPro;

public class PassarinhoEntregavel : MonoBehaviour
{
public string tagItemAceito = "Racao";
public Transform pontoEntrega;
public float distanciaEntrega = 2f;
public TextMeshProUGUI legendaTMP;
public float tempoLegenda = 4f;
private bool itemFoiEntregue = false;

void Update()
{
    if (itemFoiEntregue) return;

    GameObject jogador = GameObject.FindGameObjectWithTag("Player");
    if (jogador == null) return;

    float distancia = Vector3.Distance(jogador.transform.position, pontoEntrega.position);
    if (distancia <= distanciaEntrega && Input.GetKeyDown(KeyCode.E))
    {
        ItemInteractor interactor = jogador.GetComponent<ItemInteractor>();
        if (interactor != null && interactor.itemEmMao != null && interactor.itemEmMao.CompareTag(tagItemAceito))
        {
            EntregarItem(interactor.itemEmMao);
            interactor.itemEmMao = null; // libera a mão do jogador após entrega
        }
    }
}

void EntregarItem(ItemPickup item)
{
    itemFoiEntregue = true;

    if (item != null)
    {
        Destroy(item.gameObject);
    }

    if (legendaTMP != null)
    {
        StartCoroutine(MostrarLegenda("Merda, preciso comprar comida...\nMe lembro que abriu um mercado aqui perto"));
    }

    Debug.Log("🐦 Ração entregue ao passarinho.");
}

IEnumerator MostrarLegenda(string texto)
{
    legendaTMP.text = texto;
    legendaTMP.gameObject.SetActive(true);
    yield return new WaitForSeconds(tempoLegenda);
    legendaTMP.gameObject.SetActive(false);
}
}*/