using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AppConfiguracoes : MonoBehaviour
{
    public GameObject telaConfiguracoes;
    public GameObject[] outrasTelas;
    public int indiceAtual = 0;
    public VideoPlayer videoPlayer;
    public Slider sliderBrilhoCelular;
    public Slider sliderBrilhoJogo;
    public Slider sliderVolume;

    public Image escurecerCelular;

    [Header("Botões (Opcional)")]
    public Button botaoProximo;
    public Button botaoVoltar;

    // Persistência temporária
    private static AppConfiguracoes instancia;
    private float brilhoCelularTemp = 1f;
    private float volumeTemp = 1f;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AtualizarTelas();

        if (botaoProximo != null)
            botaoProximo.onClick.AddListener(ProximaTela);
        if (botaoVoltar != null)
            botaoVoltar.onClick.AddListener(VoltarTela);

        if (telaConfiguracoes != null)
            telaConfiguracoes.SetActive(false);

        sliderBrilhoCelular.onValueChanged.AddListener(AjustarBrilhoCelular);
        sliderVolume.onValueChanged.AddListener(AjustarVolume);

        // Aplica os valores mantidos
        sliderBrilhoCelular.value = brilhoCelularTemp;
        sliderVolume.value = volumeTemp;

        AjustarBrilhoCelular(brilhoCelularTemp);
        AjustarVolume(volumeTemp);
    }

    public void ProximaTela()
    {
        indiceAtual++;
        if (indiceAtual >= outrasTelas.Length)
            indiceAtual = 0;

        AtualizarTelas();
    }

    public void VoltarTela()
    {
        indiceAtual--;
        if (indiceAtual < 0)
            indiceAtual = outrasTelas.Length - 1;

        AtualizarTelas();
    }

    void AtualizarTelas()
    {
        for (int i = 0; i < outrasTelas.Length; i++)
        {
            outrasTelas[i].SetActive(i == indiceAtual);
        }
    }

    public void BotaoProximo() => ProximaTela();
    public void BotaoVoltar() => VoltarTela();

    public void AjustarBrilhoCelular(float valor)
    {
        brilhoCelularTemp = valor;

        if (escurecerCelular != null)
        {
            float alpha = 0.7f - Mathf.Clamp01(valor);
            var cor = escurecerCelular.color;
            cor.a = alpha;
            escurecerCelular.color = cor;
        }
    }

    public void AjustarVolume(float valor)
    {
        volumeTemp = valor;
        AudioListener.volume = valor;
    }

    public void AbrirTelaConfiguracoes()
    {
        if (!CelularEstaAberto())
        {
            Debug.Log("⛔ O celular precisa estar aberto para usar o app.");
            return;
        }
    }

    void FecharTodasAsTelas()
    {
        foreach (GameObject tela in outrasTelas)
        {
            if (tela != null)
                tela.SetActive(false);
        }
    }

    bool CelularEstaAberto()
    {
        CelularController celular = FindObjectOfType<CelularController>();
        return celular != null && celular.CelularEstaAberto();
    }
}