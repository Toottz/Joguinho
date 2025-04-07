using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string proximaFase;
    //public GameObject[] itensMenu;
    //public GameObject[] itensConfig;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;

    //public AudioSource som;

    public void StarGame()
    {
        //som.Play();
        SceneManager.LoadScene(proximaFase);
    }

    public void AbrirOpcoes()
    {
        //som.Play();
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void FecharOpcoes()
    {
        //som.Play();
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void QuitGame()
    {
        //Mensagem no Unity de saida do jogo
        //som.Play();
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }
    
}