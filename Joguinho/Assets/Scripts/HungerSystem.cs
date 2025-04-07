using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HungerSystem : MonoBehaviour
{
    public Image hungerBar; // Referência ao círculo de fome
    public Color fullHungerColor = Color.green; // Cor cheia
    public Color emptyHungerColor = new Color(0, 0, 0, 0); // Sem cor (transparente)
    
    public float maxHunger = 100f;
    public float hungerDecreaseRate = 5f;
    public float timeBetweenDecreases = 60f;

    private float currentHunger;

    void Start()
    {
        currentHunger = maxHunger;
        UpdateHungerUI();
        StartCoroutine(DecreaseHungerOverTime());
    }

    IEnumerator DecreaseHungerOverTime()
    {
        while (currentHunger > 0)
        {
            yield return new WaitForSeconds(timeBetweenDecreases);
            currentHunger -= hungerDecreaseRate;
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
            UpdateHungerUI();
        }
    }

    public void EatFood(float foodValue)
    {
        currentHunger += foodValue;
        currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
        UpdateHungerUI();
    }

    void UpdateHungerUI()
    {
        float fillAmount = currentHunger / maxHunger;
        hungerBar.fillAmount = fillAmount;

        // Interpolação de cor entre fome cheia e vazia
        hungerBar.color = Color.Lerp(emptyHungerColor, fullHungerColor, fillAmount);
    }
}