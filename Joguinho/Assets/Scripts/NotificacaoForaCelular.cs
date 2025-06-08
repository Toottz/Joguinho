using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificacaoForaCelular : MonoBehaviour
{
    [Header("Configurações")]
    public float showDuration = 2f;
    public float fadeDuration = 0.5f;

    public Vector3 originalPosition;
    public Vector3 hiddenPosition;

    [Header("Componentes")]
    public TextMeshProUGUI Notificacao;
    public TextMeshProUGUI App;
    public RawImage Logo;
    public Image Cor;
    public AudioSource som;

    private Sequence animacao;

    void Awake()
    {
        transform.localPosition = hiddenPosition;
        gameObject.SetActive(false);
    }

    public void ShowNotification(string message, string app, Color cores, RenderTexture Logos)
    {
        // Prepara os conteúdos
        Notificacao.text = message;
        App.text = app;
        Cor.color = cores;
        Logo.texture = Logos;

        // Limpa animações anteriores
        if (animacao != null && animacao.IsActive())
        {
            animacao.Kill();
        }

        // Ativa o objeto
        gameObject.SetActive(true);

        // Cria nova animação
        animacao = DOTween.Sequence();

        // Animação de entrada
        animacao.Append(
            transform.DOLocalMove(originalPosition, fadeDuration)
            .SetEase(Ease.OutBack)
            .OnStart(() => som.Play())
        );

        // Tempo visível
        animacao.AppendInterval(showDuration);

        // Animação de saída
        animacao.Append(
            transform.DOLocalMove(hiddenPosition, fadeDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false))
            .OnComplete(() => som.Stop())
        );
    }

    public void ResetNotification()
    {
        if (animacao != null && animacao.IsActive())
        {
            animacao.Kill();
        }
        transform.localPosition = hiddenPosition;
        gameObject.SetActive(false);
    }
}