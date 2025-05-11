using UnityEngine;

public class ColetarRoupa : MonoBehaviour
{
public string mensagemInteracao = "Pressione E para pegar a roupa";
public GameObject textoUI; // opcional: texto que aparece na tela
private bool dentroDaArea = false;

void Start()
{
    if (textoUI != null)
        textoUI.SetActive(false);
}

void Update()
{
    if (dentroDaArea && Input.GetKeyDown(KeyCode.E))
    {
        Coletar();
    }
}

void Coletar()
{
    if (textoUI != null)
        textoUI.SetActive(false);

    // Aqui você pode colocar algo como dar roupa no inventário

    gameObject.SetActive(false); // Esconde a roupa da cena
    Debug.Log("👕 Roupa coletada!");
}

void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        dentroDaArea = true;

        if (textoUI != null)
            textoUI.SetActive(true);
    }
}

void OnTriggerExit(Collider other)
{
    if (other.CompareTag("Player"))
    {
        dentroDaArea = false;

        if (textoUI != null)
            textoUI.SetActive(false);
    }
}
}