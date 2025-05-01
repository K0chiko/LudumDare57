using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    protected bool _isPauseGame;
    public GameObject pauseGameMenu;
    public ThirdPersonController controller;
    public Inputs inputs;
    protected virtual void ButtonResume()
    {
        pauseGameMenu.SetActive(false);
        controller.controlEnabled = true;
        inputs.cursorLocked = true;
        inputs.ApplyCursorState();
        _isPauseGame = false;
        Time.timeScale = 1f;
    }

    protected virtual void ButtonPause()
    {
        controller.controlEnabled = false;
        pauseGameMenu.SetActive(true);
        inputs.cursorLocked = false;
        inputs.ApplyCursorState();
        Time.timeScale = 0f;
        _isPauseGame = true;
    }

    protected virtual void ButtonRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((SceneManager.GetActiveScene().name));
    }

    protected virtual void ButtonExit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
