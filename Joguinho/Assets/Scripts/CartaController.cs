using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CartaController : MonoBehaviour
{
    public GameObject dialogoUI;
    public TextMeshProUGUI textoentrega;
    public GameObject[] cartasObject = new GameObject[3];
    public GameObject[] setasCasas = new GameObject[3];
    public GameObject[] Correio = new GameObject[3];

    [SerializeField] private string[] destinatarioAtual = new string[3];
    [SerializeField] private bool temCarta = false;

    private bool podePegar = false;
    private CartaOrder cartaNoChao;

    private bool podeEntregar = false;
    private NPCController npcProximo;
    private bool entregandoAgora = false; // ← novo controle de entrega atual

    private int indiceFala = 0;
    private List<Fala> falasAtuais = new List<Fala>();
    [SerializeField] private bool mostrandoDialogo = false;

    public MensagensController mensagensController;
    public bool tentouEntregar = false;

    public FirstPersonMovement movimento;
    public int quantidadedeentregas = 0;

    public GameObject risco;
    public GameObject dialogo;

    public AudioSource som;

    void Start()
    {
        dialogoUI.SetActive(false);
        for (int i = 0; i < destinatarioAtual.Length; i++)
            destinatarioAtual[i] = "";
    }

    void Update()
    {
        if (quantidadedeentregas >= 3)
            risco.SetActive(true);

        movimento.speed = dialogo.activeInHierarchy ? 0f : 5f;

        if (mostrandoDialogo && Input.GetKeyDown(KeyCode.E))
        {
            indiceFala++;
            MostrarFalaAtual();
            return;
        }

        if (podePegar && Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < destinatarioAtual.Length; i++)
            {
                if (destinatarioAtual[i] == "")
                {
                    destinatarioAtual[i] = cartaNoChao.nomeDestinatario;

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
                    if (i == 2) podePegar = false;
                    return;
                }
            }
        }

        if (podeEntregar && Input.GetKeyDown(KeyCode.E) && temCarta && !mostrandoDialogo && !entregandoAgora)
        {
            InteracaoUIManager.Instance.EsconderTexto();
            tentouEntregar = true;
            entregandoAgora = true; // ← impede cliques múltiplos

            for (int i = 0; i < destinatarioAtual.Length; i++)
            {
                if (npcProximo == null) break;

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
                        if (destinatarioAtual[j] != "") temCarta = true;

                    cartasObject[i].SetActive(false);
                    setasCasas[i].SetActive(false);
                    quantidadedeentregas++;

                    destinatarioAtual[i] = "";

                    // Importante: marca que o NPC foi entregue e remove
                    podeEntregar = false;
                    Destroy(npcProximo.gameObject);
                    npcProximo = null;
                    return;
                }
                else if (i == 2)
                {
                    MostrarDialogo(npcProximo != null ? npcProximo.mensagemErrada : "Isso não parece certo.");
                    entregandoAgora = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Carta"))
        {
            podePegar = true;
            cartaNoChao = other.GetComponent<CartaOrder>();
        }

        if (other.CompareTag("NPC"))
        {
            npcProximo = other.GetComponent<NPCController>();
            if (npcProximo != null)
                podeEntregar = true;
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
            EsconderDialogo();
            mostrandoDialogo = false;
            return;
        }

        Fala falaAtual = falasAtuais[indiceFala];
        textoentrega.text = falaAtual.texto;
        dialogoUI.SetActive(true);
    }

    void MostrarDialogo(string mensagem)
    {
        dialogoUI.SetActive(true);
        textoentrega.text = mensagem;
        Invoke(nameof(EsconderDialogo), 2.5f);
    }

    void EsconderDialogo()
    {
        mostrandoDialogo = false;
        tentouEntregar = false;
        entregandoAgora = false;
        dialogoUI.SetActive(false);
    }

    void VoltarParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
