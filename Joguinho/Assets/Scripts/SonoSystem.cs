using UnityEngine;
using UnityEngine.UI;

public class SonoSystem : MonoBehaviour
{
    [Header("Configurações UI")]
    public Image sonoBar;

    [Header("Parâmetros de Sono")]
    public float maxSono = 100f;
    public float sonoAoAcordar = 20f;
    public float tempoParaComecarRecuperar = 480f; // 8h em minutos

    [Header("Taxas de Mudança")]
    public float taxaReducaoBase = 0.1f; // Taxa de redução antes de começar a recuperar
    public float taxaRecuperacao = 0.2f; // Taxa de aumento após 8h

    private bool comecouRecuperar = false;

    void Start()
    {
        // Inicializa o sono se for a primeira vez
        if (Estatico.Sono <= 0)
        {
            Estatico.Sono = sonoAoAcordar;
        }
        UpdateUI();
    }

    void Update()
    {
        // Verifica se já passou o tempo para começar a recuperar
        if (!comecouRecuperar && Estatico.tempoEmMinutos >= tempoParaComecarRecuperar)
        {
            comecouRecuperar = true;
        }

        if (comecouRecuperar)
        {
            // Fase de RECUPERAÇÃO do sono (aumenta)
            Estatico.Sono = Mathf.Min(maxSono, Estatico.Sono + taxaRecuperacao * Time.deltaTime);
        }
        else
        {
            // Fase de PERDA do sono (diminui)
            Estatico.Sono = Mathf.Max(0, Estatico.Sono - taxaReducaoBase * Time.deltaTime);
        }

        UpdateUI();
    }

    public void Dormir(float valor)
    {
        Estatico.Sono = Mathf.Max(0, Estatico.Sono - valor);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (sonoBar != null)
        {
            sonoBar.fillAmount = Estatico.Sono / maxSono;
        }
    }

    // Método para resetar o ciclo (chamar quando o personagem dormir)
    public void ResetarCicloSono()
    {
        comecouRecuperar = false;
        Estatico.Sono = sonoAoAcordar;
    }
}