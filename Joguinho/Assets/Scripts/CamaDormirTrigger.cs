using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CamaDormirTrigger : MonoBehaviour
{
    public GameObject mensagemUI;
    public GameObject telaPreta;
    public float tempoAntesTransicao = 1.5f;
    public string nomeProximaCena = "Dia2";

    private bool jogadorPerto = false;
    private bool dormindo = false;

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

        SceneManager.LoadScene(nomeProximaCena);
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
