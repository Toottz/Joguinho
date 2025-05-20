using UnityEngine;

public class InFormaAppManager : MonoBehaviour
{
    public GameObject telaInforma;

    public void Abrir()
    {
        if (telaInforma != null)
            telaInforma.SetActive(true);
    }

    public void Fechar()
    {
        if (telaInforma != null)
            telaInforma.SetActive(false);
    }
}
