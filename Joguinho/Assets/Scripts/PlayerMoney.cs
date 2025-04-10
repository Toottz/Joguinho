using UnityEngine;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    public static PlayerMoney instance;

    public int dinheiroAtual = 0;
    public Text textoSaldo;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        AtualizarUI();
    }

    public void AdicionarDinheiro(int quantia)
    {
        dinheiroAtual += quantia;
        AtualizarUI();
    }

    public bool TentarGastarDinheiro(int quantia)
    {
        if (dinheiroAtual >= quantia)
        {
            dinheiroAtual -= quantia;
            AtualizarUI();
            return true;
        }
        return false;
    }

    void AtualizarUI()
    {
        if (textoSaldo != null)
            textoSaldo.text = "R$ " + dinheiroAtual;
    }
}
