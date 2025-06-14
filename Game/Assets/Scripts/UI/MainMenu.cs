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
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OptionsButton()
    {
        title.SetActive(false);
        mainMenuButtons.SetActive(false);
        optionsMenu.SetActive(true);

        settingsData.LoadSettings();
/*        settingsMenu.sliderAudioGeneral.value = settingsData.generalVolume;
        settingsMenu.sliderAudioMusic.value = settingsData.musicVolume;
        settingsMenu.sliderAudioEnv.value = settingsData.environmentVolume;
        settingsMenu.sliderAudioSFX.value = settingsData.sfxVolume;*/
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
    }
    public void CreditsButton()
    {
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
    }
    public void AchievmentsButton()
    {
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
    }
    public void ExitButton()
    {
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
        Debug.Log("Quit!");
        Application.Quit();
    }
}
