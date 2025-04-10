using UnityEngine;
using UnityEngine.UI;

public class InteracaoUIManager : MonoBehaviour
{
    public static InteracaoUIManager Instance;

    public GameObject painelInteracao;
    public Text textoInteracao;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        painelInteracao.SetActive(false);
    }

    public void MostrarTexto(string mensagem)
    {
        textoInteracao.text = mensagem;
        painelInteracao.SetActive(true);
    }

    public void EsconderTexto()
    {
        painelInteracao.SetActive(false);
    }
}
