using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CelularUIManager : MonoBehaviour
{
    public GameObject menuApps;
    public GameObject celularImagem;
    public GameObject celularBorda;

    public GameObject telaMensagens;
    public GameObject telaCaramelinho;
    public GameObject telaConfiguracoes;
    public GameObject telaConfiguracoes2;
    public GameObject telaConfiguracoes3;
    public GameObject telaAlarme;         
    public GameObject cell;              
    public GameObject telaBlocoNotas;
    public GameObject telaCamera;
    public GameObject telaAlbumFotos;
    public GameObject telaY;
    public GameObject telaInForma;
    public GameObject telaGabi;
    public GameObject telaVideo1;
    public GameObject telaVideo2;
    public GameObject telaVideo3;
    public GameObject telaFoto1;
    public GameObject telaFoto2;
    public GameObject telaFoto3;
    public GameObject telaNave;

    public VideoPlayer videoConfigPlayer;

    public GameObject[] telasApps;

    public AlarmManager alarmManager;     // NOVO: Referência ao gerenciador de alarme
    public MensagensAppManager mensagensAppManager;

    public static CelularUIManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // faz o objeto sobreviver entre cenas
    }


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

       // if (mensagensAppManager != null)
           // mensagensAppManager.ResetarParaListaContatos();
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

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (videoConfigPlayer != null)
        {
            videoConfigPlayer.Play();
        }

        Debug.Log("⚙️ App Configurações aberto com vídeo.");
    }

    public void AbrirConfiguracoes2()
    {
        FecharTodasTelas();

        if (menuApps != null)
            menuApps.SetActive(false);

        if (telaConfiguracoes2 != null)
            telaConfiguracoes2.SetActive(true);

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (videoConfigPlayer != null)
        {
            videoConfigPlayer.Play();
        }

        Debug.Log("⚙️ App Configurações aberto com vídeo.");
    }

    public void AbrirConfiguracoes3()
    {
        FecharTodasTelas();

        if (menuApps != null)
            menuApps.SetActive(false);

        if (telaConfiguracoes3 != null)
            telaConfiguracoes3.SetActive(true);

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (videoConfigPlayer != null)
        {
            videoConfigPlayer.Play();
        }

        Debug.Log("⚙️ App Configurações aberto com vídeo.");
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

    public void AbrirBlocoNotas()
    {
        FecharTodasTelas();

        if (menuApps != null)
            menuApps.SetActive(false);

        if (telaBlocoNotas != null)
            telaBlocoNotas.SetActive(true);
    }

    public void AbrirCamera()
    {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaCamera != null)
            telaCamera.SetActive(true);

        Debug.Log("📷 App Câmera aberto!");
    }

    public void AbrirAlbumFotos()
     {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaAlbumFotos != null)
            telaAlbumFotos.SetActive(true);

        Debug.Log("📷 App Album aberto!");
    }
    
    public void AbrirTelaVideo1()
     {
        //FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaVideo1 != null)
            telaVideo1.SetActive(true);

        Debug.Log("📷 App Album aberto!");
    }
    
    public void AbrirTelaVideo2()
    {
        //FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaVideo2 != null)
            telaVideo2.SetActive(true);

        Debug.Log("📷 App Album aberto!");
    }

    public void AbrirTelaVideo3()
     {
        //FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaVideo3 != null)
            telaVideo3.SetActive(true);

        Debug.Log("📷 App Album aberto!");
    }

    public void AbrirTelaFoto1()
     {
        //FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaFoto1 != null)
            telaFoto1.SetActive(true);

        Debug.Log("📷 App Album aberto!");
    }

    public void AbrirTelaFoto2()
     {
        //FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaFoto2 != null)
            telaFoto2.SetActive(true);

        Debug.Log("📷 App Album aberto!");
    }

    public void AbrirTelaFoto3()
     {
        //FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaFoto3 != null)
            telaFoto3.SetActive(true);

        Debug.Log("📷 App Album aberto!");
    }

    public void AbrirY()
    {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaY != null)
            telaY.SetActive(true);

        Debug.Log("📷 App Y aberto!");
    }
    
    public void AbrirNave()
    {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaNave != null)
            telaNave.SetActive(true);

        Debug.Log("📷 App Nave aberto!");
    }

    public void AbrirInForma()
    {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaInForma != null)
            telaInForma.SetActive(true);

        Debug.Log("📷 App InForma aberto!");
    }

    public void AbrirTelaGabi()
     {
        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);

        if (telaGabi != null)
            telaGabi.SetActive(true);

        Debug.Log("📷 App Gabi aberto!");
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

        //if (mensagensAppManager != null)
            //mensagensAppManager.FechamentoCompletoMensagens();
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
