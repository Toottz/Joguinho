/*using UnityEngine;
using TMPro;

public class LojaUIManager : MonoBehaviour
{
    public GameObject painelOpcoes;
    public GameObject painelConfirmar;
    public TextMeshProUGUI textoValor;

    public float precoDoItem = 10f;
    public int idItemComprado = 1; // ID do item no InventarioSimples

    public GameObject painelLoja;

    void Start()
    {
        if (painelOpcoes != null)
            painelOpcoes.SetActive(false);

        if (painelConfirmar != null)
            painelConfirmar.SetActive(false);

        if (painelLoja != null)
            painelLoja.SetActive(false);
    }

    public void MostrarOpcoesLoja()
    {
        if (painelOpcoes != null)
            painelOpcoes.SetActive(true);
    }

    public void OcultarOpcoesLoja()
    {
        if (painelOpcoes != null)
            painelOpcoes.SetActive(false);
    }

    public void EntrarNaLoja()
    {
        OcultarOpcoesLoja();

        if (painelLoja != null)
            painelLoja.SetActive(true);
    }

    public void CancelarEntrada()
    {
        OcultarOpcoesLoja();
    }

    public void ComprarComida()
    {
        if (painelConfirmar != null && textoValor != null)
        {
            textoValor.text = $"Tem certeza? Isso custará R$ {precoDoItem:F2}";
            painelConfirmar.SetActive(true);
        }
    }

    public void ConfirmarCompra()
    {
        if (PlayerWallet.Instance != null && InventarioSimples.Instance != null)
        {
            if (PlayerWallet.Instance.GastarDinheiro(precoDoItem))
            {
                InventarioSimples.Instance.AdicionarItem(idItemComprado);
                Debug.Log("✅ Compra realizada e item adicionado ao inventário!");
            }
            else
            {
                Debug.Log("❌ Dinheiro insuficiente!");
            }
        }

        SairDaLoja();
    }

    public void CancelarCompra()
    {
        SairDaLoja();
    }

    public void SairDaLoja()
    {
        if (painelLoja != null)
            painelLoja.SetActive(false);

        if (painelConfirmar != null)
            painelConfirmar.SetActive(false);
    }
}
*/