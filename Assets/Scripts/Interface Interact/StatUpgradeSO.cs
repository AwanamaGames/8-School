using UnityEngine;

public enum StatType { Attack, MaxHP, Defense }

[CreateAssetMenu(fileName = "New Stat Upgrade", menuName = "Stat Upgrade")]
public class StatUpgradeSO : ScriptableObject
{
    public string upgradeName;
    public StatType statType;
    public int price;
    public int upgradeAmount;
}
