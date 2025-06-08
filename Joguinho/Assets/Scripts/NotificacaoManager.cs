using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class NotificacaoManager : MonoBehaviour
{
    [Header("UIs")]
    public GameObject Celular;
    public NotificacaoForaCelular notificacao;
    public NotificacaoDentroCelular notificacaoDentro;

    [Header("Mensagens por hora")]
    [TextArea]
    public string[] dailyMessages;
    [TextArea]
    public string[] app;
    public Color[] cor;
    public RenderTexture[] logos;
    public float[] triggerHours;
    public float[] triggerMinutes;

    [Header("Mensagem Bom dia")]
    [TextArea]
    public string[] mensagensBlocoDeNotas;
    [TextArea]
    public string appBlocoDeNotas;
    public Color corBlocoDeNotas;
    public RenderTexture logoBlocoDeNotas;

    [Header("Ativadores")]
    public GameObject ativador2;
    public GameObject ativador3;
    public GameObject ativador4;
    public GameObject ativador5;
    public GameObject ativador6;

    // Controles de estado
    private bool jaNotificou;
    private bool[] blocodenotas = new bool[5]; // Substitui as variáveis individuais
    private bool[] invokeChamado = new bool[5]; // Controla se o Invoke já foi chamado

    void Update()
    {
        float currentHour = Estatico.Hora;
        float currentMinuto = Estatico.Minutos;

        if (!jaNotificou)
        {
            VerificarNotificacoesBlocoNotas();
            VerificarNotificacoesPorHorario(currentHour, currentMinuto);
        }
        else
        {
            VerificarResetNotificacao(currentHour, currentMinuto);
        }
    }

    void VerificarNotificacoesBlocoNotas()
    {
        // Notificação 1
        if (ativador2.activeSelf && !blocodenotas[0] && !invokeChamado[0])
        {
            invokeChamado[0] = true;
            Invoke("blocodenotas1invoke", 3f);
        }

        // Notificação 2
        if (ativador3.activeSelf && !blocodenotas[1] && !invokeChamado[1])
        {
            invokeChamado[1] = true;
            Invoke("blocodenotas2invoke", 3f);
        }

        // Notificação 3
        if (ativador4.activeSelf && !blocodenotas[2] && !invokeChamado[2])
        {
            invokeChamado[2] = true;
            Invoke("blocodenotas3invoke", 3f);
        }

        // Notificação 4
        if (ativador5.activeSelf && !blocodenotas[3] && !invokeChamado[3])
        {
            invokeChamado[3] = true;
            Invoke("blocodenotas4invoke", 0f);
        }

        // Notificação 5
        if (ativador6.activeSelf && !blocodenotas[4] && !invokeChamado[4])
        {
            invokeChamado[4] = true;
            Invoke("blocodenotas5invoke", 0f);
        }
    }

    void VerificarNotificacoesPorHorario(float currentHour, float currentMinuto)
    {
        for (int i = 0; i < triggerHours.Length; i++)
        {
            if (Mathf.FloorToInt(currentMinuto) == Mathf.FloorToInt(triggerMinutes[i]) &&
                Mathf.FloorToInt(currentHour) == Mathf.FloorToInt(triggerHours[i]))
            {
                if (Celular.activeInHierarchy)
                {
                    notificacaoDentro.ShowNotification(dailyMessages[i], app[i], cor[i], logos[i]);
                }
                else
                {
                    notificacao.ShowNotification(dailyMessages[i], app[i], cor[i], logos[i]);
                }
                Debug.Log("Notificação por horário chamada");
                jaNotificou = true;
            }
        }
    }

    void VerificarResetNotificacao(float currentHour, float currentMinuto)
    {
        for (int i = 0; i < triggerHours.Length; i++)
        {
            if (Mathf.FloorToInt(currentMinuto) == Mathf.FloorToInt(triggerMinutes[i]) + 3f &&
                Mathf.FloorToInt(currentHour) == Mathf.FloorToInt(triggerHours[i]))
            {
                jaNotificou = false;
                notificacao.ResetNotification();
                notificacaoDentro.ResetNotification();
                Debug.Log("Notificações resetadas");
            }
        }
    }

    void blocodenotas1invoke()
    {
        if (mensagensBlocoDeNotas.Length > 0)
        {
            notificacao.ShowNotification(mensagensBlocoDeNotas[0], appBlocoDeNotas, corBlocoDeNotas, logoBlocoDeNotas);
            Debug.Log("notificação bloco de notas 1");
            blocodenotas[0] = true;
        }
    }

    void blocodenotas2invoke()
    {
        if (mensagensBlocoDeNotas.Length > 1)
        {
            notificacao.ShowNotification(mensagensBlocoDeNotas[1], appBlocoDeNotas, corBlocoDeNotas, logoBlocoDeNotas);
            Debug.Log("notificação bloco de notas 2");
            blocodenotas[1] = true;
        }
    }

    void blocodenotas3invoke()
    {
        if (mensagensBlocoDeNotas.Length > 2)
        {
            notificacao.ShowNotification(mensagensBlocoDeNotas[2], appBlocoDeNotas, corBlocoDeNotas, logoBlocoDeNotas);
            Debug.Log("notificação bloco de notas 3");
            blocodenotas[2] = true;
        }
    }

    void blocodenotas4invoke()
    {
        if (mensagensBlocoDeNotas.Length > 3)
        {
            notificacao.ShowNotification(mensagensBlocoDeNotas[3], appBlocoDeNotas, corBlocoDeNotas, logoBlocoDeNotas);
            Debug.Log("notificação bloco de notas 4");
            blocodenotas[3] = true;
        }
    }

    void blocodenotas5invoke()
    {
        if (mensagensBlocoDeNotas.Length > 4)
        {
            notificacao.ShowNotification(mensagensBlocoDeNotas[4], appBlocoDeNotas, corBlocoDeNotas, logoBlocoDeNotas);
            Debug.Log("notificação bloco de notas 5");
            blocodenotas[4] = true;
        }
    }
}
