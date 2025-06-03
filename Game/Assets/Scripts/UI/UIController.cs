using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textValue;

    private OxygenController _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<OxygenController>();
        textValue = GameObject.Find("Value").GetComponent<TextMeshProUGUI>();
        textValue.text = "Value: " + _gameManager.value;

    }

}
