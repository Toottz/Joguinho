using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float minutosPorSegundoReal = 1f;
    public Text relogioUI; // opcional, mostra a hora na tela
    public Light directionalLight; // luz do sol
    public Gradient corLuzPorHora; // curva de cor ao longo do dia

    public bool usarMudancaDeCor = true;
    public static float HoraAtual { get; private set; }


    private float tempoEmMinutos = 360f; // começa às 6:00 da manhã

    void Update()
    {

        tempoEmMinutos += minutosPorSegundoReal * Time.deltaTime;

        HoraAtual = tempoEmMinutos / 60f;

        if (tempoEmMinutos >= 1440f)
            tempoEmMinutos = 0f; // reinicia ao chegar em 24h

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
            directionalLight.transform.rotation = Quaternion.Euler(new Vector3((t * 360f) - 90f, 170f, 0));
        }
    }

}
