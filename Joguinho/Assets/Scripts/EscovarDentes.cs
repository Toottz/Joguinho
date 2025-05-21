using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EscovarDentes : MonoBehaviour
{
public GameObject textoUI; // Texto "Pressione E para escovar os dentes"
public GameObject telaPreta; // UI de fade (Image com fundo preto e CanvasGroup ou Image alpha)
public AudioSource somEscovando; // som da escovação
public TextMeshProUGUI legendaTexto; // legenda exibida após escovação
public float tempoLegenda = 3f;
private bool dentroDaArea = false;
public bool escovou = false;

void Start()
{
    if (textoUI != null)
        textoUI.SetActive(false);

    if (telaPreta != null)
        telaPreta.SetActive(false);

    if (legendaTexto != null)
        legendaTexto.gameObject.SetActive(false);
}

void Update()
{
    if (dentroDaArea && !escovou && Input.GetKeyDown(KeyCode.E))
    {
        StartCoroutine(RotinaEscovarDentes());
    }
}

IEnumerator RotinaEscovarDentes()
{
    escovou = true;

    if (textoUI != null)
        textoUI.SetActive(false);

    if (telaPreta != null)
    {
        telaPreta.SetActive(true);
        Image img = telaPreta.GetComponent<Image>();
        if (img != null)
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    }

    if (somEscovando != null)
        somEscovando.Play();

    yield return new WaitForSeconds(2f); // tempo escovando com a tela preta

    if (telaPreta != null)
        telaPreta.SetActive(false);

    if (legendaTexto != null)
    {
        legendaTexto.text = "Hálito: 80% menos mortal.";
        legendaTexto.gameObject.SetActive(true);
        yield return new WaitForSeconds(tempoLegenda);
        legendaTexto.gameObject.SetActive(false);
    }
}

void OnTriggerEnter(Collider other)
{
    if (!escovou && other.CompareTag("Player"))
    {
        dentroDaArea = true;
        if (textoUI != null)
            textoUI.SetActive(true);
            legendaTexto.text = "Pressione 'E' para escovar os dentes";
    }
}

void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        dentroDaArea = false;
        if (textoUI != null)
            textoUI.SetActive(false);
    }
}
}