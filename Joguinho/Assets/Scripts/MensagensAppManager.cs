using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensagensAppManager : MonoBehaviour
{
    public GameObject listaContatos;
    public GameObject conversaChefe;
    public GameObject conversaMae;

    public void AbrirConversaChefe()
    {
        listaContatos.SetActive(false);
        conversaChefe.SetActive(true);
    }

    public void AbrirConversaMae()
    {
        listaContatos.SetActive(false);
        conversaMae.SetActive(true);
    }

    public void VoltarParaContatos()
    {
        conversaChefe.SetActive(false);
        conversaMae.SetActive(false);
        listaContatos.SetActive(true);
    }
}
