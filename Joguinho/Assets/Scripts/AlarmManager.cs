using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class AlarmManager : MonoBehaviour
{
public AudioSource somAlarme;
public GameObject telaPreta;
public GameObject videoPlayerUI;
public VideoPlayer videoPlayer;
public GameObject player;
public GameObject cameraObj;
public GameObject telaAlarmeUI;
public GameObject celularCompleto;
public GameObject perguntaBotoesUI;
public TextMeshProUGUI legendaTexto;
public float tempoLegenda = 3f;
public TutorialMensagemManager tutorialMensagemManager;
private float tempoInicio;
private bool alarmeTocando = false;
private bool alarmeDesligado = false;
private bool ignorouAlarme = false;
private bool videoTocando = false;

public bool AlarmeEsperandoInteracao { get; private set; } = false;

void Start()
{
    if (telaPreta != null)
    {
        telaPreta.SetActive(true);
        Color cor = telaPreta.GetComponent<Image>().color;
        cor.a = 1f;
        telaPreta.GetComponent<Image>().color = cor;
    }

    if (videoPlayerUI != null)
        videoPlayerUI.SetActive(false);

    if (legendaTexto != null)
        legendaTexto.gameObject.SetActive(false);

    if (perguntaBotoesUI != null)
        perguntaBotoesUI.SetActive(false);

    StartCoroutine(IniciarSequencia());
}

IEnumerator IniciarSequencia()
{
    yield return new WaitForSeconds(1f);
    StartCoroutine(FazerFadeOutTelaPreta());

    PlayAlarm();
    tempoInicio = Time.time;
    AlarmeEsperandoInteracao = true;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    yield return new WaitForSeconds(10f);

    if (!alarmeDesligado)
    {
        ignorouAlarme = true;
        StopAlarm();
        AlarmeEsperandoInteracao = false;
        StartCoroutine(VoltarADormir());

        if (telaAlarmeUI != null)
            telaAlarmeUI.SetActive(false);

        if (tutorialMensagemManager != null)
            tutorialMensagemManager.OcultarMensagem();
    }
}

public void DesligarAlarmeViaBotao()
{
    if (alarmeDesligado || ignorouAlarme) return;

    float tempoResposta = Time.time - tempoInicio;
    StopAlarm();
    alarmeDesligado = true;
    AlarmeEsperandoInteracao = false;

    if (telaAlarmeUI != null)
        telaAlarmeUI.SetActive(false);

    if (celularCompleto != null)
        celularCompleto.SetActive(true);

    CelularUIManager uiManager = FindObjectOfType<CelularUIManager>();
    if (uiManager != null && uiManager.menuApps != null)
        uiManager.menuApps.SetActive(true);

    if (tempoResposta <= 5f)
        MostrarLegenda("Pelo menos isso parou...");
    else if (tempoResposta <= 10f)
        MostrarLegenda("Por que sempre parece que tô atrasada?");

    if (tutorialMensagemManager != null)
        tutorialMensagemManager.OcultarMensagem();

    Invoke(nameof(legenda_preciso_ir_ao_banheiro), 18f);
    }

void MostrarLegenda(string texto)
{
    if (legendaTexto != null)
    {
        legendaTexto.text = texto;
        legendaTexto.gameObject.SetActive(true);
        StartCoroutine(EsconderLegendaDepoisDeSegundos());
    }
}

IEnumerator EsconderLegendaDepoisDeSegundos()
{
    yield return new WaitForSeconds(tempoLegenda);
    if (legendaTexto != null)
        legendaTexto.gameObject.SetActive(false);
}

IEnumerator VoltarADormir()
{
    if (telaPreta != null)
    {
        telaPreta.SetActive(true);
        Image img = telaPreta.GetComponent<Image>();
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
    }

    yield return StartCoroutine(FazerFadeInTelaPreta());

    if (videoPlayerUI != null)
        videoPlayerUI.SetActive(true);

    if (videoPlayer != null)
    {
        videoPlayer.Play();
        videoTocando = true;
    }

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    yield return StartCoroutine(FazerFadeOutTelaPreta());

    MostrarLegenda("Nem comecei o dia e já tô esgotada...");

    yield return new WaitForSeconds(10f);

    if (perguntaBotoesUI != null)
        perguntaBotoesUI.SetActive(true);

    yield return new WaitForSeconds(10f);

    if (perguntaBotoesUI.activeSelf)
    {
        perguntaBotoesUI.SetActive(false);

        if (videoPlayerUI != null)
            videoPlayerUI.SetActive(false);

        if (celularCompleto != null)
            celularCompleto.SetActive(true);

        videoTocando = false;
    }
}

public void BotaoContinuarNoCelular()
{
    if (perguntaBotoesUI != null)
        perguntaBotoesUI.SetActive(false);

        //MostrarLegenda("Preciso ir ao banheiro daqui a pouco");
        Invoke(nameof(legenda_preciso_ir_ao_banheiro), 5f);
    }

public void BotaoSairDoCelular()
{
    if (perguntaBotoesUI != null)
        perguntaBotoesUI.SetActive(false);

    if (videoPlayer != null)
        videoPlayer.Pause();

    if (videoPlayerUI != null)
        videoPlayerUI.SetActive(false);

    if (celularCompleto != null)
        celularCompleto.SetActive(true);

    Invoke(nameof(legenda_preciso_ir_ao_banheiro), 2f);
    //MostrarLegenda("Preciso ir ao banheiro");
    videoTocando = false;
}

public void legenda_preciso_ir_ao_banheiro()
    {
        MostrarLegenda("Preciso ir ao banheiro");
    }

public bool VideoEstaTocando() => videoTocando;
public bool AlarmeAtivo() => alarmeTocando;
public bool AlarmeJaFoiDesligado() => alarmeDesligado;

public void StopAlarm()
{
    if (somAlarme != null && somAlarme.isPlaying)
        somAlarme.Stop();

    alarmeTocando = false;
}

public void PlayAlarm()
{
    if (somAlarme != null && !somAlarme.isPlaying)
        somAlarme.Play();

    alarmeTocando = true;
    alarmeDesligado = false;
}

IEnumerator FazerFadeOutTelaPreta()
{
    if (telaPreta == null) yield break;
    float tempo = 1.5f;
    float t = 0f;
    Image imagem = telaPreta.GetComponent<Image>();
    Color cor = imagem.color;

    while (t < tempo)
    {
        t += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, t / tempo);
        imagem.color = new Color(cor.r, cor.g, cor.b, alpha);
        yield return null;
    }

    telaPreta.SetActive(false);
}

IEnumerator FazerFadeInTelaPreta()
{
    if (telaPreta == null) yield break;
    telaPreta.SetActive(true);
    float tempo = 1.5f;
    float t = 0f;
    Image imagem = telaPreta.GetComponent<Image>();
    Color cor = imagem.color;

    while (t < tempo)
    {
        t += Time.deltaTime;
        float alpha = Mathf.Lerp(0f, 1f, t / tempo);
        imagem.color = new Color(cor.r, cor.g, cor.b, alpha);
        yield return null;
    }
}
}