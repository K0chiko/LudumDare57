using UnityEngine;

public class Interactor : MonoBehaviour
{
    private IInteractable currentInteractable;
    private IInteractable lastInteractable;

    private Shop _upgradeManager;

    private void DetectInteractable()
    {
        currentInteractable = null;
        Collider[] result = Physics.OverlapSphere(transform.position, 2f);
        if (result.Length > 0)
        {
            foreach (var collider in result)
            {
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

            if (currentInteractable is IShowPrompt newPrompt && CanShowPrompt(currentInteractable))
                newPrompt.ShowPrompt();

            lastInteractable = currentInteractable;
        }
    }
    private void Start()
    {
        GameObject gm = GameObject.Find("Rope");
        _upgradeManager = gm.GetComponent<Shop>();

    }
    void Update()
    {
        DetectInteractable();

        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable is IShowPrompt prompt)
                prompt.HidePrompt();

            if (currentInteractable is IDestroyable destroyable)
                destroyable.Destroy();

            if (currentInteractable is IMovable movable)
                movable.MoveMe();

            if (currentInteractable is IBreachable breachable)
                breachable.Breach();

            currentInteractable.Interact(gameObject);

            if (currentInteractable is IShowPrompt updatedPrompt && CanShowPrompt(currentInteractable))
                updatedPrompt.ShowPrompt();
        }
    }

    private bool CanShowPrompt(IInteractable obj)
    {
        // Если объект — BreachingMiniGame, не показываем подсказку во время взлома
        if (obj is BreachingMiniGame bmg)
            return !bmg.isBreaching;

        if (obj is Shop shop)
            return !_upgradeManager.isUpgrade;

        return true;
    }
}
