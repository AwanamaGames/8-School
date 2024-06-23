using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionGate : MonoBehaviour
{
    private GameObject currentPlayer;

    [SerializeField] private TextMeshProUGUI interactPrompt;
    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;

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
                // Inform the GameManager to change the state to LevelComplete, which will handle the transition to the next level
                GameManager.Instance.ChangeState(GameState.LevelComplete);
            }
        }
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
