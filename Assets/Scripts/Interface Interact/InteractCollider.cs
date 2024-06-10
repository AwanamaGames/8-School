using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{
    private IInteractable interactable;

    void Start()
    {
        interactable = GetComponent<IInteractable>();
        if (interactable == null)
        {
            Debug.LogError("No IInteractable component found on this GameObject.");
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && interactable != null)
        {
            // Show interact UI, if IInteractable.price != 0 show price
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
    }
}
