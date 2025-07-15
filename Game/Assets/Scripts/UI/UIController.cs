using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textOxygen;

    private OxygenController _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("Oxygen&saveloadController").GetComponent<OxygenController>();
        textValue = GameObject.Find("Value").GetComponent<TextMeshProUGUI>();
        textValue.text = "Value: " + _gameManager.value;

        textOxygen = GameObject.Find("Oxygen").GetComponent<TextMeshProUGUI>();
        textOxygen.text = "Oxygen: " + _gameManager.oxygen;

    }

    private void Update()
    {
        textOxygen = GameObject.Find("Oxygen").GetComponent<TextMeshProUGUI>();
        textOxygen.text = "Oxygen: " + _gameManager.oxygen;
    }

}
