using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PortaCena : MonoBehaviour
{
    public string nomeCenaDestino = "NomeDaCena";
    public string mensagemInteracao = "Pressione 'E' para entrar";
    [SerializeField]
    private bool tarefas_Finalizadas = false;

    private bool playerPerto = false;

    public GameObject textoUI;
    public TextMeshProUGUI legendaTexto;
    [TextArea]
    public string mensagemProibido = "FeedBack";

    void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.E) && tarefas_Finalizadas)
        {
            SceneManager.LoadScene(nomeCenaDestino);
        }
        if (playerPerto && Input.GetKeyDown(KeyCode.E) && !tarefas_Finalizadas)
        {
            if (textoUI != null)
                textoUI.SetActive(true);
            legendaTexto.text = mensagemProibido;
            Invoke("desligarFala", 3f);

        }
    }

    private void desligarFala()
    {
        textoUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;
            InteracaoUIManager.Instance?.MostrarTexto(mensagemInteracao);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;
            InteracaoUIManager.Instance?.EsconderTexto();
        }
    }
}
