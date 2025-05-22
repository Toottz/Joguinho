using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.SymbolStore;
using UnityEngine.VFX;

public class CartaOrder : MonoBehaviour
{
    //public Image cartaUI;
    public GameObject cartaUI;
    public string nomeDestinatario;
    public GameObject setaCarta;
    public GameObject setaCasa;

    //[Header("Especial")]
    //public bool cartaFinal = false; // ← Marque essa carta no Inspector como final

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteracaoUIManager.Instance.MostrarTexto("Pressione 'E' para pegar a carta");
        }
    }
    

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteracaoUIManager.Instance.EsconderTexto();
        }
    }
}
