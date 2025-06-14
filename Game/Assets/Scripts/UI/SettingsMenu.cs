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
    public Slider sliderAudioEnv;
    public AudioSource environmentAudioSource;
    public Slider sliderAudioMusic;
    public AudioSource musicAudioSource;
    public AudioMixer audioMixer;


    [Tooltip("Файл настроек")]
    public JSONSettingsData settingsData;

    [Space]
    [Tooltip("Для дебага")]
    private float _generalVolume;
    private float _musicVolume;
    private float _environmentVolume;
    private float _sfxVolume;


    private void Start()
    {
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
        Screen.SetResolution(resolutions[1].horizontal, resolutions[1].vertical, true);
    }

    public void ApplySettings()
    {
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, true);


        SetMusicVolume();
        SetEnvVolume();
        SetEffectsVolume();

    }


    #region Graphics

    [System.Serializable]
    public class ResItem
    {
        public int horizontal, vertical;
    }

    public void ResLeft()
    {
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
        selectedResolution --;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;              // взять эту хрень чтобы использовать в JSON
        }

        UpdateResolutionLabel();
    }

    public void ResRight()
    {
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
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
        SoundManager.PlaySound(SoundType.BUTTONCLICK);
        title.SetActive(true);
        mainMenuButtons.SetActive(true);
        optionsMenu.SetActive(false);
    }

    #endregion

    #region Audio
    public void SetGeneralVolume()
    {
        settingsData.generalVolume = sliderAudioGeneral.value;
        settingsData.SaveSettings();
        audioMixer.SetFloat("General", Mathf.Log10(settingsData.generalVolume) * 20);
        //audioMixer.SetFloat("Music", Mathf.Log10(sliderAudioGeneral.value) * 20);
        /*        sfx.breatheSourceA.volume = sliderAudioGeneral.value;
                sfx.breatheSourceB.volume = sliderAudioGeneral.value;*/
    }

    public void SetMusicVolume()
    {
        settingsData.musicVolume = sliderAudioMusic.value;
        settingsData.SaveSettings();
        audioMixer.SetFloat("Music", Mathf.Log10(settingsData.musicVolume) * 20);
        //musicVolume = settingsData.settingsMenu.musicVolume;
        //musicAudioSource.volume = sliderAudioMusic.value;
    }

    public void SetEnvVolume()
    {
        settingsData.environmentVolume = sliderAudioEnv.value;
        settingsData.SaveSettings();
        audioMixer.SetFloat("Environment", Mathf.Log10(settingsData.environmentVolume) * 20);
        //environmentVolume = settingsData.settingsMenu.environmentVolume;
        //environmentAudioSource.volume = sliderAudioEnv.value;
    }

    public void SetEffectsVolume()
    {
        settingsData.sfxVolume = sliderAudioSFX.value;
        settingsData.SaveSettings();
        audioMixer.SetFloat("Effects", Mathf.Log10(settingsData.sfxVolume) * 20);
        //sfxVolume = settingsData.settingsMenu.sfxVolume;
        //environmentAudioSource.volume = sliderAudioEnv.value;
    }
    #endregion
}
