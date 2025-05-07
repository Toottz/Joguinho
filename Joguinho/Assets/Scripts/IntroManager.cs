using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public FirstPersonMovement movementScript;
    public FirstPersonLook lookScript;

    public CameraIntroMover cameraMover;
    public GameObject celularUI;
    public KeyCode toggleCelularKey = KeyCode.Tab;

    private bool alarmeDesligado = false;
    private bool celularAberto = false;

    void Start()
    {
        // Bloqueia movimento e câmera no começo
        if (movementScript != null) movementScript.enabled = false;
        if (lookScript != null) lookScript.enabled = false;
        celularUI.SetActive(false);
    }

    void Update()
    {
        if (!cameraMover || cameraMover.IsMoving())
            return;

        // Após movimento da câmera, habilita a rotação (olhar), mas não o movimento
        if (!alarmeDesligado && !lookScript.enabled)
            lookScript.enabled = true;

        if (Input.GetKeyDown(toggleCelularKey))
        {
            celularAberto = !celularAberto;
            celularUI.SetActive(celularAberto);

            if (celularAberto)
            {
                // Jogador abriu o celular para desligar o alarme
                alarmeDesligado = true;
            }
            else if (alarmeDesligado)
            {
                // Quando o celular é fechado após desligar o alarme, ativa o movimento
                if (movementScript != null) movementScript.enabled = true;
            }
        }
    }
}
