using UnityEngine;
using TMPro;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI popupText;

    private void Start()
    {
        if (popup != null)
        {
            popup.SetActive(false); // Ensure the popup is hidden initially
        }
        else
        {
            Debug.LogError("Popup GameObject is not assigned!");
        }

        if (popupText == null)
        {
            Debug.LogError("Popup TextMeshProUGUI is not assigned!");
        }
    }

    public void ShowPopup(string message)
    {
        if (popup != null && popupText != null)
        {
            popupText.text = message;
            popup.SetActive(true); // Show the popup
        }
    }

    public void HidePopup()
    {
        if (popup != null)
        {
            popup.SetActive(false); // Hide the popup
        }
    }

    public bool IsPopupActive()
    {
        return popup != null && popup.activeSelf;
    }
}
