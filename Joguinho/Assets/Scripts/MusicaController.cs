using UnityEngine;

public class MusicaController : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Configurações de Tempo")]
    public float delayInicial = 10f;           // Tempo para começar a tocar
    public float tempoDeDuracao = 0f;          // Tempo para parar a música (0 = toca até o fim)

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(TocarMusica), delayInicial);
    }

    void TocarMusica()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();

            // Se tempoDeDuracao for maior que 0, agendar parada
            if (tempoDeDuracao > 0f)
            {
                Invoke(nameof(PararMusica), tempoDeDuracao);
            }
        }
    }

    void PararMusica()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}