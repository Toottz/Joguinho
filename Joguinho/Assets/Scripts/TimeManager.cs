using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float minutosPorSegundoReal = 1f;
    public Text[] relogioUI; // Array de Textos para mostrar o horário
    public Light directionalLight;
    public Gradient corLuzPorHora;

    public Material SkyBoxMaterial;
    public Gradient corSkybox1;
    public Gradient corSkybox2;
    public Gradient corNuvens;

    public Material Casa1;
    public Material Casa2;
    public Material Casa3;
    public Material Casa4;
    public Material Casa5;

    public Gradient GradienteCasa1;
    public Gradient GradienteCasa2;
    public Gradient GradienteCasa3;
    public Gradient GradienteCasa4;
    public Gradient GradienteCasa5;

    public bool usarMudancaDeCor = true;
    public GameObject objetoControlador;
    private bool relogioPausado = false;

    public bool relogio;
    public GameObject relogioPonteiro;

    void Update()
    {
        relogioPausado = objetoControlador != null && objetoControlador.activeInHierarchy;

        if (!relogioPausado)
        {
            Estatico.tempoEmMinutos += minutosPorSegundoReal * Time.deltaTime;
            Estatico.Hora = Mathf.FloorToInt(Estatico.tempoEmMinutos / 60f);
            Estatico.Minutos = Mathf.FloorToInt(Estatico.tempoEmMinutos % 60f);
            if (Estatico.tempoEmMinutos >= 1440f)
                Estatico.tempoEmMinutos = 0f;
        }

        // Atualiza todos os textos do relógio
        AtualizarRelogiosUI();
        AtualizarIluminacao();
        AtualizarMomentoDoDia();
    }

    void AtualizarRelogiosUI()
    {
        string horarioFormatado = Estatico.Hora.ToString("00") + ":" + Estatico.Minutos.ToString("00");

        foreach (Text textoRelogio in relogioUI)
        {
            if (textoRelogio != null)
            {
                textoRelogio.text = horarioFormatado;
            }
        }
    }

    void AtualizarMomentoDoDia()
    {
        float hora = Estatico.Hora;
        Mensagem.MomentoDia momento;

        if (hora >= 5f && hora < 12f)
            momento = Mensagem.MomentoDia.Manha;
        else if (hora >= 12f && hora < 18f)
            momento = Mensagem.MomentoDia.Tarde;
        else
            momento = Mensagem.MomentoDia.Noite;

        MensagensAppManager mensagens = FindObjectOfType<MensagensAppManager>();
        if (mensagens != null)
        {
            mensagens.AtualizarMomentoDoDia(momento);
        }
    }

    void AtualizarIluminacao()
    {
        if (!usarMudancaDeCor)
            return;

        float t = Estatico.tempoEmMinutos / 1440f;

        if (directionalLight != null)
        {
            directionalLight.color = corLuzPorHora.Evaluate(t);
            directionalLight.transform.rotation = Quaternion.Euler((t * 360f) - 90f, 105.104f, 0);
        }

        if (relogio)
        {
            relogioPonteiro.transform.rotation = Quaternion.Euler(0f, 0f, (t * -360f) - -90f);
        }

        if (SkyBoxMaterial != null)
        {
            SkyBoxMaterial.SetColor("_Cor_Horizonte", corSkybox1.Evaluate(t));
            SkyBoxMaterial.SetColor("_Cor_ceu", corSkybox2.Evaluate(t));
            SkyBoxMaterial.SetColor("_Sky_Noise_Color", corNuvens.Evaluate(t));
            Casa1.SetColor("_EmissionColor", GradienteCasa1.Evaluate(t));
            Casa2.SetColor("_EmissionColor", GradienteCasa2.Evaluate(t));
            Casa3.SetColor("_EmissionColor", GradienteCasa3.Evaluate(t));
            Casa4.SetColor("_EmissionColor", GradienteCasa4.Evaluate(t));
            Casa5.SetColor("_EmissionColor", GradienteCasa5.Evaluate(t));
        }
    }
}
