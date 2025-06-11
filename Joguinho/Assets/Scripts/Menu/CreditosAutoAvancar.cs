using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditosAutoAvancar : MonoBehaviour
{
    public float tempoParaMenu = 10f; 
    public string nomeCenaMenu = "Menu"; 

    void Start()
    {
        Invoke("VoltarParaMenu", tempoParaMenu);
    }

    void VoltarParaMenu()
    {
        SceneManager.LoadScene(nomeCenaMenu);
    }
}
