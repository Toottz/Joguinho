using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Indoprosfinais : MonoBehaviour
{
    [Header("Configurações de Tempo")]
    public float hora;
    public float minuto;

    [Header("Configurações de Ansiedade")]
    public float limiteAnsiedade = 70f;
    public string cenaAnsiedadeAlta = "Cena_Stress";
    public string cenaAnsiedadeBaixa = "Cena_Normal";

    [Header("Efeitos de Transição")]
    public Image fade;
    public TextMeshProUGUI texto;
    public float velocidadefade = 0.2f;

    private bool escureca = false;
    private float alpha = 0f;

    void Update()
    {
        float currentHour = Estatico.Hora;
        float currentMinuto = Estatico.Minutos;

        // Verifica se atingiu o horário específico
        if (Mathf.FloorToInt(currentMinuto) == Mathf.FloorToInt(minuto) &&
            Mathf.FloorToInt(currentHour) == Mathf.FloorToInt(hora))
        {
            escureca = true;

        }

        // Executa o fade se necessário
        if (escureca)
        {
            alpha += Time.deltaTime * velocidadefade;
            fade.color = new Color(0, 0, 0, alpha);

            // Também aplica o fade no texto
            texto.color = new Color(texto.color.r, texto.color.g, texto.color.b, alpha);

            if (alpha >= 1f)
            {
                // Decide qual cena carregar baseado na ansiedade
                string cenaDestino = Estatico.Ansiedade >= limiteAnsiedade ?
                    cenaAnsiedadeAlta :
                    cenaAnsiedadeBaixa;

                SceneManager.LoadScene(cenaDestino);
            }
        }
    }
}