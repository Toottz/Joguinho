using UnityEngine;
using UnityEngine.UI;

public class SonoSystem : MonoBehaviour
{
    public Image sonoBar;
    public float maxSono = 100f;
    public float sonoDecreaseRate = 5f;
    public float timeBetweenDecreases = 0.001f;

    private float timer =0f;

    public float reducaoTotalDoSono = 10f; // Total a ser reduzido em 2h
    private float reducaoAcumulada = 0f;
    private bool periodoAtivo = false;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Estatico.tempoEmMinutos >= 480 && !periodoAtivo)
        {
            periodoAtivo = true;
            reducaoAcumulada = 0f;
        }

        // Durante o período de 2h, reduz gradualmente
        if (periodoAtivo && reducaoAcumulada < reducaoTotalDoSono)
        {
            float reducaoFrame = (reducaoTotalDoSono / 480f) * Time.deltaTime * 60f;
            Estatico.sono -= reducaoFrame;
            reducaoAcumulada += reducaoFrame;

            // Garante que não reduza mais que o total
            if (reducaoAcumulada >= reducaoTotalDoSono)
            {
                Estatico.sono += (reducaoAcumulada - reducaoTotalDoSono); // Ajuste fino
                periodoAtivo = false;
            }
        }

        else {
        timer += Time.deltaTime;
        if (timer >= timeBetweenDecreases)
        {
            ModifySono(sonoDecreaseRate);
        }
       }
    }

    public void Dormir(float valor)
    {
        ModifySono(-valor);
    }

    private void ModifySono(float valor)
    {
        Estatico.Sono = Mathf.Clamp(Estatico.Sono + valor, 0f, maxSono);
        UpdateUI();
    }

    void UpdateUI()
    {
        float fill = Estatico.Sono / maxSono;
        sonoBar.fillAmount = fill;
    }
}