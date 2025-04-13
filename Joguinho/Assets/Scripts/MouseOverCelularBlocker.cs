using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverCelularBlocker : MonoBehaviour
{
    public FirstPersonLook cameraLookScript;
    public CelularController celularController; // Referência ao script que controla o celular

    void Update()
    {
        if (celularController != null && celularController.CelularEstaAberto())
        {
            if (EventSystem.current.IsPointerOverGameObject())
                cameraLookScript.bloquearCamera = true;
            else
                cameraLookScript.bloquearCamera = false;
        }
        else
        {
            cameraLookScript.bloquearCamera = false;
        }
    }
}
