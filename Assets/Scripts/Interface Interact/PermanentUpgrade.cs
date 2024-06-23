using UnityEngine;

public class PermanentUpgrade : MonoBehaviour, IInteractable
{
    [SerializeField] private SoundEffectDetailsSO soundEffectDetails;

    public StatUpgradeSO attackUpgrade;
    public StatUpgradeSO maxHPUpgrade;
    public StatUpgradeSO defenseUpgrade;

    [HideInInspector]
    public pStatManager player;

    private PopupManager popupManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<pStatManager>();
        popupManager = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PopupManager>();
    }

    public void Interact()
    {
        if (popupManager != null)
        {
            popupManager.ShowPopup("Choose an upgrade:", this);
        }
    }

    public void TryUpgradeStat(StatType statType)
    {
        StatUpgradeSO upgrade = null;

        switch (statType)
        {
            case StatType.Attack:
                upgrade = attackUpgrade;
                break;
            case StatType.MaxHP:
                upgrade = maxHPUpgrade;
                break;
            case StatType.Defense:
                upgrade = defenseUpgrade;
                break;
        }

        if (upgrade != null)
        {
            if (player.stat.leaf >= upgrade.price)
            {
                player.stat.leaf -= upgrade.price;
                ApplyUpgrade(upgrade);
                Debug.Log($"{upgrade.upgradeName} applied!");
            }
            else
            {
                if (soundEffectDetails.notEnoughLeafSoundEffect != null)
                {
                    SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.notEnoughLeafSoundEffect);
                }

                Debug.Log("Not enough leaves!");
            }
        }
    }

    private void ApplyUpgrade(StatUpgradeSO upgrade)
    {
        if (soundEffectDetails.statUpgradeSoundEffect != null)
        {
            SoundEffectManager.Instance.PlaySoundEffect(soundEffectDetails.statUpgradeSoundEffect);
        }

        switch (upgrade.statType)
        {
            case StatType.Attack:
                player.stat.att += upgrade.upgradeAmount;
                break;
            case StatType.MaxHP:
                player.stat.maxHP += upgrade.upgradeAmount;
                break;
            case StatType.Defense:
                player.stat.def += upgrade.upgradeAmount;
                break;
        }
    }
}
