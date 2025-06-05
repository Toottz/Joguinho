using UnityEngine;

public class ConversaSalva : MonoBehaviour
{
    public string idConversa = "conversa_mae_manha";
    public GameObject grupoDeOpcoes;
    public GameObject respostaJogadora;
    public GameObject respostaPessoa;
    void Start()
    {
        if (ConversaCache.FoiSalva(idConversa))
        {
            if (grupoDeOpcoes != null)
                grupoDeOpcoes.SetActive(false);

            if (respostaJogadora != null)
                respostaJogadora.SetActive(true);

            if (respostaPessoa != null)
                respostaPessoa.SetActive(true);
        }
    }

    public void SalvarResposta()
    {
        ConversaCache.Salvar(idConversa);

        if (grupoDeOpcoes != null)
            grupoDeOpcoes.SetActive(false);

        if (respostaJogadora != null)
            respostaJogadora.SetActive(true);

        if (respostaPessoa != null)
            respostaPessoa.SetActive(true);
    }
}