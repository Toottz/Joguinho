using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CamaDormirTrigger : MonoBehaviour
{
    public GameObject mensagemUI;
    public GameObject telaPreta;
    public float tempoAntesTransicao = 1.5f;

    private bool jogadorPerto = false;
    private bool dormindo = false;

    [Header("Configurações de Ansiedade")]
    public float limiteAnsiedade = 70f;
    public string cenaAnsiedadeAlta = "Cena_Stress";
    public string cenaAnsiedadeBaixa = "Cena_Normal";
    void Start()
    {
        if (mensagemUI != null)
            mensagemUI.SetActive(false);

        if (telaPreta != null)
            telaPreta.SetActive(false);
    }

    void Update()
    {
        if (jogadorPerto && !dormindo && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(DormirERotacionarCena());
        }
    }

    IEnumerator DormirERotacionarCena()
    {
        dormindo = true;

        if (mensagemUI != null)
            mensagemUI.SetActive(false);

        if (telaPreta != null)
            telaPreta.SetActive(true);

        yield return new WaitForSeconds(tempoAntesTransicao);

        string cenaDestino = Estatico.Ansiedade >= limiteAnsiedade ?
            cenaAnsiedadeAlta :
            cenaAnsiedadeBaixa;

        SceneManager.LoadScene(cenaDestino);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !dormindo)
        {
            jogadorPerto = true;
            if (mensagemUI != null)
                mensagemUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            if (mensagemUI != null)
                mensagemUI.SetActive(false);
        }
    }
}
