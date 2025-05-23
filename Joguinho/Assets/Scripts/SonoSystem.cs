using UnityEngine;
using UnityEngine.UI;

public class SonoSystem : MonoBehaviour
{
    public Image sonoBar;
    public float maxSono = 100f;
    public float sonoDecreaseRate = 5f;
    public float timeBetweenDecreases = 90f;

    private float currentSono;
    private float timer;

    void Start()
    {
        currentSono = 0f;
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
        currentSono = Mathf.Clamp(currentSono + valor, 0f, maxSono);
        UpdateUI();
    }

    void UpdateUI()
    {
        float fill = currentSono / maxSono;
        sonoBar.fillAmount = fill;
    }
}