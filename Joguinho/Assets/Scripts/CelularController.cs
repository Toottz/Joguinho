using System.Collections;
using UnityEngine;

public class CelularController : MonoBehaviour
{
    public GameObject celularUI;
    public MensagensController mensagensController;
    public MonoBehaviour cameraLookScript;
    private bool celularAberto = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AlarmManager alarme = FindObjectOfType<AlarmManager>();

            // Impede abrir ou fechar o celular durante o vídeo
            if (alarme != null && alarme.VideoEstaTocando())
            {
                Debug.Log("📵 Não pode abrir o celular enquanto o vídeo está tocando.");
                return;
            }

            // Impede fechar o celular enquanto o alarme está aguardando interação
            if (alarme != null && alarme.AlarmeEsperandoInteracao)
            {
                if (!celularUI.activeInHierarchy)
                {
                    celularUI.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }

                Debug.Log("📱 Alarme aguardando interação - celular mantido aberto.");
                return;
            }

            // Alterna o estado do celular
            celularAberto = !celularAberto;
            celularUI.SetActive(celularAberto);

            if (cameraLookScript != null && cameraLookScript is FirstPersonLook lookScript)
                lookScript.bloquearCamera = celularAberto;

            if (celularAberto)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mensagensController?.AoAbrirCelular();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mensagensController?.AoFecharCelular();
            }

            FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();
        }
    }

    void AbrirCelular()
    {
        celularAberto = true;
        if (celularUI != null)
            celularUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (cameraLookScript != null && cameraLookScript is FirstPersonLook lookScript)
            lookScript.bloquearCamera = true;

        if (mensagensController != null)
            mensagensController.AoAbrirCelular();

        Debug.Log("📱 Celular aberto.");
    }

    public void FecharCelular()
    {
        celularAberto = false;
        if (celularUI != null)
            celularUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraLookScript != null && cameraLookScript is FirstPersonLook lookScript)
            lookScript.bloquearCamera = false;

        if (mensagensController != null)
            mensagensController.AoFecharCelular();

        FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();

        Debug.Log("📴 Celular fechado.");
    }

    public bool CelularEstaAberto()
    {
        return celularAberto;
    }
}