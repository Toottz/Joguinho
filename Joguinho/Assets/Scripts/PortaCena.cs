using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaCena : MonoBehaviour
{
    public string nomeCenaDestino = "NomeDaCena";
    public string mensagemInteracao = "Pressione 'E' para entrar";

    private bool playerPerto = false;

    void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(nomeCenaDestino);
        }
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
