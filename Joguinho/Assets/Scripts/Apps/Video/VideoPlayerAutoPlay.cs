using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayerAutoPlay : MonoBehaviour
{
    public RawImage rawImage;
    public VideoClip videoClip;
    public RenderTexture renderTexture;
    public GameObject telaVideo; // Tela/objeto que contém o vídeo (ex: o painel do app)

    private VideoPlayer videoPlayer;
    private bool videoPreparado = false;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoClip == null || renderTexture == null || rawImage == null || telaVideo == null)
        {
            Debug.LogWarning("⚠️ Preencha todos os campos no VideoPlayerAutoPlay.");
            return;
        }

        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = true;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = renderTexture;
        videoPlayer.clip = videoClip;

        rawImage.texture = renderTexture;

        telaVideo.SetActive(false); // Esconde a tela até o vídeo estar pronto

        videoPlayer.Prepare();
        videoPlayer.prepareCompleted += OnVideoReady;
    }

    void OnVideoReady(VideoPlayer vp)
    {
        videoPreparado = true;
        telaVideo.SetActive(true);
        videoPlayer.Play();
    }

    // Se quiser iniciar por script externamente
    public void ReproduzirManual()
    {
        if (videoPreparado)
        {
            telaVideo.SetActive(true);
            videoPlayer.Play();
        }
    }
}
