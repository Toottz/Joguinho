using UnityEngine;

public class Hamburger : MonoBehaviour
{
    public float hungerRestoreAmount = 100f;
    private bool isPlayerNearby = false;
    private GameObject player;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Pressionar "F" para comer
        {
            HungerSystem hungerSystem = player.GetComponent<HungerSystem>();
            if (hungerSystem != null)
            {
                hungerSystem.EatFood(hungerRestoreAmount);
            }

            InteracaoUIManager.Instance.EsconderTexto(); // Esconde o texto ao comer
            Destroy(gameObject); // Remove o hambúrguer
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
