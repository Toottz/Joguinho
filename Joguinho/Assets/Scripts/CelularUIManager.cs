using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelularUIManager : MonoBehaviour
{
    public GameObject menuApps;
    public GameObject telaMensagens;
    public GameObject celularImagem;
    public GameObject telaCaramelinho;

    public GameObject[] telasApps; // Certifique-se de preencher isso no Inspector com todas as telas de apps, incluindo "TelaSlotCaramelinho"

    public void AbrirMensagens()
    {
        menuApps.SetActive(false);
        celularImagem.SetActive(false);
        telaMensagens.SetActive(true);
    }

    public void AbrirCaramelinho()
    {
        menuApps.SetActive(false);
        celularImagem.SetActive(false);

        // Desativa todas as outras telas
        foreach (GameObject tela in telasApps)
        {
            if (tela != null)
                tela.SetActive(false);
        }

        // Ativa somente a tela do Caramelinho
        foreach (GameObject tela in telasApps)
        {
            if (tela != null && tela.name == "TelaSlotCaramelinho")
            {
                tela.SetActive(true);
                Debug.Log("📱 Caramelinho aberto!");
                break;
            }
        }
    }

    public void VoltarAoMenu()
    {
        telaMensagens.SetActive(false);
        telaCaramelinho.SetActive(false);

        // Desativa todas as telas de app se estiver usando array
        if (telasApps != null)
        {
            foreach (GameObject tela in telasApps)
            {
                if (tela != null)
                    tela.SetActive(false);
            }
        }

        celularImagem.SetActive(true);
        menuApps.SetActive(true);
    }
}
