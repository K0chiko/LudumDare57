using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    protected bool _isPauseGame;
    public GameObject pauseGameMenu;
    //public ThirdPersonController controller;
    //public Inputs inputs;
    protected virtual void ButtonResume()
    {
        pauseGameMenu.SetActive(false);
        ThirdPersonController.Instance.controlEnabled = true;
        Inputs.Instance.cursorLocked = true;
        Inputs.Instance.ApplyCursorState();
        _isPauseGame = false;
        Time.timeScale = 1f;
    }

    protected virtual void ButtonPause()
    {
        ThirdPersonController.Instance.controlEnabled = false;
        pauseGameMenu.SetActive(true);
        Inputs.Instance.cursorLocked = false;
        Inputs.Instance.ApplyCursorState();
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
