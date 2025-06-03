using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public List<ResItem> resolutions = new List<ResItem>();
    private int _selectedResolution;

    public TMP_Text resolutionLabel;

    public GameObject title;
    public GameObject mainMenuButtons;
    public GameObject optionsMenu;

    public Slider sliderAudioGeneral;
    public SFXController sfx;
    public Slider sliderAudioEnv;
    public AudioSource environmentAudioSource;
    public Slider sliderAudioMusic;
    public AudioSource musicAudioSource;

    //public SFXController sfx;

    private void Start()
    {
        Screen.SetResolution(resolutions[1].horizontal, resolutions[1].vertical, true);
        sfx = FindFirstObjectByType<SFXController>();
    }

    public void ApplySettings()
    {
        Screen.SetResolution(resolutions[_selectedResolution].horizontal, resolutions[_selectedResolution].vertical, true);

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
        _selectedResolution --;
        if (_selectedResolution < 0)
        {
            _selectedResolution = 0;
        }

        UpdateResolutionLabel();
    }

    public void ResRight()
    {
        _selectedResolution++;
        if (_selectedResolution > resolutions.Count - 1)
        {
            _selectedResolution = resolutions.Count - 1;
        }

        UpdateResolutionLabel();
    }

    public void UpdateResolutionLabel()
    {
        resolutionLabel.text = resolutions[_selectedResolution].horizontal.ToString() + " x " + resolutions[_selectedResolution].vertical.ToString();
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
        sfx.sourceA.volume = sliderAudioGeneral.value;
        sfx.sourceB.volume = sliderAudioGeneral.value;
    }

    public void SetMusicVolume()
    {
        musicAudioSource.volume = sliderAudioMusic.value;
    }

    public void SetEnvVolume()
    {
        environmentAudioSource.volume = sliderAudioEnv.value;
    }
    #endregion
}
