using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.InputSystem.XR;

public class Pickup : MonoBehaviour, IInteractable, IShowPrompt, IDestroyable
{
    public int pickupValue;
    public int oxygenValue;

    public GameObject pressEPrefab;
    public PressEPrompts promptData;
    private GameObject _promptInstance;
    private OxygenController _gameManager;
    private UIController _uiController;
    void Start()
    {
        _uiController = GameObject.Find("UI").GetComponent<UIController>();
        _gameManager = GameObject.Find("Oxygen&saveloadController").GetComponent<OxygenController>();
    }



    public void Interact(GameObject interactor)
    {

    }

    public bool CanShowPrompt()
    {
        return true;
    }
    public void ShowPrompt()
    {
        if (_promptInstance == null)
        {
            GameObject canvas = GameObject.Find("UI");
            if (canvas != null)
            {
                _promptInstance = Instantiate(pressEPrefab, canvas.transform);
                TextMeshProUGUI tmp = _promptInstance.GetComponentInChildren<TextMeshProUGUI>();
                tmp.text = promptData.promptText[0];       
            }
            else
            {
                Debug.Log("Canvas 'UI' not found!");
            }
        }

    }

    public void HidePrompt()
    {
        if (_promptInstance != null)
        {
            Destroy(_promptInstance);
            _promptInstance = null;
        }
    }

    public void Destroy()
    {
        int value = _gameManager.value;
            value += pickupValue;
            _gameManager.value = value;
            _uiController.textValue.text = "Value: " + _gameManager.value;

        float oxygen = _gameManager.oxygen;
        oxygen += oxygenValue;
        _gameManager.oxygen = oxygen;
        _uiController.textOxygen.text = "Oxygen: " + _gameManager.oxygen;

        Destroy(gameObject);

    }
}
