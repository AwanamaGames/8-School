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
        if (GameManager.Instance.progressSO != null)
        {
            GameManager.Instance.progressSO.leaf = 0;
        }
        GameManager.Instance.gameplaySO.leaf = 0;
        GameManager.Instance.gameplaySO.currentHP = GameManager.Instance.defaultStatSO.currentHP;

        // Reload the current level or start a new game
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameState.NewGame);
        }
    }

    private void OnMainMenuButtonClicked()
    {
        if (GameManager.Instance.progressSO != null)
        {
            GameManager.Instance.progressSO.leaf = 0;
        }
        GameManager.Instance.gameplaySO.leaf = 0;
        GameManager.Instance.gameplaySO.currentHP = GameManager.Instance.defaultStatSO.currentHP;

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}
