using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CartaController : MonoBehaviour
{
    public Image cartaUI;
    public GameObject dialogoUI;
    public TextMeshProUGUI textoentrega;

    [SerializeField]private string destinatarioAtual = "";
    [SerializeField]  private bool temCarta = false;

    private bool podePegar = false;
    private CartaOrder cartaNoChao;

    private bool podeEntregar = false;
    private NPCController npcProximo;

    private bool entregouCartaFinal = false;

    private int indiceFala = 0;
    private List<Fala> falasAtuais = new List<Fala>();
    [SerializeField]private bool mostrandoDialogo = false;

    public MensagensController mensagensController;
    public bool tentouEntregar = false;

    void Start()
    {
        cartaUI.gameObject.SetActive(false);
        dialogoUI.SetActive(false);
    }

    void Update()
    {
        if (mostrandoDialogo && Input.GetKeyDown(KeyCode.E))
        {
            indiceFala++;
            MostrarFalaAtual();
            return;
        }

        if (podePegar && Input.GetKeyDown(KeyCode.E) && !temCarta)
        {
            destinatarioAtual = cartaNoChao.nomeDestinatario;
            //entregouCartaFinal = cartaNoChao.cartaFinal;

            if (entregouCartaFinal)
                Debug.Log("📩 Pegou a carta final!");

            InteracaoUIManager.Instance.EsconderTexto();
            Destroy(cartaNoChao.gameObject);
            cartaNoChao = null;

            temCarta = true;
            cartaUI.gameObject.SetActive(true);
        }

        if (podeEntregar && Input.GetKeyDown(KeyCode.E) && temCarta && !mostrandoDialogo)
        {
            InteracaoUIManager.Instance.EsconderTexto();
            tentouEntregar = true;
            Debug.Log("ver se o if de cima existe");
            if (npcProximo.nomeNPC == destinatarioAtual)
            {
                Debug.Log("So pra indificar se entrou aqui");
                falasAtuais = npcProximo.dialogoCompleto;
                indiceFala = 0;
                mostrandoDialogo = true;
                MostrarFalaAtual();

                PlayerWallet.Instance.AdicionarDinheiro(25f);
                temCarta = false;
                cartaUI.gameObject.SetActive(false);
                destinatarioAtual = "";

                //if (entregouCartaFinal)
                //{
                //    Debug.Log("🎯 Entregou a carta final! Voltando para o menu...");
                //    Invoke("VoltarParaMenu", 4f);
                //}
            }
            else
            {
                MostrarDialogo(npcProximo.mensagemErrada);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
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
            EsconderDialogo();
            mostrandoDialogo = false;
            return;
        }

        Fala falaAtual = falasAtuais[indiceFala];
        string nome = falaAtual.quemFala == Fala.TipoDeFala.NPC ? npcProximo.nomeNPC : "Você";

        textoentrega.text = $"{nome}: {falaAtual.texto}";
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
