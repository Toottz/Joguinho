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

        if (cameraLookScript != null)
            cameraLookScript.enabled = true;

        if (celularUI != null && celularUI.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
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

        // Reset de tempo
        Estatico.tempoEmMinutos = 360f;
        Estatico.Hora = 6;
        Estatico.Minutos = 0;

        // Reset de dinheiro
        Estatico.Dinheiro = 10f;

        // Reset de sistemas se existirem na cena
        HungerSystem hunger = FindObjectOfType<HungerSystem>();
        if (hunger != null) hunger.Resetar();

        SedeSystem sede = FindObjectOfType<SedeSystem>();
        if (sede != null) sede.Resetar();

        SonoSystem sono = FindObjectOfType<SonoSystem>();
        if (sono != null) sono.Resetar();

        AnsiedadeSystem ansiedade = FindObjectOfType<AnsiedadeSystem>();
        if (ansiedade != null) ansiedade.Resetar();

        PlayerWallet wallet = FindObjectOfType<PlayerWallet>();
        if (wallet != null) wallet.Resetar();

        // Carrega o menu principal
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}