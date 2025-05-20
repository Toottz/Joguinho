using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LojaUIManager : MonoBehaviour
{
public GameObject telaLojaUI;
public TextMeshProUGUI textoDinheiro;
void Start()
{
    if (telaLojaUI != null)
        telaLojaUI.SetActive(false);
}

public void AbrirLoja()
{
    telaLojaUI.SetActive(true);
    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;
    AtualizarTextoDinheiro();
}

public void FecharLoja()
{
    telaLojaUI.SetActive(false);
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

public void ComprarItem(float preco)
{
    if (PlayerWallet.Instance != null)
    {
        bool comprado = PlayerWallet.Instance.GastarDinheiro(preco);
        if (comprado)
        {
            Debug.Log("🛒 Produto comprado por R$" + preco);
            AtualizarTextoDinheiro();
        }
        else
        {
            Debug.Log("❌ Dinheiro insuficiente.");
        }
    }
}

void AtualizarTextoDinheiro()
{
    if (textoDinheiro != null && PlayerWallet.Instance != null)
    {
        textoDinheiro.text = "Dinheiro: R$ " + PlayerWallet.Instance.saldo.ToString("F2");
    }
}
}