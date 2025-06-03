using System.IO;
using UnityEngine;
using UnityEngine.InputSystem.XR;


public class PlayerData
{
    public int value;
    public float oxygen;
    public Vector3 position;
}
public class JSONSaveLoad : MonoBehaviour
{
    PlayerData playerData;
    string saveFilePath;

    public OxygenController gameManager;
    private UIController _uiController;
    public GameObject player;
    public Shop shop;

    void Start()
    {
        playerData = new PlayerData();
        playerData.value = gameManager.value;
        playerData.oxygen = gameManager.oxygen;
        playerData.position = player.transform.position ;

        saveFilePath = Application.persistentDataPath + "/PlayerData.json";

        _uiController = GameObject.Find("UI").GetComponent<UIController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            SaveGame();

        if (Input.GetKeyDown(KeyCode.L))
            LoadGame();

        if (Input.GetKeyDown(KeyCode.O))
            DeleteSaveFile();
    }

    public void SaveGame()
    {
        playerData = new PlayerData();
        playerData.value = gameManager.value;
        playerData.oxygen = gameManager.oxygen;
        playerData.position = player.transform.position;

        shop.ButtonResume();

        saveFilePath = Application.persistentDataPath + "/PlayerData.json";

        string savePlayerData = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFilePath, savePlayerData);


        Debug.Log("Save file created at: " + saveFilePath);
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string loadPlayerData = File.ReadAllText(saveFilePath);
            playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

            if (player.TryGetComponent<CharacterController>(out var controller))
            {
                controller.enabled = false;
                player.transform.position = playerData.position;
                controller.enabled = true;
            }
            gameManager.value = playerData.value;
            _uiController.textValue.text = "Value: " + gameManager.value;
            gameManager.oxygen = playerData.oxygen;

            Debug.Log("Load game complete!: " + saveFilePath);
        }
        else Debug.Log("There is no save files to load!");
        
    }

    public void DeleteSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted!");
        }
        else
            Debug.Log("There is nothing to delete!");
    }
}


