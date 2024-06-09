using UnityEngine;
using TMPro;

public class LeafCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leafText;
    int leaf;

    private void Start()
    {
        UpdateLeafText(leaf);
    }

    public void UpdateLeafText(int leaf)
    {
        leafText.text = "Leaves: " + leaf.ToString();
    }
}
