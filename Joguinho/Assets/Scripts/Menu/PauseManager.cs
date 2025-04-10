using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public MonoBehaviour cameraLookScript; 
    public GameObject celularUI; 

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (cameraLookScript != null)
            cameraLookScript.enabled = true;

        if (celularUI != null)
            celularUI.SetActive(true); 
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (cameraLookScript != null)
            cameraLookScript.enabled = false;

        if (celularUI != null)
            celularUI.SetActive(false); 
    }

    public void VoltarAoMenuPrincipal()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu"); 
    }


    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit(); 
    }
}
