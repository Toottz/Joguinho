using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaOrder : MonoBehaviour
{
    public string nomeDestinatario;

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