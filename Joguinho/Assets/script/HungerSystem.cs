using UnityEngine;
using UnityEngine.UI;

public class HungerSystem : MonoBehaviour
{
    public Image hungerBar; // imagem circular da UI
    public float maxHunger = 100f;
    public float hungerDecreaseRate = 5f; // quanto vai descer por ciclo
    public float timeBetweenDecreases = 60f; // tempo em segundos

    private float currentHunger;
    private float timer;

    void Start()
    {
        currentHunger = maxHunger;
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
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
        UpdateHungerUI();
    }

    void DecreaseHunger(float amount)
    {
        currentHunger -= amount;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
        UpdateHungerUI();
    }

     void UpdateHungerUI()
    {
        float fillAmount = currentHunger / maxHunger;
        hungerBar.fillAmount = fillAmount;

        // Cor sólida (por exemplo, verde)
        hungerBar.color = Color.green;
    }
}