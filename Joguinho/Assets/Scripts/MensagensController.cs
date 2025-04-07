using UnityEngine;
using UnityEngine.UI;
using System;

public class MensagensController : MonoBehaviour
{
    public GameObject mensagemPrefab; 
    public Transform content;         
    public GameObject notificacaoIcon;

    private bool celularAberto = false;

    void Start()
    {
        // Exemplo de mensagens
        CriarMensagem("Chefe", "O ultimo cara meio que ferrou legal com o caminhão reserva da empresa. Pra sua sorte, vou diminuir suas entregas para áreas mais perto do prédio e da sua casa, só para fazê-las à pé, então você consegue fazer rápido, vamos precisar dessa força");
    }

    public void CriarMensagem(string remetente, string texto)
    {
        GameObject novaMensagem = Instantiate(mensagemPrefab, content);
        novaMensagem.SetActive(true);

        string hora = DateTime.Now.ToString("HH:mm");
        Text textoUI = novaMensagem.GetComponent<Text>();
        textoUI.text = $"{remetente} ({hora}):\n{texto}";

        if (!celularAberto && notificacaoIcon != null && notificacaoIcon.activeSelf == false)
        {
            notificacaoIcon.SetActive(true);
        }
    }

    public void AoAbrirCelular()
    {
        celularAberto = true;
        if (notificacaoIcon != null)
            notificacaoIcon.SetActive(false);
    }

    public void AoFecharCelular()
    {
        celularAberto = false;
    }
}
