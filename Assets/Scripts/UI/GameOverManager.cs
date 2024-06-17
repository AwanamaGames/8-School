using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text leavesText;
    public Button continueButton;
    public Button mainMenuButton;

    private void Start()
    {
        // Display the current number of leaves
        if (GameManager.Instance != null && GameManager.Instance.gameplaySO != null)
        {
            leavesText.text = "Leaves you left: " + GameManager.Instance.gameplaySO.leaf.ToString();
        }
        else
        {
            leavesText.text = "Leaves you left: 0";
        }

        // Add listeners to the buttons
        continueButton.onClick.AddListener(OnContinueButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnContinueButtonClicked()
    {
        // Reload the current level
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameState.NewGame);
        }
    }

    private void OnMainMenuButtonClicked()
    {
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
