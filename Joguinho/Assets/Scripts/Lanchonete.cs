using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lanchonete : MonoBehaviour
{
    public float hungerRestoreAmount = 100f;
    private bool isPlayerNearby = false;
    private GameObject player;
    public PlayerWallet wallet;
    public GameObject dialogoUI;
    public TextMeshProUGUI textoDialogosemFome;
    public TextMeshProUGUI textDialogosemDinheiro;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F)) // Pressionar "F" para comer
        {
            HungerSystem hungerSystem = player.GetComponent<HungerSystem>();
            if (hungerSystem != null)
            {
                if (!hungerSystem.TaSemFome()) 
                {
                        if (wallet.GastarDinheiro(20))
                        {
                            hungerSystem.EatFood(hungerRestoreAmount);
                        }
                        else
                        {
                            Invoke("MostrarDialogosemDinheiro", 0.00001f);
                        }
                }
                else
                {
                    Invoke("MostrarDialogosemFome", 0.00001f);
                }
            }
        }
    }
    private void MostrarDialogosemFome()
    {
        dialogoUI.SetActive(true);
        textoDialogosemFome.text = "Não to com fome agora";
        Invoke("EsconderDialogo", 2.5f);
    }

    private void MostrarDialogosemDinheiro()
    {
        dialogoUI.SetActive(true);
        textDialogosemDinheiro.text = "Estou sem dinheiro, que triste";
        Invoke("EsconderDialogo", 2.5f);
    }

    private void EsconderDialogo()
    {
        dialogoUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
            InteracaoUIManager.Instance.MostrarTexto("Pressione 'F' para comer, essa ação gastará 20Reais");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
            InteracaoUIManager.Instance.EsconderTexto();
        }
    }
}