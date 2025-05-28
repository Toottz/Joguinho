using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

public class MouseOverCelularBlocker : MonoBehaviour
{
    public FirstPersonLook cameraLookScript;
    public CelularController celularController; // Referência ao script que controla o celular

    [SerializeField]
    private GameObject UiCelular;

    void Update()
    {
        if (celularController != null && UiCelular.activeSelf)
        {
                cameraLookScript.bloquearCamera = true;
        }
        else
        {
            cameraLookScript.bloquearCamera = false;
        }
    }

    public bool MouseScreenCheck()
    {
#if UNITY_EDITOR
        if (Input.mousePosition.x < 1 || Input.mousePosition.y < 1 || Input.mousePosition.x >= Handles.GetMainGameViewSize().x - 1 || Input.mousePosition.y >= Handles.GetMainGameViewSize().y - 1){
        return false;
        }
#else
        if (Input.mousePosition.x < 1 || Input.mousePosition.y < 1 || Input.mousePosition.x >= Screen.width - 1 || Input.mousePosition.y >= Screen.height - 1)
        {
            return false;
        }
#endif
        else
        {
            return true;
        }
    }
}
