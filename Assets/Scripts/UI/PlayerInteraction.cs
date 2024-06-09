using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactionPrompt;
    private Interactable currentInteractable;

    private void Start()
    {
        interactionPrompt.gameObject.SetActive(false); // Hide prompt initially
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Interactable"))
        {
            interactionPrompt.gameObject.SetActive(true); // Show the prompt
            currentInteractable = coll.gameObject.GetComponent<Interactable>();
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Interactable") && Input.GetKeyDown(KeyCode.E))
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Interactable"))
        {
            interactionPrompt.gameObject.SetActive(false); // Hide the prompt when player leaves
            currentInteractable = null;
        }
    }
}
