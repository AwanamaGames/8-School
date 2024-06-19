using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public pStatManager playerStatManager;
    public GameObject itemIconPrefab; // Prefab for the item icon
    public Transform itemsPanel; // Panel to hold the item icons
    public ItemDatabase itemDatabase; // Reference to the item database

    private Dictionary<string, GameObject> itemIcons = new Dictionary<string, GameObject>();
    private HashSet<string> currentInventoryItems = new HashSet<string>(); // Track current items in inventory

    void Start()
    {
        if (playerStatManager == null)
        {
            Debug.LogError("PlayerStatManager is not assigned in the InventoryUI.");
        }

        UpdateUI();
    }

    void Update()
    {
        // Check if inventory has changed since last update
        if (InventoryChangedSinceLastUpdate())
        {
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        // Clear only icons that are no longer in the inventory
        HashSet<string> updatedItems = new HashSet<string>();
        foreach (ItemList item in playerStatManager.items)
        {
            updatedItems.Add(item.name);

            if (!itemIcons.ContainsKey(item.name))
            {
                GameObject newIcon = Instantiate(itemIconPrefab, itemsPanel);
                TextMeshProUGUI itemNameText = newIcon.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                Image itemImage = newIcon.transform.Find("ItemImage").GetComponent<Image>();

                itemNameText.text = item.name;

                ItemData itemData = itemDatabase.items.Find(i => i.itemName == item.name);
                if (itemData != null)
                {
                    itemImage.sprite = itemData.itemIcon;
                }

                itemIcons[item.name] = newIcon;
            }
        }

        // Remove icons of items that are no longer in inventory
        List<string> itemsToRemove = new List<string>();
        foreach (var itemIcon in itemIcons)
        {
            if (!updatedItems.Contains(itemIcon.Key))
            {
                Destroy(itemIcon.Value);
                itemsToRemove.Add(itemIcon.Key);
            }
        }

        // Clear removed items from the dictionary
        foreach (var key in itemsToRemove)
        {
            itemIcons.Remove(key);
        }

        // Update current inventory items
        currentInventoryItems = updatedItems;
    }

    bool InventoryChangedSinceLastUpdate()
    {
        HashSet<string> newItems = new HashSet<string>();
        foreach (ItemList item in playerStatManager.items)
        {
            newItems.Add(item.name);
        }

        return !newItems.SetEquals(currentInventoryItems);
    }
}
