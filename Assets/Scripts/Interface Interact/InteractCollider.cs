using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{
    private IInteractable interactable;
    private ItemPickUp itemPickUp;
    [SerializeField] private TextMeshProUGUI interactPrompt;

    void Start()
    {
        interactable = GetComponent<IInteractable>();
        itemPickUp = GetComponent<ItemPickUp>();
        if (interactable == null)
        {
            Debug.LogError("No IInteractable component found on this GameObject.");
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && interactable != null)
        {
            if (itemPickUp != null)
            {
                if (itemPickUp.price > 0)
                {
                    interactPrompt.text = $"Press E to interact (Price: {itemPickUp.price})";
                }
                else
                {
                    interactPrompt.text = "Press E to interact";
                }
            }
            else
            {
                interactPrompt.text = "Press E to interact";
            }

            interactPrompt.gameObject.SetActive(true); // Show the prompt

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            interactPrompt.gameObject.SetActive(false); // Hide the prompt when player leaves the trigger
        }
    }
}
