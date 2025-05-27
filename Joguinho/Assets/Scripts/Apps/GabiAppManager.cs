using UnityEngine;
using UnityEngine.Video;

public class GabiAppManager : MonoBehaviour
{
public GameObject telaGabi;
public GameObject[] outrasTelas;
public VideoPlayer videoPlayer;
void Start()
{
    if (telaGabi != null)
        telaGabi.SetActive(false);

    if (videoPlayer != null)
    {
        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.Stop();
    }
}

public void AbrirTelaGabi()
{
    if (!CelularEstaAberto())
    {
        Debug.Log("⛔ O celular precisa estar aberto para usar o app.");
        return;
    }

    FecharTodasAsTelas();

    if (telaGabi != null)
        telaGabi.SetActive(true);

    if (videoPlayer != null)
    {
        videoPlayer.Stop();  // reinicia sempre do início
        Invoke("PlayVideo", 0.05f); // pequena espera para evitar travamento
    }

    Debug.Log("📱 Tela Gabi aberta");
}

void PlayVideo()
{
    if (videoPlayer != null)
        videoPlayer.Play();
}

void FecharTodasAsTelas()
{
    foreach (GameObject tela in outrasTelas)
    {
        if (tela != null)
            tela.SetActive(false);
    }

    if (videoPlayer != null)
        videoPlayer.Stop();
}

bool CelularEstaAberto()
{
    CelularController celular = FindObjectOfType<CelularController>();
    return celular != null && celular.CelularEstaAberto();
}
}