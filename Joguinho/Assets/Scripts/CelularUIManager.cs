using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelularUIManager : MonoBehaviour
{
    public GameObject menuApps;
    public GameObject celularImagem;

    public GameObject telaMensagens;
    public GameObject telaCaramelinho;
    public GameObject telaConfiguracoes;

    public GameObject[] telasApps; // Todas as telas de apps, incluindo a "TelaSlotCaramelinho"

    public void AbrirMensagens()
    {
        FecharTodasTelas();
        if (celularImagem != null)
            celularImagem.SetActive(false);
        telaMensagens.SetActive(true);
    }

    public void AbrirCaramelinho()
    {
        FecharTodasTelas();
        if (celularImagem != null)
            celularImagem.SetActive(false);

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

    public void AbrirConfiguracoes()
    {
        FecharTodasTelas();
        menuApps.SetActive(false);
        if (telaConfiguracoes != null)
            telaConfiguracoes.SetActive(true);
        // Mantenha a imagem do celular visível neste app
    }

    public void VoltarAoMenu()
    {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(true);

        if (menuApps != null)
            menuApps.SetActive(true);
    }

    private void FecharTodasTelas()
    {
        foreach (GameObject tela in telasApps)
        {
            if (tela != null)
                tela.SetActive(false);
        }

        // Também desativa diretamente as telas específicas, por segurança
        if (telaMensagens != null) telaMensagens.SetActive(false);
        if (telaCaramelinho != null) telaCaramelinho.SetActive(false);
        if (telaConfiguracoes != null) telaConfiguracoes.SetActive(false);
    }
}

