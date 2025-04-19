using UnityEngine;
using TMPro;

public class MovableObject : MonoBehaviour, IInteractable, IMovable, IShowPrompt
{
    public GameObject movableObjectPivot;
    public GameObject pressEPrefab;
    public PressEPrompts promptData;
    private GameObject _promptInstance;
    private bool _isDrag = false;
    private TextMeshProUGUI _tmp;
    public void Interact(GameObject interactor)
    {
        _isDrag = !_isDrag;
        UpdatePromptText();
    }

    public bool CanShowPrompt()
    {
        return true;
    }
    public void SwitchPrompt()
    {
        if (_promptInstance != null && _tmp != null)
        {
            _tmp.text = _isDrag ? promptData.altPromptText : promptData.promptText[1];
        }
    }

    public void ShowPrompt()
    {
        Debug.Log("ShowPrompt called!");
        if (_promptInstance == null)
        {
            GameObject canvas = GameObject.Find("UI");
            if (canvas != null)
            {
                _promptInstance = Instantiate(pressEPrefab, canvas.transform);
                _tmp = _promptInstance.GetComponentInChildren<TextMeshProUGUI>();
            }
            else
            {
                Debug.Log("Canvas 'UI' not found!");
            }


        }
        if (_tmp != null)
            UpdatePromptText();
    }

    private void UpdatePromptText()
    {
        _tmp.text = _isDrag ? promptData.altPromptText : promptData.promptText[1];
    }

    public void HidePrompt()
    {
        if (_promptInstance != null && !_isDrag)
        {
            Destroy(_promptInstance);
            _promptInstance = null;
        }

    }

    public void MoveMe()
    {
        if (_isDrag)
        {
            transform.position = movableObjectPivot.transform.position;

            Vector3 dirToBox = (movableObjectPivot.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(-dirToBox);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); 
        }

    }

    private void Update()
    {
        MoveMe();
    }
}
