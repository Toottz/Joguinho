using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MensagensAppManager : MonoBehaviour
{
    public ContatoConversa conversaAtual;
    public GameObject painelMensagem;
    public TextMeshProUGUI textoMensagem;
    public Button[] botoesRespostas;
    public TextMeshProUGUI[] textosBotoes;
    private Mensagem.MomentoDia momentoAtual;

    void Start()
    {
        painelMensagem.SetActive(false);
        // O momento será atualizado pelo TimeManager
    }

    public void AtualizarMomentoDoDia(Mensagem.MomentoDia novoMomento)
    {
        momentoAtual = novoMomento;
    }

    public void AbrirConversa(ContatoConversa contato)
    {
        conversaAtual = contato;
        MostrarMensagemDoMomento();
    }

    void MostrarMensagemDoMomento()
    {
        foreach (var mensagem in conversaAtual.mensagens)
        {
            if (mensagem.momento == momentoAtual && !mensagem.jaFoiRespondida)
            {
                painelMensagem.SetActive(true);
                textoMensagem.text = mensagem.textoPergunta;

                for (int i = 0; i < botoesRespostas.Length; i++)
                {
                    int index = i;
                    textosBotoes[i].text = mensagem.opcoesResposta[i];
                    botoesRespostas[i].onClick.RemoveAllListeners();
                    botoesRespostas[i].onClick.AddListener(() => Responder(index, mensagem));
                }

                return;
            }
        }

        painelMensagem.SetActive(false);
    }

    void Responder(int respostaIndex, Mensagem mensagem)
    {
        textoMensagem.text = mensagem.respostasPersonagem[respostaIndex];
        mensagem.jaFoiRespondida = true;

        foreach (var btn in botoesRespostas)
            btn.gameObject.SetActive(false);
    }
}
