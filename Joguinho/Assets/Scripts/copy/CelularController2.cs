using System.Collections;
using UnityEngine;

public class CelularController2 : MonoBehaviour
{
public GameObject celularUI;
public MensagensController mensagensController;
    public MonoBehaviour cameraLookScript; // Ex: FirstPersonLook
private bool celularAberto = false;
private FirstPersonLook lookScript;

void Start()
{
    // Garante que o script correto seja usado
    lookScript = cameraLookScript as FirstPersonLook;
}

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

            if (lookScript != null)
                lookScript.bloquearCamera = true;

            FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (lookScript != null)
                lookScript.bloquearCamera = false;

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

    if (lookScript != null)
        lookScript.bloquearCamera = false;

    if (mensagensController != null)
        mensagensController.AoFecharCelular();

    FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();
}
}