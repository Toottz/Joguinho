using UnityEngine;
using UnityEngine.UI;

public class MercadoUIManager : MonoBehaviour
{
    public GameObject painelPerguntaEntrada; // painel com as opções "Entrar" ou "Não"
    public GameObject painelLoja; // painel principal da loja com os itens
    public GameObject[] itensLoja; // objetos visuais de itens com botão de compra

    void Start()
    {
        if (painelPerguntaEntrada != null)
            painelPerguntaEntrada.SetActive(false);

        if (painelLoja != null)
            painelLoja.SetActive(false);
    }

    // Chamado pelo trigger ao se aproximar
    public void MostrarPerguntaEntrada()
    {
        if (painelPerguntaEntrada != null)
            painelPerguntaEntrada.SetActive(true);
    }

    // Quando o jogador clica em "Entrar"
    public void EntrarNaLoja()
    {
        if (painelPerguntaEntrada != null)
            painelPerguntaEntrada.SetActive(false);

        if (painelLoja != null)
            painelLoja.SetActive(true);
    }

    // Quando o jogador clica em "Não"
    public void CancelarEntrada()
    {
        if (painelPerguntaEntrada != null)
            painelPerguntaEntrada.SetActive(false);
    }

    public void ComprarItem(float preco)
    {
        if (PlayerWallet.Instance != null)
        {
            bool comprou = PlayerWallet.Instance.GastarDinheiro(preco);
            if (comprou)
            {
                Debug.Log("Item comprado por R$ " + preco);
                // Aqui você pode adicionar lógica como remover o item, dar feedback etc
            }
            else
            {
                Debug.Log("Não foi possível comprar, saldo insuficiente.");
            }
        }
    }

    public void FecharLoja()
    {
        if (painelLoja != null)
            painelLoja.SetActive(false);
    }
}
