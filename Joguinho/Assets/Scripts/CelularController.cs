using System.Collections;
using System.Collections.Generic;
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
            celularAberto = !celularAberto;
            celularUI.SetActive(celularAberto);

            if (celularAberto)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (cameraLookScript != null)
                    cameraLookScript.enabled = false;

                // Garante que volta ao menu ao abrir
                FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (cameraLookScript != null)
                    cameraLookScript.enabled = true;

                // Garante que todos os apps são fechados
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

        if (cameraLookScript != null)
            cameraLookScript.enabled = true;

        if (mensagensController != null)
            mensagensController.AoFecharCelular();

        // Garante que volta ao menu e desativa tudo
        FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();
    }
}
