using UnityEngine;

public class Hamburger : MonoBehaviour
{
    public float hungerRestoreAmount = 100f;
    private bool isPlayerNearby = false;
    private GameObject player;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F)) // Pressionar "F" para comer
        {
            HungerSystem hungerSystem = player.GetComponent<HungerSystem>();
            if (hungerSystem != null)
            {
                hungerSystem.EatFood(hungerRestoreAmount);
            }
            Destroy(gameObject); // Remove o hambÃºrguer
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
        }
    }
}