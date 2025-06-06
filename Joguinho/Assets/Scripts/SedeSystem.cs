using UnityEngine;
using UnityEngine.UI;

public class SedeSystem : MonoBehaviour
{
    public Image sedeBar;
    public float maxSede = 100f;
    public float sedeDecreaseRate = 5f;
    public float timeBetweenDecreases = 60f;

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
            ModifySede(-sedeDecreaseRate);
        }
    }
    public void EatFood(float amount)
    {
        Estatico.Social += amount;
        Estatico.Social = Mathf.Clamp(Estatico.Social, 0f, maxSede);
        UpdateUI();
    }

    public void BeberAgua(float valor)
    {
        ModifySede(valor);
    }

    private void ModifySede(float valor)
    {
        Estatico.Social = Mathf.Clamp(Estatico.Social + valor, 0f, maxSede);
        UpdateUI();
    }

   void UpdateUI()
    {
        float fill = Estatico.Social / maxSede;
        sedeBar.fillAmount = fill;
    }

}