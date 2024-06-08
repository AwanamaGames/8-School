using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public IInteractable interactable;

    void Start()
    {
        interactable = GetComponent<IInteractable>();
    }
    public void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            ///show interact UI, if IInteractable.price != show price

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }


    }
}
