using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class NotificacaoForaCelular : MonoBehaviour
{
    [Header("Configurações")]
    public float showDuration = 2f;    // Tempo visível
    public float fadeDuration = 0.5f;  // Duração da animação

    public Vector3 originalPosition;
    public Vector3 hiddenPosition;
    private bool isShowing = false;
    public TextMeshProUGUI TextMeshProUGUI;


    void Awake()
    {

        transform.position = hiddenPosition; // Começa escondido
    }


    // Método público para ativar a notificação
    public void ShowNotification(string message)
    {
        gameObject.SetActive(true);

        if (isShowing) return;

        isShowing = true;
        TextMeshProUGUI.text = message;

        // Animação de entrada
        transform.DOMove(originalPosition, fadeDuration)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                // Animação de saída após o tempo definido
                DOVirtual.DelayedCall(showDuration, () =>
                {
                    transform.DOMove(hiddenPosition, fadeDuration)
                        .SetEase(Ease.InBack)
                        .OnComplete(() => isShowing = false);
                });
            });
    }

    // Método para resetar manualmente
    public void ResetNotification()
    {
        transform.DOKill();
        transform.position = hiddenPosition;
        isShowing = false;
    }
}
