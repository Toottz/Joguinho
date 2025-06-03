using UnityEngine;

public class ConversaSalva : MonoBehaviour
{
    public string idConversa = "conversa_mae_manha"; // Um identificador único para essa conversa
    public GameObject grupoDeOpcoes;
    public GameObject respostaJogadora;
    public GameObject respostaPessoa;

    void Start()
    {
        if (PlayerPrefs.GetInt(idConversa, 0) == 1)
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
        PlayerPrefs.SetInt(idConversa, 1);
        PlayerPrefs.Save();

        if (grupoDeOpcoes != null)
            grupoDeOpcoes.SetActive(false);

        if (respostaJogadora != null)
            respostaJogadora.SetActive(true);

        if (respostaPessoa != null)
            respostaPessoa.SetActive(true);
    }
}
