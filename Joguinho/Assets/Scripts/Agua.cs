using UnityEngine;

public class Agua : MonoBehaviour
{
    public float sedeRestoreAmount = 100f;
    private bool isPlayerNearby = false;
    private GameObject player;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F)) // Pressionar "F" para beber
        {
            SedeSystem sedeSystem = player.GetComponent<SedeSystem>();
            if (sedeSystem != null)
            {
                sedeSystem.EatFood(sedeRestoreAmount);
            }

            InteracaoUIManager.Instance.EsconderTexto(); // Esconde o texto ao beber
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
            InteracaoUIManager.Instance.MostrarTexto("Pressione 'F' para beber");
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
