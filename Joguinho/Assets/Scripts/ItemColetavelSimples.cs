using UnityEngine;
using TMPro;

public class ItemColetavelSimples : MonoBehaviour
{
public string idItem;
public float distanciaColeta = 1.5f;
public KeyCode teclaColetar = KeyCode.E;
public TextMeshProUGUI legendaProximidadeUI;
public string textoLegenda = "Aperte 'E' para pegar";

private Transform jogador;

void Start()
{
    jogador = GameObject.FindGameObjectWithTag("Player")?.transform;

    if (legendaProximidadeUI != null)
        legendaProximidadeUI.gameObject.SetActive(false);
}

void Update()
{
    if (jogador == null) return;

    float distancia = Vector3.Distance(transform.position, jogador.position);

    if (distancia <= distanciaColeta)
    {
        if (legendaProximidadeUI != null)
        {
            legendaProximidadeUI.text = textoLegenda;
            legendaProximidadeUI.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(teclaColetar))
        {
            if (InventarioSimples.Instance != null)
            {
                InventarioSimples.Instance.AdicionarItem(idItem);
                Debug.Log("Item coletado: " + idItem);
                if (legendaProximidadeUI != null)
                    legendaProximidadeUI.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
    else
    {
        if (legendaProximidadeUI != null)
            legendaProximidadeUI.gameObject.SetActive(false);
    }
}
}