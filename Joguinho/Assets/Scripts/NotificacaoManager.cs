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
    public string[] app; // Mensagens para cada horário
    public Color[] cor;
    public Sprite[] logos;
    public float[] triggerHours;
    public float[] triggerMinutes;

    [Header("Triggers mensagens especificas")]
    public GameObject Alarme;

    private bool alarmetocou = false;
    private bool jaNotificou;
    private bool notificacaomae=false;

    [Header("Mensagem Bom dia")]
    [TextArea]
    public string mensagemBomDia;
    [TextArea]
    public string appBomDia;
    public Color corBomDia;
    public Sprite logoBomDia;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentHour = TimeManager.HoraAtual;
        float currentMinuto = TimeManager.MinutoAtual;


        if (!jaNotificou)
        {
            if (Alarme.activeInHierarchy && !alarmetocou)
            {
                notificacaoDentro.ShowNotification(mensagemBomDia, appBomDia, corBomDia, logoBomDia);
                jaNotificou = true;
                alarmetocou = true;
            }

            for (int i = 0; i < triggerHours.Length; i++)
            {

                if (Mathf.FloorToInt(currentMinuto) == Mathf.FloorToInt(triggerMinutes[i]))
                {
                    if (Mathf.FloorToInt(currentHour) == Mathf.FloorToInt(triggerHours[i]))
                    {
                        if (Celular.activeInHierarchy)
                        {
                            notificacaoDentro.ShowNotification(dailyMessages[i], app[i], cor[i], logos[i]);
                        }
                        else
                        {
                            notificacao.ShowNotification(dailyMessages[i], app[i], cor[i], logos[i]);
                        }
                        Debug.Log("Notificacao chamada");
                        jaNotificou = true;
                    }
                }
            }
        }

        if (jaNotificou)
        {
            if (alarmetocou && !notificacaomae)
            {
                DOVirtual.DelayedCall(3f, () =>
                { // 2 segundos de delay
                    jaNotificou = false;
                    notificacao.ResetNotification();
                    notificacaoDentro.ResetNotification();
                    Debug.Log("Ja pode notificar de novo");
                });
                notificacaomae = true;
            }

            for (int i = 0;i < triggerHours.Length; i++)
            {
                if (Mathf.FloorToInt(currentMinuto) == Mathf.FloorToInt(triggerMinutes[i]) + 3f)
                {
                    if (Mathf.FloorToInt(currentHour) == Mathf.FloorToInt(triggerHours[i]))
                    {
                        jaNotificou = false;
                        notificacao.ResetNotification();
                        notificacaoDentro.ResetNotification();
                        Debug.Log("Ja pode notificar de novo");
                    }
                }
            }
        }
    }
}
