using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passarinho : MonoBehaviour
{
    public float socialRestore = 5f;
    public float AnsiedadeDiminuir = 3f;
    private bool isPlayerNearby = false;
    private GameObject player;
    public Collision Collision;
    public GameObject falaPersona;
    // Start is called before the first frame update
    void Start()
    {
        if (falaPersona != null)
            falaPersona.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E)) // Pressionar "F" para comer
        {
            SedeSystem SedeSystem = player.GetComponent<SedeSystem>();
            if (SedeSystem != null)
            {
                SedeSystem.BeberAgua(socialRestore);
            }

            AnsiedadeSystem AnsiedadeSystem = player.GetComponent<AnsiedadeSystem>();
            if (AnsiedadeSystem != null)
            {
                AnsiedadeSystem.Relaxar(AnsiedadeDiminuir);
            }

            if (falaPersona != null)
                falaPersona.SetActive(true);

            Invoke(nameof(desligarFala), 2f);

            InteracaoUIManager.Instance.EsconderTexto(); // Esconde o texto ao comer
            Destroy(gameObject); // Remove o hambúrguer
        }
    }
    private void desligarFala()
    {
        falaPersona.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            player = other.gameObject;
            InteracaoUIManager.Instance.MostrarTexto("Pressione 'E' para falar com");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            player = null;
            InteracaoUIManager.Instance.EsconderTexto();
        }
    }
}
