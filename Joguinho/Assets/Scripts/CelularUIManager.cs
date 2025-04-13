using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelularUIManager : MonoBehaviour
{
    public GameObject menuApps;
    public GameObject celularImagem;
    public GameObject celularBorda;

    public GameObject telaMensagens;
    public GameObject telaCaramelinho;
    public GameObject telaConfiguracoes;

    public GameObject[] telasApps;

    public MensagensAppManager mensagensAppManager;

    public void AbrirMensagens()
    {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaMensagens != null)
            telaMensagens.SetActive(true);

        if (mensagensAppManager != null)
            mensagensAppManager.ResetarParaListaContatos();
    }

    public void AbrirCaramelinho()
    {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);
        if (celularBorda != null)
            celularBorda.SetActive(false);

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

        if (menuApps != null)
            menuApps.SetActive(false);

        if (telaConfiguracoes != null)
            telaConfiguracoes.SetActive(true);
    }

    public void VoltarAoMenu()
    {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(true);
        if (celularBorda != null)
            celularBorda.SetActive(true);
        if (menuApps != null)
            menuApps.SetActive(true);

        // Novo: garante que nenhuma conversa continue aberta
        if (mensagensAppManager != null)
            mensagensAppManager.FechamentoCompletoMensagens();
    }

    private void FecharTodasTelas()
    {
        foreach (GameObject tela in telasApps)
        {
            if (tela != null)
                tela.SetActive(false);
        }

        if (telaMensagens != null) telaMensagens.SetActive(false);
        if (telaCaramelinho != null) telaCaramelinho.SetActive(false);
        if (telaConfiguracoes != null) telaConfiguracoes.SetActive(false);
    }
}
