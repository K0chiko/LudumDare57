using UnityEngine;
using TMPro;

public class MovableObject : MonoBehaviour, IInteractable, IMovable, IShowPrompt
{
    public GameObject movableObjectPivot;
    public GameObject pressEPrefab;
    public PressEPrompts promptData;
    private GameObject _promptInstance;

    public void Interact(GameObject interactor)
    {

    }

    public void ShowPrompt()
    {
        if (_promptInstance == null)
        {
            GameObject canvas = GameObject.Find("UI");
            if (canvas != null)
            {
                TextMeshProUGUI tmp = _promptInstance.GetComponentInChildren<TextMeshProUGUI>();
                tmp.text = promptData.promptText[1];
                _promptInstance = Instantiate(pressEPrefab, canvas.transform);
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
            Debug.Log("Destroy!");
            _promptInstance = null;
        }
    }

    public void MoveMe()
    {
        transform.position = movableObjectPivot.transform.position;

        Vector3 dirToBox = (movableObjectPivot.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(-dirToBox);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }

    private void Update()
    {
        MoveMe();
    }
}
