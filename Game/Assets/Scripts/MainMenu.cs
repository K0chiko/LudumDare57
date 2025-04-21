using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsButton()
    {

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
