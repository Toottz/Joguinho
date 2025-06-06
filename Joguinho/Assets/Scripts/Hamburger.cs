using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hamburger : MonoBehaviour
{
    public float hungerRestoreAmount = 100f;
    private bool isPlayerNearby = false;
    private GameObject player;
    public GameObject falaPersona;
    public Toggle toggleComida;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Pressionar "F" para comer
        {
            HungerSystem hungerSystem = player.GetComponent<HungerSystem>();
            if (hungerSystem != null)
            {
                hungerSystem.EatFood(hungerRestoreAmount, -1f);
            }

            if (falaPersona != null)
                falaPersona.SetActive(true);

            InteracaoUIManager.Instance.EsconderTexto(); // Esconde o texto ao comer
            Destroy(gameObject); // Remove o hambúrguer
            if (toggleComida != null)
                toggleComida.isOn = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
            InteracaoUIManager.Instance.MostrarTexto("Pressione 'E' para comer");
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
