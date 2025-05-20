using UnityEngine;
using TMPro;

public class PassarinhoGaiola : MonoBehaviour
{
public string itemNecessario = "Racao";
public float distanciaInteracao = 3f;
public KeyCode teclaInteracao = KeyCode.E;
public TextMeshProUGUI legendaTexto;
public float tempoLegenda = 3f;
private bool jaAlimentado = false;

void Start()
{
    if (legendaTexto != null)
        legendaTexto.gameObject.SetActive(false);
}

void Update()
{
    if (jaAlimentado) return;

    if (Input.GetKeyDown(teclaInteracao))
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, distanciaInteracao))
        {
            if (hit.collider.gameObject == gameObject)
            {
                ItemPickup racao = ProcurarRacaoColetada();
                if (racao != null)
                {
                    racao.gameObject.SetActive(false); // remove da cena
                    MostrarLegenda("O passarinho está feliz!");
                    jaAlimentado = true;
                }
            }
        }
    }
}

ItemPickup ProcurarRacaoColetada()
{
    ItemPickup[] itens = FindObjectsOfType<ItemPickup>();

    foreach (var item in itens)
    {
        if (item.tipoItem == itemNecessario && item.foiColetado)
            return item;
    }

    return null;
}

void MostrarLegenda(string texto)
{
    if (legendaTexto != null)
    {
        legendaTexto.text = texto;
        legendaTexto.gameObject.SetActive(true);
        Invoke(nameof(EsconderLegenda), tempoLegenda);
    }
}

void EsconderLegenda()
{
    if (legendaTexto != null)
        legendaTexto.gameObject.SetActive(false);
}
}