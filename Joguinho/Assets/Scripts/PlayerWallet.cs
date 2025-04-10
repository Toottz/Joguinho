using UnityEngine;
using UnityEngine.UI;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance;

    public float saldo = 0f;
    public Text textoSaldo;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        AtualizarUI();
    }

    public void AdicionarDinheiro(float valor)
    {
        saldo += valor;
        AtualizarUI();
    }

    public bool GastarDinheiro(float valor)
    {
        if (saldo >= valor)
        {
            saldo -= valor;
            AtualizarUI();
            return true;
        }

        Debug.Log("Saldo insuficiente!");
        return false;
    }

    void AtualizarUI()
    {
        if (textoSaldo != null)
            textoSaldo.text = "R$ " + saldo.ToString("F2");
    }
}
