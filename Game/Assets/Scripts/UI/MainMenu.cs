using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject title;
    public GameObject mainMenuButtons;
    public GameObject optionsMenu;
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsButton()
    {
        title.SetActive(false);
        mainMenuButtons.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void CreditsButton()
    {

    }
    public void AchievmentsButton()
    {

    }
    public void ExitButton()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
