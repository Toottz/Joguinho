using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortaCena : MonoBehaviour
{
    public string nomeCenaDestino = "NomeDaCena";
    public string mensagemInteracao = "Pressione 'E' para entrar";
    [SerializeField]
    private bool tarefas_Finalizadas = false;

    private bool playerPerto = false;

    public GameObject textoUI;
    public TextMeshProUGUI legendaTexto;
    [TextArea]
    public string mensagemProibido = "FeedBack";

    public GameObject player;

    [Header("Perdas se não falar com passarinho")]
    public float socialLost;
    public float ansiedadeAumento;
    public Passarinho passaro;

    public GameObject risco1;
    public GameObject risco2;
    public GameObject risco3;
    public GameObject risco4;



    void Update()
    {
        if (playerPerto && Input.GetKeyDown(KeyCode.E) && tarefas_Finalizadas)
        {
            SceneManager.LoadScene(nomeCenaDestino);

            if (!passaro.falou)
            {
                SedeSystem SedeSystem = player.GetComponent<SedeSystem>();
                if (SedeSystem != null)
                {
                    SedeSystem.BeberAgua(socialLost);
                }

                AnsiedadeSystem AnsiedadeSystem = player.GetComponent<AnsiedadeSystem>();
                if (AnsiedadeSystem != null)
                {
                    AnsiedadeSystem.Relaxar(ansiedadeAumento);
                }
            }

        }
        if (playerPerto && Input.GetKeyDown(KeyCode.E) && !tarefas_Finalizadas)
        {
            if (textoUI != null)
                textoUI.SetActive(true);
            legendaTexto.text = mensagemProibido;
            Invoke("desligarFala", 3f);

        }

        if (risco4 && risco3 && risco2 && risco1)
        {
            tarefas_Finalizadas = true; 
        }
    }

    private void desligarFala()
    {
        textoUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = true;
            InteracaoUIManager.Instance?.MostrarTexto(mensagemInteracao);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerPerto = false;
            InteracaoUIManager.Instance?.EsconderTexto();
        }
    }
}
