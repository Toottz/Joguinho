using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CartaController : MonoBehaviour
{
    public Image cartaUI;
    public GameObject dialogoUI;
    public Text textoDialogo;

    private string destinatarioAtual = "";
    private bool temCarta = false;

    private bool podePegar = false;
    private CartaOrder cartaNoChao;

    private bool podeEntregar = false;
    private NPCController npcProximo;

    private bool entregouCartaFinal = false;

    private int indiceFala = 0;
    private List<Fala> falasAtuais = new List<Fala>();
    private bool mostrandoDialogo = false;

    public MensagensController mensagensController;

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
            entregouCartaFinal = cartaNoChao.cartaFinal;

            if (entregouCartaFinal)
                Debug.Log("📩 Pegou a carta final!");

            Destroy(cartaNoChao.gameObject);
            cartaNoChao = null;

            temCarta = true;
            cartaUI.gameObject.SetActive(true);
        }

        if (podeEntregar && Input.GetKeyDown(KeyCode.E) && temCarta && !mostrandoDialogo)
        {
            if (npcProximo.nomeNPC == destinatarioAtual)
            {
                falasAtuais = npcProximo.dialogoCompleto;
                indiceFala = 0;
                mostrandoDialogo = true;
                MostrarFalaAtual();

                PlayerWallet.Instance.AdicionarDinheiro(25f);
                temCarta = false;
                cartaUI.gameObject.SetActive(false);
                destinatarioAtual = "";

                if (entregouCartaFinal)
                {
                    Debug.Log("🎯 Entregou a carta final! Voltando para o menu...");
                    Invoke("VoltarParaMenu", 4f);
                }
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

        textoDialogo.text = $"{nome}: {falaAtual.texto}";
        dialogoUI.SetActive(true);

        Debug.Log($"Mostrando fala {indiceFala} de {falasAtuais.Count}");
    }

    void MostrarDialogo(string mensagem)
    {
        dialogoUI.SetActive(true);
        textoDialogo.text = mensagem;
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
