using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject title;
    public GameObject mainMenuButtons;
    public GameObject optionsMenu;

    [Space]
    [Tooltip("Файл настроек")]
    public JSONSettingsData settingsData;
    public SettingsMenu settingsMenu;
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsButton()
    {
        title.SetActive(false);
        mainMenuButtons.SetActive(false);
        optionsMenu.SetActive(true);

        settingsData.LoadSettings();
        settingsMenu.sliderAudioGeneral.value = settingsMenu.generalVolume;
        settingsMenu.sliderAudioMusic.value = settingsMenu.musicVolume;
        settingsMenu.sliderAudioEnv.value = settingsMenu.environmentVolume;
        settingsMenu.sliderAudioSFX.value = settingsMenu.sfxVolume;
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
