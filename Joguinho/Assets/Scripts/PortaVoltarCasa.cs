using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Caso use TextMeshPro

public class PortaVoltarCasa : MonoBehaviour
{
public string nomeCenaCasa = "ApVolta"; // Nome da cena de destino
public GameObject textoInteracao; // Texto na tela ("Pressione E para entrar")
private bool jogadorPerto = false;
void Start()
{
    if (textoInteracao != null)
        textoInteracao.SetActive(false);
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
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        jogadorPerto = false;

        if (textoInteracao != null)
            textoInteracao.SetActive(false);
    }
}
}