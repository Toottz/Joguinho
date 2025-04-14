using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaOrder : MonoBehaviour
{
    public string nomeDestinatario;

    [Header("Especial")]
    public bool cartaFinal = false; // ← Marque essa carta no Inspector como final

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InteracaoUIManager.Instance.MostrarTexto("Pressione 'E' para pegar");
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
