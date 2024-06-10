using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactPrompt;
    [SerializeField] private PopupManager popupManager;
    private IInteractable currentInteractable;

    private void Start()
    {
        if (interactPrompt == null)
        {
            Debug.LogError("InteractPrompt reference is missing!");
        }

        if (popupManager == null)
        {
            Debug.LogError("PopupManager reference is missing!");
        }

        interactPrompt.gameObject.SetActive(false); // Hide the prompt initially
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Interactable") && !popupManager.IsPopupActive())
        {
            currentInteractable = coll.gameObject.GetComponent<IInteractable>();
            if (currentInteractable != null)
            {
                interactPrompt.gameObject.SetActive(true); // Show the prompt
            }
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (currentInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            interactPrompt.gameObject.SetActive(false); // Hide the prompt
            currentInteractable.Interact();
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Interactable"))
        {
            interactPrompt.gameObject.SetActive(false); // Hide the prompt
            currentInteractable = null;
        }
    }
}
