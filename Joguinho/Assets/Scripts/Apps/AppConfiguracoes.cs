using UnityEngine;
using UnityEngine.UI;

public class AppConfiguracoes : MonoBehaviour
{
    public Slider sliderBrilhoCelular;
    public Slider sliderBrilhoJogo;
    public Slider sliderVolume;

    public Image telaCelular; // A imagem da tela do celular (para simular brilho)
    public Light luzPrincipal; // A luz da cena
    public Image overlayEscurecer; // preto
    public Image overlayClarear;   // branco



    void Start()
    {
        sliderBrilhoCelular.onValueChanged.AddListener(AjustarBrilhoCelular);
        sliderBrilhoJogo.onValueChanged.AddListener(AjustarBrilhoJogo);
        sliderVolume.onValueChanged.AddListener(AjustarVolume);

        // Valores padrão
        sliderBrilhoCelular.value = 1f;
        sliderBrilhoJogo.value = luzPrincipal != null ? luzPrincipal.intensity : 1f;
        sliderVolume.value = AudioListener.volume;
    }

    void AjustarBrilhoCelular(float valor)
    {
        if (telaCelular != null)
        {
            Color cor = telaCelular.color;
            cor.a = Mathf.Clamp01(valor);
            telaCelular.color = cor;
        }
    }

    void AjustarBrilhoJogo(float valor)
{
    float brilhoNormal = 1f;

    // LIMITES personalizados
    float maxEscurecer = 0.5f; // até 50% de opacidade preta
    float maxClarear = 0.3f;   // até 30% de opacidade branca

    if (valor < brilhoNormal)
    {
        float escurecer = (1f - valor) * maxEscurecer;

        if (overlayEscurecer != null)
        {
            var c = overlayEscurecer.color;
            c.a = escurecer;
            overlayEscurecer.color = c;
        }

        if (overlayClarear != null)
        {
            var c = overlayClarear.color;
            c.a = 0f;
            overlayClarear.color = c;
        }
    }
    else if (valor > brilhoNormal)
    {
        float clarear = (valor - 1f) * maxClarear;

        if (overlayClarear != null)
        {
            var c = overlayClarear.color;
            c.a = clarear;
            overlayClarear.color = c;
        }

        if (overlayEscurecer != null)
        {
            var c = overlayEscurecer.color;
            c.a = 0f;
            overlayEscurecer.color = c;
        }
    }
    else
    {
        if (overlayClarear != null)
        {
            var c = overlayClarear.color;
            c.a = 0f;
            overlayClarear.color = c;
        }
        if (overlayEscurecer != null)
        {
            var c = overlayEscurecer.color;
            c.a = 0f;
            overlayEscurecer.color = c;
        }
    }
}

    void AjustarVolume(float valor)
    {
        AudioListener.volume = valor;
    }
}
