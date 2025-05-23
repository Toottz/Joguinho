using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MensagensAppManager : MonoBehaviour
{
    public GameObject listaContatos;
    public GameObject conversaChefe;
    public GameObject conversaMae;
    public GameObject botaovoltar;
    public GameObject botaovoltarmenu;
    public GameObject celularBorda;

    public void AbrirConversaChefe()
    {
        listaContatos.SetActive(false);
        conversaChefe.SetActive(true);
        botaovoltar.SetActive(true);
        botaovoltarmenu.SetActive(false);

        if (celularBorda != null)
            celularBorda.SetActive(false);
    }

    public void AbrirConversaMae()
    {
        listaContatos.SetActive(false);
        conversaMae.SetActive(true);
        botaovoltar.SetActive(true);
        botaovoltarmenu.SetActive(false);   

        if (celularBorda != null)
            celularBorda.SetActive(false);
    }

    public void VoltarParaContatos()
    {
        Debug.Log("Voltar para contatos");
        conversaChefe.SetActive(false);
        conversaMae.SetActive(false);
        listaContatos.SetActive(true);
        botaovoltar.SetActive(false);
        botaovoltarmenu.SetActive(true);

        if (celularBorda != null)
            celularBorda.SetActive(true);
    }

    public void ResetarParaListaContatos()
    {
        if (conversaChefe != null)
            conversaChefe.SetActive(false);

        if (conversaMae != null)
            conversaMae.SetActive(false);

        if (listaContatos != null)
            listaContatos.SetActive(true);
    }

    public void FechamentoCompletoMensagens()
    {
        if (listaContatos != null)
            listaContatos.SetActive(false);
        if (conversaChefe != null)
            conversaChefe.SetActive(false);
        if (conversaMae != null)
            conversaMae.SetActive(false);
    }
}
