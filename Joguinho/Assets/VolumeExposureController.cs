using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class VolumeExposureController : MonoBehaviour
{
    [Header("Referências")]
    public Slider slider;

    [Header("Configurações")]
    public float minExposure = -1f;
    public float maxExposure = 1f;

    private ColorAdjustments colorAdjustments;
    private float defaultExposure = 0f; // Valor inicial neutro

    void Awake()
    {
        // 1. Encontra o Global Volume e configura exposição padrão
        if (!InitializeVolume()) return;

        // 2. Configura o slider sem aplicar mudanças imediatas
        if (slider != null)
        {
            slider.minValue = 0f;
            slider.maxValue = 1f;
            // Converte a exposição atual (ex: -1 a 1) para o valor do slider (0 a 1)
            float normalizedValue = Mathf.InverseLerp(minExposure, maxExposure, colorAdjustments.postExposure.value);
            slider.value = normalizedValue;

            slider.onValueChanged.AddListener(OnSliderChanged);
        }
    }

    bool InitializeVolume()
    {
        var globalVolume = FindObjectOfType<Volume>();
        if (globalVolume == null || !globalVolume.profile.TryGet(out colorAdjustments))
        {
            Debug.LogError("Global Volume não configurado!");
            return false;
        }

        // Garante que a exposição comece neutra
        colorAdjustments.postExposure.overrideState = true;
        colorAdjustments.postExposure.value = defaultExposure;
        return true;
    }

    public void OnSliderChanged(float sliderValue)
    {
        if (colorAdjustments == null) return;

        // Converte o valor do slider (0-1) para exposição (-1 a 1)
        float targetExposure = Mathf.Lerp(minExposure, maxExposure, sliderValue);
        colorAdjustments.postExposure.value = targetExposure;
    }

    void OnDestroy()
    {
        if (slider != null)
            slider.onValueChanged.RemoveListener(OnSliderChanged);
    }
}