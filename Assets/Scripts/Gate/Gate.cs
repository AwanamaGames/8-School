using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private GameObject currentPlayer;

    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;
    [SerializeField] private Transform destination;
    [SerializeField] private TextMeshProUGUI interactPrompt;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (currentPlayer != null)
            {
                if (soundEffectDetails.gateSoundEffect != null)
                {
                    SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.gateSoundEffect);
                }

                currentPlayer.transform.position = destination.position;
            }
        }
    }

    public Transform GetDestination()
    {
        return destination;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            currentPlayer = collision.gameObject;
            interactPrompt.gameObject.SetActive(true); // Show the prompt
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactPrompt.gameObject.SetActive(false); // Hide the prompt

            if (collision.gameObject == currentPlayer)
            {
                currentPlayer = null;
            }
        }
    }
}
