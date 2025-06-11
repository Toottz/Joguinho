using UnityEngine;

public class MusicaController : MonoBehaviour
{
    private AudioSource audioSource;

    public float delayInicial = 10f; // tempo de espera em segundos

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke(nameof(TocarMusica), delayInicial);
    }

    void TocarMusica()
    {
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
