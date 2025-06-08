using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AnsiedadeSystem : MonoBehaviour
{
    [Header("Configurações UI")]
    public Image ansiedadeBar;

    [Header("Parâmetros de Ansiedade")]
    public float maxAnsiedade = 100f;
    public float ansiedadeIncreaseRate = 5f;
    public float timeBetweenIncreases = 60f;
    public float limiarEfeitos = 60f; // Ansiedade mínima para ativar efeitos

    [Header("Efeitos Visuais")]
    [Range(0, 1)] public float maxFilmGrain = 0.8f;
    [Range(0, 0.6f)] public float maxLensDistortion = 0.3f;

    private float timer;
    private VolumeProfile volumeProfile;
    private FilmGrain filmGrain;
    private LensDistortion lensDistortion;

    void Start()
    {
        SetupPostProcessing();
        UpdateUI();
    }

    void SetupPostProcessing()
    {
        Volume globalVolume = FindObjectOfType<Volume>();
        if (globalVolume != null)
        {
            volumeProfile = globalVolume.profile;

            if (!volumeProfile.TryGet(out filmGrain))
                Debug.LogWarning("Film Grain não encontrado!");

            if (!volumeProfile.TryGet(out lensDistortion))
                Debug.LogWarning("Lens Distortion não encontrado!");
        }
    }

    void Update()
    {
        // Sistema original de ansiedade
        timer += Time.deltaTime;
        if (timer >= timeBetweenIncreases)
        {
            timer = 0f;
            ModifyAnsiedade(ansiedadeIncreaseRate);
        }

        // Controle dos efeitos visuais
        UpdateVisualEffects();
    }

    void UpdateVisualEffects()
    {
        if (Estatico.Ansiedade <= limiarEfeitos)
        {
            // Reset dos efeitos se ansiedade baixa
            if (filmGrain != null) filmGrain.intensity.value = 0f;
            if (lensDistortion != null) lensDistortion.intensity.value = 0f;
            return;
        }

        // Calcula a intensidade normalizada (0 a 1) baseada na ansiedade acima do limiar
        float intensidadeNormalizada = Mathf.InverseLerp(limiarEfeitos, maxAnsiedade, Estatico.Ansiedade);

        // Aplica gradualmente os efeitos
        if (filmGrain != null)
        {
            filmGrain.active = true;
            filmGrain.intensity.value = Mathf.Lerp(0f, maxFilmGrain, intensidadeNormalizada);
        }

        if (lensDistortion != null)
        {
            lensDistortion.active = true;
            lensDistortion.intensity.value = Mathf.Lerp(0f, maxLensDistortion, intensidadeNormalizada);
        }
    }

    public void Relaxar(float valor)
    {
        ModifyAnsiedade(-valor);
    }

    private void ModifyAnsiedade(float valor)
    {
        Estatico.Ansiedade = Mathf.Clamp(Estatico.Ansiedade + valor, 0f, maxAnsiedade);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (ansiedadeBar != null)
        {
            ansiedadeBar.fillAmount = Estatico.Ansiedade / maxAnsiedade;
        }
    }
}