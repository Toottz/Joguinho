using UnityEngine;
using UnityEngine.UI;

public class AnsiedadeSystem : MonoBehaviour
{
    public Image ansiedadeBar;
    public float maxAnsiedade = 100f;
    public float ansiedadeIncreaseRate = 5f;
    public float timeBetweenIncreases = 60f;

    private float currentAnsiedade;
    private float timer;

    void Start()
    {
        currentAnsiedade = 0f;
        UpdateUI();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenIncreases)
        {
            timer = 0f;
            ModifyAnsiedade(ansiedadeIncreaseRate);
        }
    }

    public void Relaxar(float valor)
    {
        ModifyAnsiedade(-valor);
    }

    private void ModifyAnsiedade(float valor)
    {
        currentAnsiedade = Mathf.Clamp(currentAnsiedade + valor, 0f, maxAnsiedade);
        UpdateUI();
    }

  void UpdateUI()
{
    float fill = currentAnsiedade / maxAnsiedade;
    ansiedadeBar.fillAmount = fill;
    
    
}
}