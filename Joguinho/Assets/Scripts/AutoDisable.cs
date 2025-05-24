using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    [Tooltip("Tempo em segundos antes de desativar")]
    public float disableDelay = 3f;

    private void OnEnable()
    {
        // Quando o objeto é ativado, inicia a contagem para desativar
        Invoke("DisableObject", disableDelay);
    }

    private void OnDisable()
    {
        // Cancela a invocação se o objeto for desativado manualmente
        CancelInvoke("DisableObject");
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}