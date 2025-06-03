using UnityEngine;

public class EscolherRespostaConversa : MonoBehaviour
{
    public GameObject grupoDeOpcoes;      // O grupo com os dois botões de escolha
    public GameObject respostaJogadora;   // O balão da jogadora (só aparece depois da escolha)
    public GameObject respostaPessoa;     // O balão da outra pessoa (resposta ao que você escolheu)

    public void Escolher()
    {
        if (grupoDeOpcoes != null)
            grupoDeOpcoes.SetActive(false);

        if (respostaJogadora != null)
            respostaJogadora.SetActive(true);

        if (respostaPessoa != null)
            respostaPessoa.SetActive(true);
    }
}
