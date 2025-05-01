using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    protected bool _isPauseGame;
    public GameObject pauseGameMenu;
    public ThirdPersonController controller;
    public Inputs inputs;
    public virtual void ButtonResume()
    {
        pauseGameMenu.SetActive(false);
        controller.controlEnabled = true;
        inputs.cursorLocked = true;
        inputs.ApplyCursorState();
        _isPauseGame = false;
        Time.timeScale = 1f;
    }

    public virtual void ButtonPause()
    {
        controller.controlEnabled = false;
        pauseGameMenu.SetActive(true);
        inputs.cursorLocked = false;
        inputs.ApplyCursorState();
        Time.timeScale = 0f;
        _isPauseGame = true;
    }

    public virtual void ButtonRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene((SceneManager.GetActiveScene().name));
    }

    public virtual void ButtonExit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
