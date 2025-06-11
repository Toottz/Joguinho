using UnityEngine;

public class NPCInteracao : MonoBehaviour
{
    public GameObject mensagemPressioneF; // Texto "Aperte F para interagir"
    public GameObject painelDialogo; // Painel com legenda e botões
    public FirstPersonMovement movimentoJogador; // Script de movimento do jogador
    public FirstPersonLook cameraLookScript; // Script de rotação da câmera
    private bool jogadorPerto = false;
    private bool jaConversou = false;

    void Start()
    {
        if (mensagemPressioneF != null)
            mensagemPressioneF.SetActive(false);

        if (painelDialogo != null)
            painelDialogo.SetActive(false);
    }

    void Update()
    {
        if (jogadorPerto && !jaConversou && Input.GetKeyDown(KeyCode.F))
        {
            AbrirDialogo();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !jaConversou)
        {
            jogadorPerto = true;
            if (mensagemPressioneF != null)
                mensagemPressioneF.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            if (mensagemPressioneF != null)
                mensagemPressioneF.SetActive(false);
        }
    }

    void AbrirDialogo()
    {
        if (mensagemPressioneF != null)
            mensagemPressioneF.SetActive(false);

        if (painelDialogo != null)
            painelDialogo.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (movimentoJogador != null)
            movimentoJogador.enabled = false;

        if (cameraLookScript != null)
            cameraLookScript.bloquearCamera = true;
    }

    // Esta função deve ser chamada pelos botões do diálogo
    public void EncerrarDialogo()
    {
        if (painelDialogo != null)
            painelDialogo.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (movimentoJogador != null)
            movimentoJogador.enabled = true;

        if (cameraLookScript != null)
            cameraLookScript.bloquearCamera = false;

        jaConversou = true;
    }
}