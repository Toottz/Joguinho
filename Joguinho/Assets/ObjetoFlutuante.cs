using UnityEngine;
using DG.Tweening;

public class ObjetoFlutuante : MonoBehaviour
{
    [Header("Configurações")]
    public float alturaSubida = 10f;    // Altura em unidades locais
    public float duracaoSubida = 1f;    // Tempo para subir
    public float duracaoDescida = 1f;   // Tempo para descer
    public Ease easeSubida = Ease.OutSine;
    public Ease easeDescida = Ease.InSine;

    private Vector3 posicaoLocalInicial;
    private Sequence animacao;
    private int ciclosCompletos = 0;
    private const int ciclosDesejados = 4; // 4 subidas+descidas

    void Awake()
    {
        posicaoLocalInicial = transform.localPosition;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        IniciarAnimacao();
    }

    void OnDisable()
    {
        PararAnimacao();
        transform.localPosition = posicaoLocalInicial;
        ciclosCompletos = 0; // Reseta ao desligar
    }

    void IniciarAnimacao()
    {
        // Prepara a animação
        PararAnimacao();
        transform.localPosition = posicaoLocalInicial;

        animacao = DOTween.Sequence();

        // Ciclo completo (subir + descer)
        animacao.Append(
            transform.DOLocalMoveY(posicaoLocalInicial.y + alturaSubida, duracaoSubida)
            .SetEase(easeSubida)
            .OnComplete(ContarCiclo)
        );
        animacao.Append(
            transform.DOLocalMoveY(posicaoLocalInicial.y, duracaoDescida)
            .SetEase(easeDescida)
            .OnComplete(ContarCiclo)
        );

        // Repete o ciclo completo 4 vezes
        animacao.SetLoops(ciclosDesejados * 1); // Multiplica por 2 (subida+descida = 1 ciclo)
        animacao.OnComplete(() => gameObject.SetActive(false));
    }

    void ContarCiclo()
    {
        ciclosCompletos++;
        if (ciclosCompletos >= ciclosDesejados * 2) // 2 movimentos por ciclo
        {
            ciclosCompletos = 0;
        }
    }

    void PararAnimacao()
    {
        if (animacao != null && animacao.IsActive())
        {
            animacao.Kill();
        }
    }

    // Método público para controle
    public void Ativar()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            // Reinicia se já estiver ativo
            PararAnimacao();
            IniciarAnimacao();
        }
    }
}