using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public List<ResItem> resolutions = new List<ResItem>();
    public int selectedResolution;

    public TMP_Text resolutionLabel;

    [Space]
    public GameObject title;
    public GameObject mainMenuButtons;
    public GameObject optionsMenu;

    [Space]
    public Slider sliderAudioGeneral;
    public Slider sliderAudioSFX;
    public SFXController sfx;
    public Slider sliderAudioEnv;
    public AudioSource environmentAudioSource;
    public Slider sliderAudioMusic;
    public AudioSource musicAudioSource;


    [Tooltip("Файл настроек")]
    public JSONSettingsData settingsData;

    public float generalVolume;
    public float musicVolume;
    public float environmentVolume;
    public float sfxVolume;

    //public SFXController sfx;

    private void Start()
    {
        Screen.SetResolution(resolutions[1].horizontal, resolutions[1].vertical, true);
        sfx = FindFirstObjectByType<SFXController>();
    }

    public void ApplySettings()
    {
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, true);

        SetGeneralVolume();
        SetMusicVolume();
        SetEnvVolume();
        Debug.Log(sliderAudioEnv.value);
        sfx.ChangeVolume(sliderAudioEnv.value);
    }


    #region Graphics

    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
    }

    public void ResLeft()
    {
        selectedResolution --;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;              // взять эту хрень чтобы использовать в JSON
        }

        UpdateResolutionLabel();
    }

    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }

        UpdateResolutionLabel();
    }

    public void UpdateResolutionLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }

    public void BackButton()
    {
        title.SetActive(true);
        mainMenuButtons.SetActive(true);
        optionsMenu.SetActive(false);
    }

    #endregion

    #region Audio
    public void SetGeneralVolume()
    {
        settingsData.settingsMenu.generalVolume = sliderAudioGeneral.value;
        settingsData.SaveSettings();
        /*sfx.breatheSourceA.volume = sliderAudioGeneral.value;
        sfx.breatheSourceB.volume = sliderAudioGeneral.value;*/
    }

    public void SetMusicVolume()
    {
        settingsData.settingsMenu.musicVolume = sliderAudioMusic.value;
        settingsData.SaveSettings();
        //musicVolume = settingsData.settingsMenu.musicVolume;
        //musicAudioSource.volume = sliderAudioMusic.value;
    }

    public void SetEnvVolume()
    {
        settingsData.settingsMenu.environmentVolume = sliderAudioEnv.value;
        settingsData.SaveSettings();
        //environmentVolume = settingsData.settingsMenu.environmentVolume;
        //environmentAudioSource.volume = sliderAudioEnv.value;
    }

    public void SetEffectsVolume()
    {
        settingsData.settingsMenu.sfxVolume = sliderAudioSFX.value;
        settingsData.SaveSettings();
        //sfxVolume = settingsData.settingsMenu.sfxVolume;
        //environmentAudioSource.volume = sliderAudioEnv.value;
    }
    #endregion
}
