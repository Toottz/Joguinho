using System.Collections;
using UnityEngine;
using TMPro;

public class EntregarItemSimples : MonoBehaviour
{
public string idEsperado;
public string mensagemAposEntrega = "Item entregue com sucesso!";
public TextMeshProUGUI legendaUI;
public float tempoLegenda = 3f;
private bool podeEntregar = false;
private bool itemEntregue = false;

void Start()
{
    if (legendaUI != null)
        legendaUI.gameObject.SetActive(false);
}

void Update()
{
    if (podeEntregar && !itemEntregue && Input.GetKeyDown(KeyCode.E))
    {
        if (InventarioSimples.Instance != null && InventarioSimples.Instance.TemItem(idEsperado))
        {
            InventarioSimples.Instance.RemoverItem(idEsperado);
            itemEntregue = true;

            if (legendaUI != null)
            {
                legendaUI.text = mensagemAposEntrega;
                legendaUI.gameObject.SetActive(true);
                StartCoroutine(EsconderLegenda());
            }

            Debug.Log("✅ Item entregue: " + idEsperado);
        }
        else
        {
            Debug.Log("❌ Você não tem o item necessário.");
        }
    }
}

void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        podeEntregar = true;
    }
}

void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        podeEntregar = false;
    }
}

IEnumerator EsconderLegenda()
{
    yield return new WaitForSeconds(tempoLegenda);

    if (legendaUI != null)
        legendaUI.gameObject.SetActive(false);
}
}