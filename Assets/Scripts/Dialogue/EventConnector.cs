using System;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerActionType
{
    SpawnGameObject,
    DestroyGameObject
}

[Serializable]
public class TriggerEvent
{
    public GameObject triggerObject; // GameObject with the trigger collider
    public TriggerActionType actionType; // Type of action to perform
    public GameObject spawnObject; // GameObject to spawn or destroy
}

public class EventConnector : MonoBehaviour
{
    [SerializeField] private List<TriggerEvent> triggerEvents = new List<TriggerEvent>();

    private void Update()
    {
        // Check each trigger event in every frame
        foreach (TriggerEvent triggerEvent in triggerEvents)
        {
            // Perform action based on actionType
            switch (triggerEvent.actionType)
            {
                case TriggerActionType.SpawnGameObject:
                    if (triggerEvent.spawnObject != null && triggerEvent.triggerObject == null)
                    {
                        triggerEvent.spawnObject.SetActive(true); // Activate spawnObject if triggerObject is null
                    }
                    break;
                case TriggerActionType.DestroyGameObject:
                    if (triggerEvent.spawnObject != null && triggerEvent.triggerObject == null)
                    {
                        Destroy(triggerEvent.spawnObject); // Destroy spawnObject if triggerObject is null
                    }
                    break;
                default:
                    Debug.LogWarning("Unhandled TriggerActionType: " + triggerEvent.actionType);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Find and handle the trigger event
            foreach (TriggerEvent triggerEvent in triggerEvents)
            {
                if (triggerEvent.triggerObject == gameObject)
                {
                    // Perform action based on actionType
                    switch (triggerEvent.actionType)
                    {
                        case TriggerActionType.SpawnGameObject:
                            if (triggerEvent.spawnObject != null)
                            {
                                triggerEvent.spawnObject.SetActive(true); // Activate spawnObject
                            }
                            break;
                        case TriggerActionType.DestroyGameObject:
                            if (triggerEvent.spawnObject != null)
                            {
                                Destroy(triggerEvent.spawnObject); // Destroy spawnObject
                            }
                            break;
                        default:
                            Debug.LogWarning("Unhandled TriggerActionType: " + triggerEvent.actionType);
                            break;
                    }
                }
            }
        }
    }
}
