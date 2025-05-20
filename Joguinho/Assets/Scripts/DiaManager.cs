using UnityEngine;

public class DiaManager : MonoBehaviour
{
    public static DiaManager Instance;

    [Header("Dia Atual (começa no 1)")]
    public int diaAtual = 1;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Mantém entre cenas, se necessário
    }

    public void AvancarDia()
    {
        diaAtual++;
        Debug.Log("📆 Novo dia: " + diaAtual);
    }

    public int ObterDiaAtual()
    {
        return diaAtual;
    }
}
