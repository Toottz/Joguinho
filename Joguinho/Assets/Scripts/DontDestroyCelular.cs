using UnityEngine;

public class DontDestroyCelular : MonoBehaviour
{
    private void Awake()
    {
        // Garante que só exista um celular persistente
        if (FindObjectsOfType<DontDestroyCelular>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
}
