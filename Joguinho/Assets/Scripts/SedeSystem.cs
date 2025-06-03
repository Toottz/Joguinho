using UnityEngine;
using UnityEngine.UI;

public class SedeSystem : MonoBehaviour
{
    public Image sedeBar;
    public float maxSede = 100f;
    public float sedeDecreaseRate = 5f;
    public float timeBetweenDecreases = 60f;

    private float currentSede;
    private float timer;

    void Start()
    {
        currentSede = maxSede;
        UpdateUI();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenDecreases)
        {
            timer = 0f;
            ModifySede(-sedeDecreaseRate);
        }
    }
    public void EatFood(float amount)
    {
        currentSede += amount;
        currentSede = Mathf.Clamp(currentSede, 0f, maxSede);
        UpdateUI();
    }

    public void BeberAgua(float valor)
    {
        ModifySede(valor);
    }

    private void ModifySede(float valor)
    {
        currentSede = Mathf.Clamp(currentSede + valor, 0f, maxSede);
        UpdateUI();
    }

   void UpdateUI()
    {
        float fill = currentSede / maxSede;
        sedeBar.fillAmount = fill;
    }

}