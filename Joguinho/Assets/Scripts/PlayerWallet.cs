using UnityEngine;
using UnityEngine.UI;

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance;

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
        Estatico.Dinheiro += valor;
        AtualizarUI();
    }

    public bool GastarDinheiro(float valor)
    {
        if (Estatico.Dinheiro >= valor)
        {
            Estatico.Dinheiro -= valor;
            AtualizarUI();
            return true;
        }

        Debug.Log("Saldo insuficiente!");
        return false;
    }

    void AtualizarUI()
    {
        if (textoSaldo != null)
            textoSaldo.text = "R$ " + Estatico.Dinheiro.ToString("F2");
    }

    public void Resetar()
    {
        Estatico.Dinheiro = 10f;
        AtualizarUI();
    }

}
