using UnityEngine;
using UnityEngine.UI;

public class AppConfiguracoes : MonoBehaviour
{
    public Slider sliderBrilhoCelular;
    public Slider sliderBrilhoJogo;
    public Slider sliderVolume;

    public Image escurecerCelular;     // Imagem preta DENTRO do celular (escurece a tela do app)
    public Light luzPrincipal;         // Luz principal da cena (se usada)
    public Image overlayEscurecer;     // Imagem preta para escurecer o JOGO
    public Image overlayClarear;       // Imagem branca para clarear o JOGO

    void Start()
    {
        sliderBrilhoCelular.onValueChanged.AddListener(AjustarBrilhoCelular);
        sliderBrilhoJogo.onValueChanged.AddListener(AjustarBrilhoJogo);
        sliderVolume.onValueChanged.AddListener(AjustarVolume);

        // Valores padrão ao iniciar
        sliderBrilhoCelular.value = 1f;
        sliderBrilhoJogo.value = luzPrincipal != null ? luzPrincipal.intensity : 1f;
        sliderVolume.value = AudioListener.volume;

        // Começa com brilho do celular total (sem escurecimento)
        if (escurecerCelular != null)
        {
            var cor = escurecerCelular.color;
            cor.a = 0f;
            escurecerCelular.color = cor;
        }
    }

    void AjustarBrilhoCelular(float valor)
    {
        // 1 = sem escurecimento | 0 = escuro total
        if (escurecerCelular != null)
        {
            float alpha = 0.7f - Mathf.Clamp01(valor);
            var cor = escurecerCelular.color;
            cor.a = alpha;
            escurecerCelular.color = cor;
        }
    }

    void AjustarBrilhoJogo(float valor)
    {
        float brilhoNormal = 1f;

        // Limites máximos dos efeitos visuais
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
