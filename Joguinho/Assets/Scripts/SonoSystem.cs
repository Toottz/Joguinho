using UnityEngine;
using UnityEngine.UI;

public class SonoSystem : MonoBehaviour
{
    public Image sonoBar;
    public float maxSono = 100f;
    public float sonoDecreaseRate = 5f;
    public float timeBetweenDecreases = 90f;

    private float timer;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenDecreases)
        {
            timer = 0f;
            ModifySono(sonoDecreaseRate);
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