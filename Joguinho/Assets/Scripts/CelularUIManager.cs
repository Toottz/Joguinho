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
    public GameObject telaAlarme;         // NOVO: Tela do app de alarme
    public GameObject cell;               // NOVO: Container com a interface normal do celular


    public GameObject[] telasApps;

    public AlarmManager alarmManager;     // NOVO: Referência ao gerenciador de alarme
    public MensagensAppManager mensagensAppManager;

    public void AoAbrirCelular()
    {
        if (alarmManager != null && alarmManager.AlarmeAtivo())
        {
            // Mostra a interface especial de alarme (sem borda e apps)
            if (cell != null) cell.SetActive(false);
            if (telaAlarme != null) telaAlarme.SetActive(true);
        }
        else
        {
            // Abre o celular normalmente
            if (cell != null) cell.SetActive(true);
            if (telaAlarme != null) telaAlarme.SetActive(false);

            VoltarAoMenu();
        }
    }

    public void AoFecharCelular()
    {
        if (telaAlarme != null) telaAlarme.SetActive(false);
        if (cell != null) cell.SetActive(true);
    }

    public void DesligarAlarmeDoBotao()
    {
        if (alarmManager != null)
            alarmManager.DesligarAlarmeViaBotao();

        if (telaAlarme != null) telaAlarme.SetActive(false);
        if (cell != null) cell.SetActive(true);
    }

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

    public void MostrarTelaAlarme()
    {
        if (telaAlarme != null)
            telaAlarme.SetActive(true);
        if (cell != null)
            cell.SetActive(false); // Esconde todo o resto do celular
    }

    public void EsconderTelaAlarme()
    {
        if (telaAlarme != null)
            telaAlarme.SetActive(false);
        if (cell != null)
            cell.SetActive(true); // Traz o celular normal de volta
    }

    public void MostrarTelaAlarmeSomente()
    {
        if (telaAlarme != null)
            telaAlarme.SetActive(true);

        if (menuApps != null)
            menuApps.SetActive(false);

        if (celularImagem != null)
            celularImagem.SetActive(false);
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
        if (telaAlarme != null) telaAlarme.SetActive(false);
    }
}
