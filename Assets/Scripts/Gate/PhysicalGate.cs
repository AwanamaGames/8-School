using TMPro;
using UnityEngine;

public class PhysicalGate : MonoBehaviour
{
    private GameObject currentPlayer;

    [SerializeField] private TextMeshProUGUI interactPrompt;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (currentPlayer != null)
            {
                Destroy(gameObject); // Destroy the gate when player interacts
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
