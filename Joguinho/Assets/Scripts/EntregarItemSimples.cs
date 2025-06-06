using System.Collections;
using UnityEngine;
using TMPro;

public class EntregarItemSimples : MonoBehaviour
{
    public string idEsperado;
    public string mensagemAposEntrega = "Item entregue com sucesso!";
    public TextMeshProUGUI legendaInteragir;     // Legenda de instrução
    public TextMeshProUGUI legendaMensagem;      // Legenda após entrega
    public float tempoLegenda = 3f;

    public GameObject objetoApareceAposEntrega;   

    private bool podeEntregar = false;
    private bool itemEntregue = false;
    public GameObject risco;

    void Start()
    {
        if (legendaInteragir != null)
            legendaInteragir.gameObject.SetActive(false);

        if (legendaMensagem != null)
            legendaMensagem.gameObject.SetActive(false);

        if (objetoApareceAposEntrega != null)
            objetoApareceAposEntrega.SetActive(false); // Garante que comece invisível
    }

    void Update()
    {
        if (itemEntregue)
            risco.SetActive(true);
        if (podeEntregar && !itemEntregue && Input.GetKeyDown(KeyCode.E))
        {
            if (InventarioSimples.Instance != null && InventarioSimples.Instance.TemItem(idEsperado))
            {
                InventarioSimples.Instance.RemoverItem(idEsperado);
                itemEntregue = true;

                if (legendaInteragir != null)
                    legendaInteragir.gameObject.SetActive(false);

                if (legendaMensagem != null)
                {
                    legendaMensagem.text = mensagemAposEntrega;
                    legendaMensagem.gameObject.SetActive(true);
                    StartCoroutine(EsconderLegendaMensagem());
                }

                if (objetoApareceAposEntrega != null)
                    objetoApareceAposEntrega.SetActive(true); // Ativa o novo objeto

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
        if (other.CompareTag("Player") && !itemEntregue)
        {
            podeEntregar = true;

            if (legendaInteragir != null)
            {
                legendaInteragir.text = "Aperte 'E' para interagir";
                legendaInteragir.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            podeEntregar = false;

            if (legendaInteragir != null)
                legendaInteragir.gameObject.SetActive(false);
        }
    }

    IEnumerator EsconderLegendaMensagem()
    {
        yield return new WaitForSeconds(tempoLegenda);

        if (legendaMensagem != null)
            legendaMensagem.gameObject.SetActive(false);
    }
}