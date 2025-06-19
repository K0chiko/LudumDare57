using TMPro;
using UnityEngine;

public class StoryText : PauseManager
{
    public TextMeshProUGUI hintText;

    [Tooltip("Что показывать, когда игрок входит в триггер")]
    [TextArea]
    public string messageKey;
    private bool isOnTrigger;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            string message = StoryTextManager.Instance.GetText(messageKey);
            hintText.text = message;
            isOnTrigger = true;
            ButtonPause();
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && isOnTrigger)
        {
            ButtonResume();
            gameObject.SetActive(false);
        }
    }

}
