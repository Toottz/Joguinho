using UnityEngine;
using UnityEngine.UI;

public class HungerSystem : MonoBehaviour
{
    public Image hungerBar; // imagem circular da UI
    public float maxHunger = 100f;
    public float hungerDecreaseRate = 5f; // quanto vai descer por ciclo
    public float timeBetweenDecreases = 60f; // tempo em segundos

    private float timer;

    void Start()
    {
        UpdateHungerUI();
    }

    void Update()
    {
        // Contador de tempo entre quedas
        timer += Time.deltaTime;

        if (timer >= timeBetweenDecreases)
        {
            timer = 0f;
            DecreaseHunger(hungerDecreaseRate);
        }
    }

    public void EatFood(float amount)
    {
        Estatico.Fome += amount;
        Estatico.Fome = Mathf.Clamp(Estatico.Fome, 0f, maxHunger);
        UpdateHungerUI();
    }

    void DecreaseHunger(float amount)
    {
        Estatico.Fome -= amount;
        Estatico.Fome = Mathf.Clamp(Estatico.Fome, 0f, maxHunger);
        UpdateHungerUI();
    }

     void UpdateHungerUI()
    {
        float fillAmount = Estatico.Fome / maxHunger;
        hungerBar.fillAmount = fillAmount;
    }

    public bool TaSemFome()
    {
        if (Estatico.Fome == maxHunger) return true;   
        else return false;
    }
}