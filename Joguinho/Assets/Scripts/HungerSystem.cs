using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class HungerSystem : MonoBehaviour
{
    [Header("Configurações UI")]
    public Image hungerBar;

    [Header("Parâmetros de Fome")]
    public float maxHunger = 100f;
    public float hungerDecreaseRate = 5f;
    public float timeBetweenDecreases = 60f;
    public float limiarSemEfeitos = 40f; // Acima deste valor, efeito desligado

    [Header("Efeito Visual")]
    [Range(0, 1)] public float maxChromaticAberration = 0.8f;

    private float timer;
    private VolumeProfile volumeProfile;
    private ChromaticAberration chromaticAberration;

    void Start()
    {
        SetupPostProcessing();
        UpdateHungerUI();
    }

    void SetupPostProcessing()
    {
        Volume globalVolume = FindObjectOfType<Volume>();
        if (globalVolume != null)
        {
            volumeProfile = globalVolume.profile;

            if (!volumeProfile.TryGet(out chromaticAberration))
                Debug.LogWarning("Chromatic Aberration não encontrado!");
            else
                chromaticAberration.active = true; // Garante que está ativo
        }
    }

    void Update()
    {
        // Sistema de diminuição de fome
        timer += Time.deltaTime;
        if (timer >= timeBetweenDecreases)
        {
            timer = 0f;
            DecreaseHunger(hungerDecreaseRate);
        }

        UpdateVisualEffects();
    }

    void UpdateVisualEffects()
    {
        if (chromaticAberration == null) return;

        if (Estatico.Fome >= limiarSemEfeitos)
        {
            // Desativa completamente se a fome estiver acima do limiar
            chromaticAberration.intensity.value = 0f;
        }
        else
        {
            // Calcula a intensidade (1 quando fome = 0, 0 quando fome = limiarSemEfeitos)
            float intensidade = Mathf.InverseLerp(limiarSemEfeitos, 0f, Estatico.Fome);
            chromaticAberration.intensity.value = Mathf.Lerp(0f, maxChromaticAberration, intensidade);
        }
    }

    public void EatFood(float amount, float valor1)
    {
        Estatico.Fome += amount;
        Estatico.Fome = Mathf.Clamp(Estatico.Fome, 0f, maxHunger);
        UpdateHungerUI();
    }

    void DecreaseHunger(float amount)
    {
        Estatico.Fome = Mathf.Clamp(Estatico.Fome - amount, 0f, maxHunger);
        UpdateHungerUI();
    }

    void UpdateHungerUI()
    {
        if (hungerBar != null)
        {
            hungerBar.fillAmount = Estatico.Fome / maxHunger;
        }
    }

    public bool TaSemFome()
    {
        return Estatico.Fome <= 0f;
    }
}