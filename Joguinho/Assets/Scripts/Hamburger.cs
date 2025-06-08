using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hamburger : MonoBehaviour
{
    public float hungerRestoreAmount = 100f;
    private bool isPlayerNearby = false;
    private GameObject player;
    public GameObject falaPersona;
    public GameObject risco;
    public GameObject setaFomeUp;
    public GameObject fade;
    public GameObject comida;
    public AudioSource som;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Pressionar "F" para comer
        {
            som.Play();
            HungerSystem hungerSystem = player.GetComponent<HungerSystem>();
            if (hungerSystem != null)
            {
                hungerSystem.EatFood(hungerRestoreAmount, -1f);
                setaFomeUp.SetActive(true);
            }
            if (fade != null)
            {
                fade.SetActive(true);
                Image img = fade.GetComponent<Image>();
                if (img != null)
                    img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
                Invoke("telapreta", 2f);
            }

            if (falaPersona != null)
                falaPersona.SetActive(true);
            if (risco != null) 
                risco.SetActive(true);

            InteracaoUIManager.Instance.EsconderTexto(); // Esconde o texto ao comer
            comida.SetActive(false);
            Debug.Log("destroi hamburguer");

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
    private void telapreta()
    {
        if (fade != null)
            fade.SetActive(false);
    }
}
