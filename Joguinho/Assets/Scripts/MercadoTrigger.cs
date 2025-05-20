using UnityEngine;

public class MercadoTrigger : MonoBehaviour
{
    public GameObject uiPerguntaEntrar; // Painel com os botões "Sim" e "Não"

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && uiPerguntaEntrar != null)
        {
            uiPerguntaEntrar.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && uiPerguntaEntrar != null)
        {
            uiPerguntaEntrar.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
