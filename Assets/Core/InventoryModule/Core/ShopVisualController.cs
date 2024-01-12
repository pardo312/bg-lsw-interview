using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopVisualController : MonoBehaviour
{
    public InventoryItemVisualController itemPrefab;
    public Transform inventoryGrid;
    public GameObject inventoryPanelParent;

    [Header("Fields")]
    private List<InventoryItemVisualController> shopVisualItems = new List<InventoryItemVisualController>();

    public void Initialize(Dictionary<string, InventoryItemData> shopItems, ItemsDatabaseScriptableObject itemsDatabase, Action<string> OnButtonPressed)
    {
        foreach (var item in shopItems)
        {
            InventoryItemVisualController itemVisual = Instantiate(itemPrefab, inventoryGrid);

            var itemInfo = itemsDatabase.itemsDictionary[item.Key];
            itemVisual.InitializeItem(item.Key, itemInfo.name, item.Value.quantity, itemInfo.price, itemInfo.icon, () => OnButtonPressed?.Invoke(item.Key));

            shopVisualItems.Add(itemVisual);
        }
        onPressedAction = OnButtonPressed;
    }

    private Action<string> onPressedAction;

    public void RefreshUI(Dictionary<string, InventoryItemData> shopItems, ItemsDatabaseScriptableObject itemsDatabase)
    {
        foreach (var child in shopVisualItems)
            Destroy(child.gameObject);
        shopVisualItems.Clear();

        Initialize(shopItems, itemsDatabase, onPressedAction);
    }

    public void OpenInventory(bool isOpen)
    {
        inventoryPanelParent.SetActive(isOpen);
    }
}