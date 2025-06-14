using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textValue;

    private OxygenController _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("Oxygen&saveloadController").GetComponent<OxygenController>();
        textValue = GameObject.Find("Value").GetComponent<TextMeshProUGUI>();
        textValue.text = "Value: " + _gameManager.value;

    }

}
