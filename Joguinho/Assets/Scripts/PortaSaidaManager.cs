using UnityEngine;
using TMPro;

public class PortaSaidaManager : MonoBehaviour
{
[Header("Dependências")]
public TextMeshProUGUI legendaTexto;
public float tempoLegenda = 3f;
[Header("Tarefas obrigatórias")]
public bool banhoFeito = false;
public bool dentesEscovados = false;
public bool passarinhoAlimentado = false;

private bool podeSair = false;

void Update()
{
    // Ativa flag para sair se todas as tarefas foram feitas
    if (banhoFeito && dentesEscovados && passarinhoAlimentado)
    {
        podeSair = true;
    }
}

private void OnTriggerStay(Collider other)
{
    if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
    {
        if (podeSair)
        {
            Debug.Log("✅ Todas as tarefas concluídas. Saindo...");
            // Aqui você pode carregar nova cena ou liberar porta
        }
        else
        {
            MostrarLegenda("Não terminei o que precisava, vou olhar o bloco de notas que eu anoto por lá.");
        }
    }
}

void MostrarLegenda(string texto)
{
    if (legendaTexto != null)
    {
        legendaTexto.text = texto;
        legendaTexto.gameObject.SetActive(true);
        CancelInvoke(nameof(EsconderLegenda));
        Invoke(nameof(EsconderLegenda), tempoLegenda);
    }
}

void EsconderLegenda()
{
    if (legendaTexto != null)
        legendaTexto.gameObject.SetActive(false);
}

// Estes métodos podem ser chamados por outros scripts:
public void MarcarBanhoFeito() => banhoFeito = true;
public void MarcarDentesEscovados() => dentesEscovados = true;
public void MarcarPassarinhoAlimentado() => passarinhoAlimentado = true;
}