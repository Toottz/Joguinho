using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelularController : MonoBehaviour
{
    public GameObject celularUI;
    public GameObject menuApps; 
    public GameObject[] telasApps; 
    public GameObject celularImagem;
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

                // Volta para o menu principal ao abrir
                VoltarParaMenuApps();
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (cameraLookScript != null)
                    cameraLookScript.enabled = true;
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
    }

    public void VoltarParaMenuApps()
    {
        if (celularImagem != null)
            celularImagem.SetActive(true); // Garante que a imagem do celular esteja visível

        if (menuApps != null)
            menuApps.SetActive(true); // Reativa o menu de apps

        foreach (GameObject tela in telasApps)
        {
            if (tela != null)
                tela.SetActive(false); // Desativa todas as telas de apps
        }
    }
    public void AbrirAppCaramelinho()
    {
        if (menuApps != null)
            menuApps.SetActive(false);

        if (celularImagem != null)
            celularImagem.SetActive(false);

        foreach (GameObject tela in telasApps)
            if (tela != null)
                tela.SetActive(false);

        foreach (GameObject tela in telasApps)
            if (tela != null && tela.name == "TelaSlotCaramelinho") // nome exato da tela
            {
                tela.SetActive(true);
                break;
            }
    }
}
