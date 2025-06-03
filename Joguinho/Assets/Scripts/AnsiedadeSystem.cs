using UnityEngine;
using UnityEngine.UI;

public class AnsiedadeSystem : MonoBehaviour
{
    public Image ansiedadeBar;
    public float maxAnsiedade = 100f;
    public float ansiedadeIncreaseRate = 5f;
    public float timeBetweenIncreases = 60f;

    private float timer;

    void Start()
    {
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
        Estatico.Ansiedade = Mathf.Clamp(Estatico.Ansiedade + valor, 0f, maxAnsiedade);
        UpdateUI();
    }

    void UpdateUI()
    {
        float fill = Estatico.Ansiedade / maxAnsiedade;
        ansiedadeBar.fillAmount = fill;

    }
}
