using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics.SymbolStore;
using Unity.VisualScripting;

public class CartaController : MonoBehaviour
{
    public GameObject dialogoUI;
    public TextMeshProUGUI textoentrega;
    public GameObject[] cartasObject = new GameObject[3];
    public GameObject[] setasCasas = new GameObject[3];
    public GameObject[] Correio = new GameObject[3];

    [SerializeField] private string [] destinatarioAtual = new string [3];
    [SerializeField]  private bool temCarta = false;

    private bool podePegar = false;
    private CartaOrder cartaNoChao;

    private bool podeEntregar = false;
    private NPCController npcProximo;

    //private bool entregouCartaFinal = false;

    private int indiceFala = 0;
    private List<Fala> falasAtuais = new List<Fala>();
    [SerializeField]private bool mostrandoDialogo = false;

    public MensagensController mensagensController;
    public bool tentouEntregar = false;

    public FirstPersonMovement movimento;
    public int quantidadedeentregas=0;

    public GameObject risco;
    public GameObject dialogo;

    public AudioSource som;

    void Start()
    {
        //cartaUI.gameObject.SetActive(false);
        dialogoUI.SetActive(false);
        for (int i = 0; i < destinatarioAtual.Length; i++)
        {
            destinatarioAtual[i] = "";
        }
    }

    void Update()
    {
        if(quantidadedeentregas>=3)
            risco.SetActive(true);
        if (dialogo.activeInHierarchy)
        {
            movimento.speed = 0f;

        }
        if (!dialogo.activeInHierarchy)
        {
            movimento.speed = 5f;
        }
        if (mostrandoDialogo && Input.GetKeyDown(KeyCode.E))
        {
            indiceFala++;
            Debug.Log(indiceFala);
            MostrarFalaAtual();
            Debug.Log($"aqeui é o mostrarfala atual{falasAtuais.Count}");
            return;
        }

        if (podePegar && Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < destinatarioAtual.Length; i++) 
            {
                Debug.Log("Ta entrando no for pode pegar");
                if (destinatarioAtual[i] == "")
                {
                    Debug.Log("Ta entrnado no if do pode pegar");
                    destinatarioAtual[i] = cartaNoChao.nomeDestinatario;

                    //entregouCartaFinal = cartaNoChao.cartaFinal;
                    //if (entregouCartaFinal)
                    //    Debug.Log("📩 Pegou a carta final!");

                    InteracaoUIManager.Instance.EsconderTexto();

                    cartasObject[i] = cartaNoChao.cartaUI;
                    cartasObject[i].SetActive(true);

                    setasCasas[i] = cartaNoChao.setaCasa;
                    cartaNoChao.setaCasa.SetActive(true);

                    Correio[i] = cartaNoChao.Correio;
                    cartaNoChao.Correio.SetActive(true);

                    cartaNoChao.setaCarta.SetActive(false);

                    Destroy(cartaNoChao.gameObject);
                    cartaNoChao = null;

                    temCarta = true;

                    if (i == 2)
                    {
                        podePegar = false;
                    }
                    return;

                }
            }
        }

        if (podeEntregar && Input.GetKeyDown(KeyCode.E) && temCarta && !mostrandoDialogo)
        {
            InteracaoUIManager.Instance.EsconderTexto();
            tentouEntregar = true;
            for (int i = 0; i < destinatarioAtual.Length; i++)
            {
                if (destinatarioAtual[i] == npcProximo.nomeNPC)
                {
                    falasAtuais = npcProximo.dialogoCompleto;
                    indiceFala = 0;
                    mostrandoDialogo = true;
                    MostrarFalaAtual();
                    som.Play();
                    PlayerWallet.Instance.AdicionarDinheiro(25f);
                    temCarta = false;

                    for (int j = 0; j < destinatarioAtual.Length; j++)
                    {
                        if (destinatarioAtual[j] != "")
                        {
                            temCarta = true;
                        }
                    }

                    cartasObject[i].SetActive(false);
                    setasCasas[i].SetActive(false);
                    quantidadedeentregas++;

                    podeEntregar = false;
                    npcProximo = null;

                    Destroy(npcProximo.gameObject);

                    destinatarioAtual[i] = "";
                    return;
                }
                else if (i == 2)
                {
                    MostrarDialogo(npcProximo.mensagemErrada);
                }
            }
        }
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Carta"))
        {
            podePegar = true;
            cartaNoChao = other.GetComponent<CartaOrder>();
        }

        if (other.CompareTag("NPC"))
        {
            podeEntregar = true;
            npcProximo = other.GetComponent<NPCController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Carta"))
        {
            podePegar = false;
            cartaNoChao = null;
        }

        if (other.CompareTag("NPC"))
        {
            podeEntregar = false;
            npcProximo = null;
        }
    }

    void MostrarFalaAtual()
    {

        if (indiceFala >= falasAtuais.Count)
        {
            Debug.Log("acabo a fala");
            EsconderDialogo();
            mostrandoDialogo = false;
            return;
        }

        Fala falaAtual = falasAtuais[indiceFala];

        textoentrega.text = falaAtual.texto;
        dialogoUI.SetActive(true);

        Debug.Log($"Mostrando fala {indiceFala} de {falasAtuais.Count}");
    }

    void MostrarDialogo(string mensagem)
    {
        dialogoUI.SetActive(true);
        textoentrega.text = mensagem;
        Invoke("EsconderDialogo", 2.5f);
    }

    void EsconderDialogo()
    {
        Debug.Log("EsconerDialogo");
        mostrandoDialogo = false;
        tentouEntregar= false;
        dialogoUI.SetActive(false);
    }

    void VoltarParaMenu()
    {
        Debug.Log("🔁 Carregando cena do menu...");
        Debug.Log($"Cena atual: {SceneManager.GetActiveScene().name}");
        Debug.Log($"Total de cenas carregadas: {SceneManager.sceneCount}");

        Time.timeScale = 1f; // Garante que o tempo esteja normal
        SceneManager.LoadScene("Menu"); // ← Certifique-se que esse é o nome exato da cena
    }
}
