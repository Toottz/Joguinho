using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineCaramelinho : MonoBehaviour
{
    public Sprite[] simbolos;             // Ícones do caramelo
    public Image[] slotImages;            // Slot1, Slot2, Slot3
    public float spinDuration = 1.5f;     // Tempo total de giro
    public float spinSpeed = 0.05f;       // Velocidade de troca dos símbolos
    public Button girarButton;
    public GameObject mensagemVitoria;
    public Color corVitoria = Color.yellow;
    public float tempoMensagem = 2f;
    public Text mensagemErro;
    public Color corErro = Color.red;

    private bool girando = false;

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
                        StartCoroutine(MostrarErro()); // <- Aqui está a chamada
                }
            }
        });
    }

    IEnumerator GirarSlots()
    {
        girando = true;
        girarButton.interactable = false;

        // Rodar cada slot separadamente com atraso entre eles
        for (int i = 0; i < slotImages.Length; i++)
        {
            StartCoroutine(GirarSlotIndividual(slotImages[i], spinDuration + (i * 0.3f)));
        }

        // Espera até todos pararem
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

        // Ao final, sorteia um símbolo final
        slot.sprite = simbolos[Random.Range(0, simbolos.Length)];
    }

    IEnumerator MostrarMensagemVitoria()
    {
        mensagemVitoria.SetActive(true);

        // Efeito de cor: piscar ou mudar temporariamente
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
            int recompensa = Random.Range(2, 11) * 5; // múltiplos de 5 entre 10 (2*5) e 50 (10*5)
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