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

    private void Start()
    {
        Screen.SetResolution(resolutions[1].horizontal, resolutions[1].vertical, true);
    }

    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[_selectedResolution].horizontal, resolutions[_selectedResolution].vertical, true);
    }

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

    public void backButton()
    {
        title.SetActive(true);
        mainMenuButtons.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        // audioMixer.SetFloat("general", Mathf.Log10(volume - 80) * 20);
    }
}
