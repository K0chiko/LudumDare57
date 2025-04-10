using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textValue;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textValue = GameObject.Find("Value").GetComponent<TextMeshProUGUI>();
        textValue.text = "Value: " + gameManager.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
