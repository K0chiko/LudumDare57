using UnityEngine;

public class Interactor : MonoBehaviour
{
    private IInteractable currentInteractable;
    private IInteractable lastInteractable;
    private void DetectInteractable()
    {
        currentInteractable = null;
        Collider[] result = Physics.OverlapSphere(transform.position, 2f);
        if (result.Length > 0)
        {
            foreach(var collider in result) {
                IInteractable[] interactable = collider.GetComponents<IInteractable>();
                if (interactable.Length > 0)
                {
                    currentInteractable = interactable[0];
                    break;
                }
            }
        }

        if (currentInteractable != lastInteractable)
        {
            if (lastInteractable is IShowPrompt oldPrompt)
                oldPrompt.HidePrompt();

            if (currentInteractable is IShowPrompt newPrompt)
                newPrompt.ShowPrompt();

            lastInteractable = currentInteractable;
        }

    }





void Update()
    {
        Debug.Log("cInt " + currentInteractable);
        DetectInteractable();
        if (currentInteractable != null && Input.GetKey(KeyCode.E))
        {
            if (currentInteractable is IShowPrompt prompt)
                prompt.HidePrompt();
            currentInteractable.Interact(gameObject);
            currentInteractable = null;
        }



    }
}
