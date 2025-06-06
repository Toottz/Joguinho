using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AppConfiguracoes : MonoBehaviour
{
public GameObject telaConfiguracoes;
public GameObject[] outrasTelas;
    public int indiceAtual = 0; // Começa na primeira tela
    public VideoPlayer videoPlayer;

public Slider sliderBrilhoCelular;
public Slider sliderBrilhoJogo;
public Slider sliderVolume;

public Image escurecerCelular;     // Imagem preta DENTRO do celular (escurece a tela do app)

    [Header("Botões (Opcional)")]
    public Button botaoProximo; // Arraste o botão "Próximo" aqui
    public Button botaoVoltar;  // Arraste o botão "Voltar" aqui

    void Start()
    {
        AtualizarTelas();

        // Configura os botões (se existirem)
        if (botaoProximo != null)
            botaoProximo.onClick.AddListener(ProximaTela);

        if (botaoVoltar != null)
            botaoVoltar.onClick.AddListener(VoltarTela);

        {
            if (telaConfiguracoes != null)
                telaConfiguracoes.SetActive(false);
        }

        sliderBrilhoCelular.onValueChanged.AddListener(AjustarBrilhoCelular);
        sliderVolume.onValueChanged.AddListener(AjustarVolume);

        // Valores padrão ao iniciar
        sliderBrilhoCelular.value = 1f;
        sliderVolume.value = AudioListener.volume;

        // Começa com brilho do celular total (sem escurecimento)
        if (escurecerCelular != null)
        {
            var cor = escurecerCelular.color;
            cor.a = 0f;
            escurecerCelular.color = cor;
        }
    }
    public void ProximaTela()
    {
        indiceAtual++;
        if (indiceAtual >= outrasTelas.Length)
            indiceAtual = 0; // Volta para a primeira tela se chegar no fim

        AtualizarTelas();
    }

    // Chamado pelo botão "Voltar"
    public void VoltarTela()
    {
        indiceAtual--;
        if (indiceAtual < 0)
            indiceAtual = outrasTelas.Length - 1; // Vai para a última tela se chegar no início

        AtualizarTelas();
    }

    // Ativa/desativa as telas conforme o índice
    void AtualizarTelas()
    {
        for (int i = 0; i < outrasTelas.Length; i++)
        {
            outrasTelas[i].SetActive(i == indiceAtual); // Ativa só a tela atual
        }
    }

    // Opcional: Métodos para chamar diretamente em eventos de UI
    public void BotaoProximo() => ProximaTela();
    public void BotaoVoltar() => VoltarTela();
void AjustarBrilhoCelular(float valor)
    {
        // 1 = sem escurecimento | 0 = escuro total
        if (escurecerCelular != null)
        {
            float alpha = 0.7f - Mathf.Clamp01(valor);
            var cor = escurecerCelular.color;
            cor.a = alpha;
            escurecerCelular.color = cor;
        }
    }

    void AjustarVolume(float valor)
    {
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

