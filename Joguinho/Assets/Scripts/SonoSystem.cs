using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class SonoSystem : MonoBehaviour
{
    [Header("Configura��es UI")]
    public Image sonoBar;

    [Header("Par�metros de Sono")]
    public float maxSono = 100f;
    public float sonoAoAcordar = 20f;
    public float inicioRecuperacao = 480f;    // 8h em minutos
    public float fimRecuperacao = 1320f;      // 22h em minutos
    public float taxaReducaoBase = 0.1f;      // Redu��o por minuto

    [Header("Efeito Vignette (Piscadas R�pidas)")]
    public float minVignette = 0.125f;
    public float maxVignette = 1f;
    public float duracaoFechar = 0.5f;       // Tempo para fechar os olhos
    public float duracaoAbrir = 0.5f;        // Tempo para abrir os olhos
    public float tempoEntrePiscadasBase = 3f; // Intervalo base entre piscadas
    public float sonoMinimoParaEfeito = 80f;  // Sono necess�rio para ativar

    private VolumeProfile volumeProfile;
    private Vignette vignette;
    private float timerPiscada;
    private float proximaPiscada;
    private bool piscando = false;
    private Coroutine piscadaCoroutine;

    public GameObject SetaUp;
    public GameObject SetaDown;

    void Start()
    {
        if (Estatico.Sono <= 0) Estatico.Sono = sonoAoAcordar;
        SetupPostProcessing();
        UpdateUI();
        CalcularProximaPiscada();
    }

    void SetupPostProcessing()
    {
        Volume globalVolume = FindObjectOfType<Volume>();
        if (globalVolume != null)
        {
            volumeProfile = globalVolume.profile;
            if (volumeProfile.TryGet(out vignette))
            {
                vignette.intensity.value = minVignette;
                vignette.active = true;
            }
        }
    }

    void Update()
    {
        UpdateSono();
        UpdateVignetteEffect();
        UpdateUI();
    }

    void UpdateSono()
    {
        // Fora do per�odo de recupera��o: diminui o sono
        if (Estatico.tempoEmMinutos < inicioRecuperacao || Estatico.tempoEmMinutos >= fimRecuperacao)
        {
            Estatico.Sono = Mathf.Max(sonoAoAcordar, Estatico.Sono - taxaReducaoBase * Time.deltaTime);
        }
        // Dentro do per�odo: aumenta baseado no tempo
        else
        {
            float progresso = Mathf.InverseLerp(inicioRecuperacao, fimRecuperacao, Estatico.tempoEmMinutos);
            Estatico.Sono = Mathf.Lerp(sonoAoAcordar, maxSono, progresso);
        }
    }

    void UpdateVignetteEffect()
    {
        if (vignette == null || Estatico.Sono < sonoMinimoParaEfeito)
        {
            if (vignette != null) vignette.intensity.value = minVignette;
            return;
        }

        timerPiscada += Time.deltaTime;

        // Dispara piscada quando atinge o intervalo
        if (!piscando && timerPiscada >= proximaPiscada)
        {
            Piscar();
        }
    }

    void Piscar()
    {
        if (piscadaCoroutine != null)
            StopCoroutine(piscadaCoroutine);

        piscadaCoroutine = StartCoroutine(ExecutarPiscada());
    }

    IEnumerator ExecutarPiscada()
    {
        piscando = true;

        // Fechar os olhos (0.125 -> 1 em 0.5s)
        float timer = 0f;
        while (timer < duracaoFechar)
        {
            timer += Time.deltaTime;
            float progresso = Mathf.Clamp01(timer / duracaoFechar);
            vignette.intensity.value = Mathf.Lerp(minVignette, maxVignette, progresso);
            yield return null;
        }

        // Garante que chegou no m�ximo
        vignette.intensity.value = maxVignette;

        // Abrir os olhos (1 -> 0.125 em 0.5s)
        timer = 0f;
        while (timer < duracaoAbrir)
        {
            timer += Time.deltaTime;
            float progresso = Mathf.Clamp01(timer / duracaoAbrir);
            vignette.intensity.value = Mathf.Lerp(maxVignette, minVignette, progresso);
            yield return null;
        }

        // Garante que voltou ao m�nimo
        vignette.intensity.value = minVignette;

        // Prepara pr�xima piscada
        timerPiscada = 0f;
        CalcularProximaPiscada();
        piscando = false;
    }

    void CalcularProximaPiscada()
    {
        float sonoNormalizado = Mathf.InverseLerp(sonoMinimoParaEfeito, maxSono, Estatico.Sono);
        proximaPiscada = Mathf.Lerp(tempoEntrePiscadasBase, tempoEntrePiscadasBase * 3f, sonoNormalizado);
    }

    public void Dormir(float valor)
    {
        Estatico.Sono = Mathf.Max(0, Estatico.Sono - valor);
        if (valor > 0f)
        {
            SetaUp.SetActive(true);
        }
        if (valor < 0f)
        {
            SetaDown.SetActive(true);
        }
        UpdateUI();
    }

    void UpdateUI()
    {
        if (sonoBar != null)
            sonoBar.fillAmount = Estatico.Sono / maxSono;
    }

    public void Resetar()
    {
        Estatico.Sono = sonoAoAcordar;
        if (vignette != null) vignette.intensity.value = minVignette;
        if (piscadaCoroutine != null)
        {
            StopCoroutine(piscadaCoroutine);
            piscadaCoroutine = null;
        }
        piscando = false;

        UpdateUI();
    }

    public void ResetarCicloSono()
    {
        Estatico.Sono = sonoAoAcordar;
        if (vignette != null) vignette.intensity.value = minVignette;

        if (piscadaCoroutine != null)
        {
            StopCoroutine(piscadaCoroutine);
            piscadaCoroutine = null;
        }
        piscando = false;
    }
}