using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textValue;
    public GameObject pressE;
    private GameManager _gameManager;
    private bool isPressE;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        textValue = GameObject.Find("Value").GetComponent<TextMeshProUGUI>();
        textValue.text = "Value: " + _gameManager.value;

    }


    private void PressEText(bool isPressE)
    {
        if (isPressE)
        {
            Debug.Log($"PressE: {isPressE}");
            pressE.SetActive(true);
        }
        else
        {
            Debug.Log($"PressE: {isPressE}");
            pressE.SetActive(false);
        }
            
    }
    // Update is called once per frame
    void Update()
    {
/*        isPressE = InteractiveObject.isPressE;
        if (isPressE)
        {
            pressE.SetActive(true);
        }
        else
        {
            pressE.SetActive(false);
        }*/
    }
}
