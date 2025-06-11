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

    public AlarmManager alarmManager;
    public MensagensAppManager mensagensAppManager;

    public static CelularUIManager instance;
    public GameObject player;

    public GameObject setaSocialDown;
    public GameObject setaAnsiedadeDown;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");
    }

    public void AoAbrirCelular()
    {
        if (alarmManager != null && alarmManager.AlarmeAtivo())
        {
            if (cell != null) cell.SetActive(false);
            if (telaAlarme != null) telaAlarme.SetActive(true);
        }
        else
        {
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
    }

    public void AbrirCaramelinho()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player");

        FecharTodasTelas();

        if (celularImagem != null)
            celularImagem.SetActive(false);
        if (celularBorda != null)
            celularBorda.SetActive(false);

        if (player != null)
        {
            AnsiedadeSystem ansiedadeSystem = player.GetComponent<AnsiedadeSystem>();
            if (ansiedadeSystem != null)
            {
                ansiedadeSystem.Relaxar(-5);
                if (setaAnsiedadeDown != null) setaAnsiedadeDown.SetActive(true);
            }

            SedeSystem sedeSystem = player.GetComponent<SedeSystem>();
            if (sedeSystem != null)
            {
                sedeSystem.EatFood(-5);
                if (setaSocialDown != null) setaSocialDown.SetActive(true);
            }
        }

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
        if (menuApps != null) menuApps.SetActive(false);
        if (telaConfiguracoes != null) telaConfiguracoes.SetActive(true);
        if (celularImagem != null) celularImagem.SetActive(false);
        if (videoConfigPlayer != null) videoConfigPlayer.Play();
    }

    public void AbrirConfiguracoes2()
    {
        FecharTodasTelas();
        if (menuApps != null) menuApps.SetActive(false);
        if (telaConfiguracoes2 != null) telaConfiguracoes2.SetActive(true);
        if (celularImagem != null) celularImagem.SetActive(false);
        if (videoConfigPlayer != null) videoConfigPlayer.Play();
    }

    public void AbrirConfiguracoes3()
    {
        FecharTodasTelas();
        if (menuApps != null) menuApps.SetActive(false);
        if (telaConfiguracoes3 != null) telaConfiguracoes3.SetActive(true);
        if (celularImagem != null) celularImagem.SetActive(false);
        if (videoConfigPlayer != null) videoConfigPlayer.Play();
    }

    public void MostrarTelaAlarme()
    {
        if (telaAlarme != null) telaAlarme.SetActive(true);
        if (cell != null) cell.SetActive(false);
    }

    public void EsconderTelaAlarme()
    {
        if (telaAlarme != null) telaAlarme.SetActive(false);
        if (cell != null) cell.SetActive(true);
    }

    public void MostrarTelaAlarmeSomente()
    {
        if (telaAlarme != null) telaAlarme.SetActive(true);
        if (menuApps != null) menuApps.SetActive(false);
        if (celularImagem != null) celularImagem.SetActive(false);
    }

    public void AbrirBlocoNotas()
    {
        FecharTodasTelas();
        if (menuApps != null) menuApps.SetActive(false);
        if (telaBlocoNotas != null) telaBlocoNotas.SetActive(true);
    }

    public void AbrirCamera()
    {
        FecharTodasTelas();
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaCamera != null) telaCamera.SetActive(true);
    }

    public void AbrirAlbumFotos()
    {
        FecharTodasTelas();
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaAlbumFotos != null) telaAlbumFotos.SetActive(true);
    }

    public void AbrirTelaVideo1()
    {
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaVideo1 != null) telaVideo1.SetActive(true);
    }

    public void AbrirTelaVideo2()
    {
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaVideo2 != null) telaVideo2.SetActive(true);
    }

    public void AbrirTelaVideo3()
    {
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaVideo3 != null) telaVideo3.SetActive(true);
    }

    public void AbrirTelaFoto1()
    {
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaFoto1 != null) telaFoto1.SetActive(true);
    }

    public void AbrirTelaFoto2()
    {
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaFoto2 != null) telaFoto2.SetActive(true);
    }

    public void AbrirTelaFoto3()
    {
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaFoto3 != null) telaFoto3.SetActive(true);
    }

    public void AbrirY()
    {
        FecharTodasTelas();
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaY != null) telaY.SetActive(true);
    }

    public void AbrirNave()
    {
        FecharTodasTelas();
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaNave != null) telaNave.SetActive(true);
    }

    public void AbrirInForma()
    {
        FecharTodasTelas();
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaInForma != null) telaInForma.SetActive(true);
    }

    public void AbrirTelaGabi()
    {
        FecharTodasTelas();
        if (celularImagem != null) celularImagem.SetActive(false);
        if (telaGabi != null) telaGabi.SetActive(true);
    }

    public void VoltarAoMenu()
    {
        FecharTodasTelas();
        if (celularImagem != null) celularImagem.SetActive(true);
        if (celularBorda != null) celularBorda.SetActive(true);
        if (menuApps != null) menuApps.SetActive(true);
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