using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private TextMeshProUGUI popupText;

    [SerializeField] private Button attackUpgradeButton;
    [SerializeField] private TextMeshProUGUI attackPriceText;
    [SerializeField] private TextMeshProUGUI attackUpgradeAmountText;

    [SerializeField] private Button maxHPUpgradeButton;
    [SerializeField] private TextMeshProUGUI maxHPPriceText;
    [SerializeField] private TextMeshProUGUI maxHPUpgradeAmountText;

    [SerializeField] private Button defenseUpgradeButton;
    [SerializeField] private TextMeshProUGUI defensePriceText;
    [SerializeField] private TextMeshProUGUI defenseUpgradeAmountText;

    private PermanentUpgrade currentUpgrade;

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

        attackUpgradeButton.onClick.AddListener(() => UpgradeStat(StatType.Attack));
        maxHPUpgradeButton.onClick.AddListener(() => UpgradeStat(StatType.MaxHP));
        defenseUpgradeButton.onClick.AddListener(() => UpgradeStat(StatType.Defense));
    }

    public void ShowPopup(string message, PermanentUpgrade upgrade)
    {
        if (popup != null && popupText != null)
        {
            popupText.text = message;
            currentUpgrade = upgrade;

            if (currentUpgrade != null)
            {
                SetUpgradeInfo(currentUpgrade.attackUpgrade, attackPriceText, attackUpgradeAmountText);
                SetUpgradeInfo(currentUpgrade.maxHPUpgrade, maxHPPriceText, maxHPUpgradeAmountText);
                SetUpgradeInfo(currentUpgrade.defenseUpgrade, defensePriceText, defenseUpgradeAmountText);
            }

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

    private void UpgradeStat(StatType statType)
    {
        if (currentUpgrade != null)
        {
            currentUpgrade.TryUpgradeStat(statType);
        }
    }

    private void SetUpgradeInfo(StatUpgradeSO upgrade, TextMeshProUGUI priceText, TextMeshProUGUI upgradeAmountText)
    {
        if (upgrade != null)
        {
            priceText.text = $"Price: {upgrade.price} leaves";
            upgradeAmountText.text = $"Upgrade: +{upgrade.upgradeAmount}";
        }
        else
        {
            priceText.text = "N/A";
            upgradeAmountText.text = "N/A";
        }
    }
}
