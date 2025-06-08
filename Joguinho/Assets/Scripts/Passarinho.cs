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
    public bool falou = false;
    public GameObject setaSocialUp;
    public GameObject setaAnsiedadeUp;

    public GameObject passarinhocode;
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
                setaSocialUp.SetActive(true);
                Debug.Log("afetando social");
            }

            AnsiedadeSystem AnsiedadeSystem = player.GetComponent<AnsiedadeSystem>();
            if (AnsiedadeSystem != null)
            {
                AnsiedadeSystem.Relaxar(AnsiedadeDiminuir);
                setaAnsiedadeUp.SetActive(true);
                Debug.Log("afetando ansiedade");
            }

            if (falaPersona != null)
                falaPersona.SetActive(true);

            Invoke(nameof(desligarFala), 2f);

            falou = true;

            InteracaoUIManager.Instance.EsconderTexto();
            passarinhocode.SetActive(false );
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
