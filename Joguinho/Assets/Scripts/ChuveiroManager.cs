using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChuveiroManager : MonoBehaviour
{
public static ChuveiroManager Instance;
public GameObject chuveiroUI;
public TextMeshProUGUI legendaChuveiro;
public float tempoLegenda = 3f;
public GameObject telaPreta;
public AudioSource somChuveiro;
public GameObject roupaNoChao; // NOVO
public bool tomouBanho = false;

void Awake()
{
    Instance = this;

    if (chuveiroUI != null)
        chuveiroUI.SetActive(false);

    if (legendaChuveiro != null)
        legendaChuveiro.gameObject.SetActive(false);

    if (telaPreta != null)
        telaPreta.SetActive(false);

    if (roupaNoChao != null)
        roupaNoChao.SetActive(false); // garante que comece oculta
}

public void MostrarOpcoesChuveiro()
{
    if (chuveiroUI != null)
        chuveiroUI.SetActive(true);

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
}

public void EscolherBanhoSimples()
{
    StartCoroutine(RotinaBanhoSimples());
    tomouBanho = true;
}

public void EscolherBanhoComCabelo()
{
    StartCoroutine(RotinaBanhoComCabelo());
    tomouBanho = true;
}

IEnumerator RotinaBanhoSimples()
{
    FecharOpcoes();

    if (somChuveiro != null)
        somChuveiro.Play();

    yield return StartCoroutine(FadePreto(3f));

    MostrarLegenda("Água quente. Mente fria.");

    // Ativa a roupa no chão
    if (roupaNoChao != null)
        roupaNoChao.SetActive(true);
        
    tomouBanho = true;
}

IEnumerator RotinaBanhoComCabelo()
{
    FecharOpcoes();

    if (somChuveiro != null)
        somChuveiro.Play();

    yield return StartCoroutine(FadePreto(3f));

    MostrarLegenda("Sai sujeira, sai suor, sai cheirinho de chuleeeé.");
    yield return new WaitForSeconds(tempoLegenda + 0.5f);
    MostrarLegenda("Se eu lavo o cabelo, talvez o mundo lave junto…");

    // Ativa a roupa no chão
    if (roupaNoChao != null)
        roupaNoChao.SetActive(true);
        
    tomouBanho = true;
}

void MostrarLegenda(string texto)
{
    if (legendaChuveiro != null)
    {
        legendaChuveiro.text = texto;
        legendaChuveiro.gameObject.SetActive(true);
        StartCoroutine(EsconderLegenda());
    }
}

IEnumerator EsconderLegenda()
{
    yield return new WaitForSeconds(tempoLegenda);
    if (legendaChuveiro != null)
        legendaChuveiro.gameObject.SetActive(false);
}

IEnumerator FadePreto(float duracao)
{
    if (telaPreta != null)
    {
        telaPreta.SetActive(true);
        Image img = telaPreta.GetComponent<Image>();
        Color cor = img.color;
        img.color = new Color(cor.r, cor.g, cor.b, 1f);

        yield return new WaitForSeconds(duracao);

        img.color = new Color(cor.r, cor.g, cor.b, 0f);
        telaPreta.SetActive(false);
    }

    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

public void FecharOpcoes()
{
    if (chuveiroUI != null)
        chuveiroUI.SetActive(false);
}
}