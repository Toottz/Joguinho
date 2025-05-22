using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Caso use TextMeshPro

public class PortaVoltarCasa : MonoBehaviour
{

public string nomeCenaCasa = "ApVolta"; // Nome da cena de destino
public GameObject textoInteracao; // Texto na tela ("Pressione E para entrar")
private bool jogadorPerto = false;
    public GameObject falaPersona;

void Start()
{
    if (textoInteracao != null)
        textoInteracao.SetActive(false);
        if (falaPersona != null)
            falaPersona.SetActive(false);
    }

void Update()
{
    if (jogadorPerto && Input.GetKeyDown(KeyCode.E))
    {
        SceneManager.LoadScene(nomeCenaCasa);
    }
}

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        jogadorPerto = true;

        if (textoInteracao != null)
            textoInteracao.SetActive(true);
        if (falaPersona != null)
                falaPersona.SetActive(true);
    }
}

private void desligarFala()
    {
        falaPersona.SetActive (false);
    }

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        jogadorPerto = false;

        if (textoInteracao != null)
            textoInteracao.SetActive(false);
            if (falaPersona != null)
                Invoke(nameof(desligarFala), 2f);

    }
}
}