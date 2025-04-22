using System.Diagnostics.Contracts;
using StarterAssets;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private bool _isPauseGame;
    public GameObject pauseGameMenu;
    public ThirdPersonController controller;
    public Inputs inputs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (_isPauseGame) ButtonResume();
            else ButtonPause();
        }    
    }

    public void ButtonResume()
    {
        pauseGameMenu.SetActive(false);
        controller.controlEnabled = true;
        inputs.cursorLocked = true;
        inputs.ApplyCursorState();
        _isPauseGame = false;
        Time.timeScale = 1f;
    }

    public void ButtonPause()
    {
        controller.controlEnabled = false;
        pauseGameMenu.SetActive(true);
        inputs.cursorLocked = false;
        inputs.ApplyCursorState();
        Time.timeScale = 0f;
        _isPauseGame = true;
    }

    public void ButtonRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((SceneManager.GetActiveScene().name));
    }

    public void ButtonExit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
