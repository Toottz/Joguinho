using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelularController : MonoBehaviour
{
    public GameObject celularUI;
    public MensagensController mensagensController;
    public MonoBehaviour cameraLookScript; // Ex: FirstPersonLook

    private bool celularAberto = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            celularAberto = !celularAberto;
            celularUI.SetActive(celularAberto);

            if (celularAberto)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Em vez de desativar o script, apenas bloqueia a câmera
                if (cameraLookScript != null && cameraLookScript is FirstPersonLook lookScript)
                    lookScript.bloquearCamera = false; // Deixa o mouse controlar fora do celular
                

                FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (cameraLookScript != null && cameraLookScript is FirstPersonLook lookScript)
                    lookScript.bloquearCamera = false; // Garante que a câmera volte ao normal

                FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();
            }

            if (mensagensController != null)
            {
                if (celularAberto)
                    mensagensController.AoAbrirCelular();
                else
                    mensagensController.AoFecharCelular();
            }
        }
    }

    public void FecharCelular()
    {
        celularAberto = false;
        celularUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraLookScript != null && cameraLookScript is FirstPersonLook lookScript)
            lookScript.bloquearCamera = false;

        if (mensagensController != null)
            mensagensController.AoFecharCelular();

        FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();
    }

    public bool CelularEstaAberto()
    {
    return celularAberto;
    }
}
