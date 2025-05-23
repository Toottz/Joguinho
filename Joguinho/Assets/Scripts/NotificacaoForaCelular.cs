using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class NotificacaoForaCelular : MonoBehaviour
{
    [Header("Configura��es")]
    public float showDuration = 2f;    // Tempo vis�vel
    public float fadeDuration = 0.5f;  // Dura��o da anima��o

    public Vector3 originalPosition;
    public Vector3 hiddenPosition;
    private bool isShowing = false;
    public TextMeshProUGUI Notificacao;
    public TextMeshProUGUI App;
    public Image Logo;
    public Image Cor;



    void Awake()
    {
        transform.localPosition = hiddenPosition; // Come�a escondido
    }


    // M�todo p�blico para ativar a notifica��o
    public void ShowNotification(string message, string app, Color cores, Sprite Logos)
    {
        gameObject.SetActive(true);

        if (isShowing) return;

        isShowing = true;
        Notificacao.text = message;
        App.text = app;
        Cor.color = cores;
        Logo.sprite = Logos;

        // Anima��o de entrada
        transform.DOLocalMove(originalPosition, fadeDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Anima��o de sa�da ap�s o tempo definido
                DOVirtual.DelayedCall(showDuration, () =>
                {
                    transform.DOLocalMove(hiddenPosition, fadeDuration)
                        .SetEase(Ease.InBack)
                        .OnComplete(() => isShowing = false)
                        .OnComplete(() => gameObject.SetActive(false));
                });
            });
    }

    // M�todo para resetar manualmente
    public void ResetNotification()
    {
        transform.DOKill();
        transform.localPosition = hiddenPosition;
        isShowing = false;
    }
}
