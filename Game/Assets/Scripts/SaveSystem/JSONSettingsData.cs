using System.IO;
using UnityEngine;
using UnityEngine.InputSystem.XR;


public class SettingsData
{
    public int difficulty;

    public bool isFullscreen;
    public int selectedResolution;

    public float generalVolume;
    public float musicVolume;
    public float environmentVolume;
    public float sfxVolume;   
}
public class JSONSettingsData : MonoBehaviour
{
    SettingsData settingsData;
    string saveFilePath;

    public bool isFullscreen;
    public int  selectedResolution;

    public float generalVolume;
    public float musicVolume;
    public float environmentVolume;
    public float sfxVolume;

    //public SettingsMenu settingsMenu;
    //public Shop shop;

    void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/SettingsData.json";

        if (File.Exists(saveFilePath))
        {
            LoadSettings();
        }
        else
        {
            SetDefaultSettings();
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            SaveSettings();

        if (Input.GetKeyDown(KeyCode.L))
            LoadSettings();

        if (Input.GetKeyDown(KeyCode.O))
            DeleteSaveFile();
    }

    public void SetDefaultSettings()
    {
        settingsData = new SettingsData
        {
            selectedResolution = 0, 
            generalVolume = 1f,
            musicVolume = 0.7f,
            environmentVolume = 70f,
            sfxVolume = 70f
        };


        selectedResolution  = settingsData.selectedResolution;
        generalVolume       = settingsData.generalVolume;
        musicVolume         = settingsData.musicVolume;
        environmentVolume   = settingsData.environmentVolume;
        sfxVolume           = settingsData.sfxVolume;

        SaveSettings();

        Debug.Log("Settings file not found. Default settings created and saved.");
    }

    public void SaveSettings()
    {
        settingsData = new SettingsData();
        //settingsData.difficulty       = settingsMenu.difficulty;
        //settingsData.isFullscreen     = settingsMenu.isFullscreen;
        settingsData.selectedResolution = selectedResolution;
        settingsData.generalVolume      = generalVolume;
        settingsData.musicVolume        = musicVolume;
        settingsData.environmentVolume  = environmentVolume;
        settingsData.sfxVolume          = sfxVolume;

        //shop.ButtonResume();

        saveFilePath = Application.persistentDataPath + "/SettingsData.json";

        string saveSettingsData = JsonUtility.ToJson(settingsData, true);
        File.WriteAllText(saveFilePath, saveSettingsData);


        Debug.Log("Settings file created at: " + saveFilePath);
    }

    public void LoadSettings()
    {
        if (File.Exists(saveFilePath))
        {
            string loadSettingsData = File.ReadAllText(saveFilePath);
            settingsData = JsonUtility.FromJson<SettingsData>(loadSettingsData);

            selectedResolution = settingsData.selectedResolution;
            generalVolume      = settingsData.generalVolume;
            musicVolume        = settingsData.musicVolume;
            environmentVolume  = settingsData.environmentVolume;
            sfxVolume          = settingsData.sfxVolume;


            Debug.Log("Load settings complete!: " + saveFilePath);
        }
        else Debug.Log("There is no setiings files to load!");
        
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Settings file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }
}


