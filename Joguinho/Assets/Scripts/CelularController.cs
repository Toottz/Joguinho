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

        if (alarme != null)
        {
            if (alarme.VideoEstaTocando())
            {
                Debug.Log("📵 Não pode abrir o celular enquanto o vídeo está tocando.");
                return;
            }

            if (alarme.AlarmeEsperandoInteracao)
            {
                if (celularUI != null) celularUI.SetActive(true);
                return;
            }
        }

        celularAberto = !celularAberto;
        celularUI.SetActive(celularAberto);

        Cursor.lockState = celularAberto ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = celularAberto;

        if (cameraLookScript != null && cameraLookScript is FirstPersonLook lookScript)
            lookScript.bloquearCamera = false;

        FindObjectOfType<CelularUIManager>()?.VoltarAoMenu();

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