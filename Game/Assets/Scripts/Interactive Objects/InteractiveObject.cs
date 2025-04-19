using UnityEngine;

public class InteractiveObject : MonoBehaviour, IInteractable//, IShowPrompt
{
/*    public GameObject pressEPrefab;
    private GameObject _promptInstance;*/

/*    public void ShowPrompt() 
    {
        if (_promptInstance == null)
        {
            GameObject canvas = GameObject.Find("UI");
            if (canvas != null)
            {
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
    }*/
    public void Interact(GameObject interactor)
    {
        //Destroy(gameObject);
    }
}
