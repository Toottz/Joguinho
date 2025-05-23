using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class NotificacaoForaCelular : MonoBehaviour
{
    [Header("Configura��es")]
    public float showDuration = 2f;    // Tempo vis�vel
    public float fadeDuration = 0.5f;  // Dura��o da anima��o

    public Vector3 originalPosition;
    public Vector3 hiddenPosition;
    private bool isShowing = false;
    public TextMeshProUGUI TextMeshProUGUI;


    void Awake()
    {

        transform.position = hiddenPosition; // Come�a escondido
    }


    // M�todo p�blico para ativar a notifica��o
    public void ShowNotification(string message)
    {
        gameObject.SetActive(true);

        if (isShowing) return;

        isShowing = true;
        TextMeshProUGUI.text = message;

        // Anima��o de entrada
        transform.DOMove(originalPosition, fadeDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Anima��o de sa�da ap�s o tempo definido
                DOVirtual.DelayedCall(showDuration, () =>
                {
                    transform.DOMove(hiddenPosition, fadeDuration)
                        .SetEase(Ease.InBack)
                        .OnComplete(() => isShowing = false);
                });
            });
    }

    // M�todo para resetar manualmente
    public void ResetNotification()
    {
        transform.DOKill();
        transform.position = hiddenPosition;
        isShowing = false;
    }
}
