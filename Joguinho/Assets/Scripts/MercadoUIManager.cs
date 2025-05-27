using UnityEngine;
using UnityEngine.UI;

public class MercadoUIManager : MonoBehaviour
{
public GameObject painelPerguntaInicial; // "Comprar comidas? [Sim] [Não]"
public GameObject painelConfirmacao; // "Tem certeza? [Sim] [Não]"
public GameObject painelPrincipalLoja; // Painel de fundo da loja (se quiser mostrar algo)
public float precoComida = 10f;

public Text textoPrecoConfirmacao;         // "Tem certeza? Isso custará X reais"
public AudioSource somCompra;

private void Start()
{
    FecharTudo();
}

public void MostrarPergunta()
{
    FecharTudo();
    painelPerguntaInicial.SetActive(true);
}

public void BotaoSimPrimeiraPergunta()
{
    painelPerguntaInicial.SetActive(false);
    textoPrecoConfirmacao.text = $"Tem certeza? Isso custará R$ {precoComida:F2}";
    painelConfirmacao.SetActive(true);
}

public void BotaoNaoPrimeiraPergunta()
{
    SairDaLoja();
}

public void BotaoSimConfirmacao()
{
    if (PlayerWallet.Instance != null && PlayerWallet.Instance.GastarDinheiro(precoComida))
    {
        Debug.Log("🛒 Compra realizada!");
        if (somCompra != null) somCompra.Play();
    }
    else
    {
        Debug.Log("❌ Não foi possível comprar (dinheiro insuficiente)");
    }

    SairDaLoja();
}

public void BotaoNaoConfirmacao()
{
    SairDaLoja();
}

void SairDaLoja()
{
    FecharTudo();
    if (painelPrincipalLoja != null)
        painelPrincipalLoja.SetActive(false);
}

void FecharTudo()
{
    painelPerguntaInicial?.SetActive(false);
    painelConfirmacao?.SetActive(false);
}
}