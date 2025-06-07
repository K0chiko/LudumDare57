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

    public SettingsMenu settingsMenu;
    //public Shop shop;

    void Start()
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
        // Установить дефолтные значения
        settingsData = new SettingsData
        {
            selectedResolution = 0,  // например, индекс 0
            generalVolume = 0.8f,
            musicVolume = 0.6f,
            environmentVolume = 0.7f,
            sfxVolume = 0.7f
        };

        // Применить к меню
        settingsMenu.selectedResolution = settingsData.selectedResolution;
        settingsMenu.generalVolume = settingsData.generalVolume;
        settingsMenu.musicVolume = settingsData.musicVolume;
        settingsMenu.environmentVolume = settingsData.environmentVolume;
        settingsMenu.sfxVolume = settingsData.sfxVolume;

        // Сразу сохранить файл
        SaveSettings();

        Debug.Log("Settings file not found. Default settings created and saved.");
    }

    public void SaveSettings()
    {
        settingsData = new SettingsData();
        //settingsData.difficulty       = settingsMenu.difficulty;
        //settingsData.isFullscreen     = settingsMenu.isFullscreen;
        settingsData.selectedResolution = settingsMenu.selectedResolution;
        settingsData.generalVolume      = settingsMenu.generalVolume;
        settingsData.musicVolume        = settingsMenu.musicVolume;
        settingsData.environmentVolume  = settingsMenu.environmentVolume;
        settingsData.sfxVolume          = settingsMenu.sfxVolume;

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

            settingsMenu.selectedResolution = settingsData.selectedResolution;
            settingsMenu.generalVolume = settingsData.generalVolume;
            settingsMenu.musicVolume = settingsData.musicVolume;
            settingsMenu.environmentVolume = settingsData.environmentVolume;
            settingsMenu.sfxVolume = settingsData.sfxVolume;


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


