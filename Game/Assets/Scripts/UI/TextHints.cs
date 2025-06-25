using TMPro;
using UnityEngine;

public class TextHints : MonoBehaviour
{
    public GameObject textObject;
    public TextMeshProUGUI hintText;

    [Tooltip("Что показывать, когда игрок входит в триггер")]
    [TextArea]
    public string message;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hintText.text = message;
            textObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            textObject.SetActive(false);
        }
    }

}

