using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Dialogo interativo
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
        // Avancar dialogo com E
        if (mostrandoDialogo && Input.GetKeyDown(KeyCode.E))
        {
            indiceFala++;
            MostrarFalaAtual();
            return; 
        }

        // Pegar carta
        if (podePegar && Input.GetKeyDown(KeyCode.E) && !temCarta)
        {
            destinatarioAtual = cartaNoChao.nomeDestinatario;
            Destroy(cartaNoChao.gameObject);
            temCarta = true;
            cartaUI.gameObject.SetActive(true);
        }

        // Entregar carta
        if (podeEntregar && Input.GetKeyDown(KeyCode.E) && temCarta && !mostrandoDialogo)
        {
            if (npcProximo.nomeNPC == destinatarioAtual)
            {
                falasAtuais = npcProximo.dialogoCompleto;
                indiceFala = 0;
                mostrandoDialogo = true;
                MostrarFalaAtual();
                
                PlayerWallet.Instance.AdicionarDinheiro(10f);

                temCarta = false;
                cartaUI.gameObject.SetActive(false);
                destinatarioAtual = "";
            }
            else
            {
                MostrarDialogo(npcProximo.mensagemErrada);
            }
        }

        // Avancar dialogo
        if (mostrandoDialogo && Input.GetKeyDown(KeyCode.E))
        {
            indiceFala++;
            MostrarFalaAtual();
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
            // Fim do diálogo
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
}