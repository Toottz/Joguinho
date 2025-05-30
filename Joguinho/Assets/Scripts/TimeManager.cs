using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float minutosPorSegundoReal = 1f;
    public Text relogioUI; // opcional, mostra a hora na tela
    public Light directionalLight; // luz do sol
    public Gradient corLuzPorHora; // curva de cor ao longo do dia

    public Material SkyBoxMaterial;
    public Gradient corSkybox1;
    public Gradient corSkybox2;
    public Gradient corNuvens;

    public Material Casa1;
    public Material Casa2;
    public Material Casa3; 
    public Material Casa4;
    public Material Casa5;
    public Material Casa6;

    public Gradient GradienteCasa1;
    public Gradient GradienteCasa2;
    public Gradient GradienteCasa3;
    public Gradient GradienteCasa4;
    public Gradient GradienteCasa5;

    public bool usarMudancaDeCor = true;

    [SerializeField]
    public static float HoraAtual { get; private set; }
    [SerializeField]
    public static float MinutoAtual { get; private set; }


    private float tempoEmMinutos = 360f; // começa às 6:00 da manhã

    public GameObject objetoControlador; // Arraste o objeto que controlará o relógio no Inspector
    private bool relogioPausado = false;

    void Update()
    {
        // Verifica se o objeto controlador está ativo
        relogioPausado = objetoControlador != null && objetoControlador.activeInHierarchy;

        // Se o relógio não estiver pausado, atualiza o tempo
        if (!relogioPausado)
        {
            tempoEmMinutos += minutosPorSegundoReal * Time.deltaTime;
            HoraAtual = tempoEmMinutos / 60f;
            MinutoAtual = tempoEmMinutos % 60f;

            if (tempoEmMinutos >= 1440f)
                tempoEmMinutos = 0f; // reinicia ao chegar em 24h
        }

        AtualizarRelogio();
        AtualizarIluminacao();
    }

    void AtualizarRelogio()
    {
        int horas = Mathf.FloorToInt(tempoEmMinutos / 60f);
        int minutos = Mathf.FloorToInt(tempoEmMinutos % 60f);

        if (relogioUI != null)
            relogioUI.text = horas.ToString("00") + ":" + minutos.ToString("00");
    }

    void AtualizarIluminacao()
    {
        if (!usarMudancaDeCor)
            return;

        float t = tempoEmMinutos / 1440f;

        if (directionalLight != null && corLuzPorHora != null)
        {
            directionalLight.color = corLuzPorHora.Evaluate(t);
            directionalLight.transform.rotation = Quaternion.Euler(new Vector3((t * 360f) - 90f, 105.104f, 0));
        }
        if (SkyBoxMaterial != null && corLuzPorHora != null)
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
