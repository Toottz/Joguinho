using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeedBackLegendas : MonoBehaviour
{
public GameObject textoUI; 
public TextMeshProUGUI legendaTexto;
public float tempoLegenda = 3f;
private bool dentroDaArea = false;
private bool escovou = false;
public string mensagemInteracao = "FeedBack";

void Start()
{
    if (textoUI != null)
        textoUI.SetActive(false);

    if (legendaTexto != null)
        legendaTexto.gameObject.SetActive(false);

}

void Update()
{
        //if (dentroDaArea && !escovou && Input.GetKeyDown(KeyCode.E))
        // {
        // StartCoroutine(RotinaEscovarDentes());
        // }
    }

    //IEnumerator RotinaEscovarDentes()
    //{
    //escovou = true;

    // if (textoUI != null)
    // textoUI.SetActive(false);

    //if (telaPreta != null)
    //{
    //telaPreta.SetActive(true);
    //Image img = telaPreta.GetComponent<Image>();
    //    if (img != null)
    //img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
    //}

    //if (somEscovando != null)
    //somEscovando.Play();

    //   yield return new WaitForSeconds(2f); // tempo escovando com a tela preta

    // if (telaPreta != null)
    //telaPreta.SetActive(false);

    // if (legendaTexto != null)
    //{
    //  legendaTexto.text = "Hálito: 80% menos mortal.";
    //   legendaTexto.gameObject.SetActive(true);
    //  yield return new WaitForSeconds(tempoLegenda);
    //  legendaTexto.gameObject.SetActive(false);
    //}
    //}

    void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        dentroDaArea = true;
        if (textoUI != null)
            textoUI.SetActive(true);
            legendaTexto.text = mensagemInteracao;
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