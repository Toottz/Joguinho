using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineCaramelinho : MonoBehaviour
{
    public Sprite[] simbolos;
    public Image[] slotImages;
    public float spinDuration = 1.5f;
    public float spinSpeed = 0.05f;
    public Button girarButton;
    public GameObject mensagemVitoria;
    public Color corVitoria = Color.yellow;
    public float tempoMensagem = 2f;
    public Text mensagemErro;
    public Color corErro = Color.red;
    public AudioSource somGirar;
    public AudioSource somVitoria;

    private bool girando = false;
    private Coroutine erroCoroutine; // ← armazena referência da coroutine de erro

    void OnDisable()
    {
        // Sempre que a tela do app for desativada, esconde as mensagens
        if (mensagemErro != null)
            mensagemErro.gameObject.SetActive(false);

        if (mensagemVitoria != null)
            mensagemVitoria.SetActive(false);

        if (erroCoroutine != null)
        {
            StopCoroutine(erroCoroutine);
            erroCoroutine = null;
        }
    }

    void Start()
    {
        if (mensagemVitoria != null)
            mensagemVitoria.SetActive(false);

        if (mensagemErro != null)
            mensagemErro.gameObject.SetActive(false);

        girarButton.onClick.AddListener(() =>
        {
            if (!girando)
            {
                if (PlayerWallet.Instance.GastarDinheiro(5f))
                {
                    StartCoroutine(GirarSlots());
                }
                else
                {
                    Debug.Log("💸 Saldo insuficiente para girar o Caramelinho!");

                    if (mensagemErro != null)
                    {
                        if (erroCoroutine != null)
                            StopCoroutine(erroCoroutine); // Para erro anterior, se houver

                        erroCoroutine = StartCoroutine(MostrarErro());
                    }
                }
            }
        });
    }

    IEnumerator GirarSlots()
    {
        girando = true;
        girarButton.interactable = false;

        if (somGirar != null)
            somGirar.Play();

        for (int i = 0; i < slotImages.Length; i++)
        {
            StartCoroutine(GirarSlotIndividual(slotImages[i], spinDuration + (i * 0.3f)));
        }

        yield return new WaitForSeconds(spinDuration + 1f);

        girando = false;
        girarButton.interactable = true;

        VerificarResultado();
    }

    IEnumerator GirarSlotIndividual(Image slot, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            slot.sprite = simbolos[Random.Range(0, simbolos.Length)];
            timer += spinSpeed;
            yield return new WaitForSeconds(spinSpeed);
        }

        slot.sprite = simbolos[Random.Range(0, simbolos.Length)];
    }

    IEnumerator MostrarMensagemVitoria()
    {
        if (somVitoria != null)
            somVitoria.Play();

        mensagemVitoria.SetActive(true);
        Color originalColor = mensagemVitoria.GetComponent<Text>().color;
        mensagemVitoria.GetComponent<Text>().color = corVitoria;

        yield return new WaitForSeconds(tempoMensagem);

        mensagemVitoria.SetActive(false);
        mensagemVitoria.GetComponent<Text>().color = originalColor;
    }

    IEnumerator MostrarErro()
    {
        mensagemErro.gameObject.SetActive(true);

        Color originalColor = girarButton.image.color;
        girarButton.image.color = corErro;

        yield return new WaitForSeconds(0.3f);
        girarButton.image.color = originalColor;

        yield return new WaitForSeconds(1.5f);
        mensagemErro.gameObject.SetActive(false);

        erroCoroutine = null;
    }

    void VerificarResultado()
    {
        Sprite primeiro = slotImages[0].sprite;
        bool todosIguais = true;

        for (int i = 1; i < slotImages.Length; i++)
        {
            if (slotImages[i].sprite != primeiro)
            {
                todosIguais = false;
                break;
            }
        }

        if (todosIguais)
        {
            int recompensa = Random.Range(2, 11) * 5;
            Debug.Log($"🎉 Você ganhou R$ {recompensa} com o Caramelinho!");
            PlayerWallet.Instance.AdicionarDinheiro(recompensa);

            if (mensagemVitoria != null)
                StartCoroutine(MostrarMensagemVitoria());
        }
        else
        {
            Debug.Log("🐶 Não foi dessa vez...");
        }
    }
}
