using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class ShopController : MonoBehaviour
{
    #region ----Fields----
    [Header("References")]
    public ItemsDatabaseScriptableObject itemsDatabase;
    public ShopVisualController shopVisualController;

    public List<InventoryItemData> initialShopItems = new List<InventoryItemData>();
    public Dictionary<string, InventoryItemData> shopItems = new Dictionary<string, InventoryItemData>();
    private bool isOpen;
    #endregion ----Fields----

    #region ----Methods----
    public void Start()
    {
        shopItems = initialShopItems.ToDictionary(item => item.id, item => item);
        shopVisualController.Initialize(shopItems, itemsDatabase, (itemId) => BuyItem(itemId));
    }

    public void OpenShop()
    {
        isOpen = !isOpen;
        shopVisualController.OpenInventory(isOpen);
    }

    public void BuyItem(string itemId)
    {
        if (!shopItems.ContainsKey(itemId))
            return;

        var shopItem = shopItems[itemId];
        ReduceItem(itemId, ref shopItems);
        IncreaseItem(itemId, ref PlayerInventoryController.Singleton.playerItems, shopItem);

        // Refresh UI
        shopVisualController.RefreshUI(shopItems, itemsDatabase);
        PlayerInventoryController.Singleton.RefreshUI();
    }

    public void SellItem(string itemId)
    {
        if (!PlayerInventoryController.Singleton.playerItems.ContainsKey(itemId))
            return;

        var playerItem = PlayerInventoryController.Singleton.playerItems[itemId];
        ReduceItem(itemId, ref PlayerInventoryController.Singleton.playerItems);
        IncreaseItem(itemId, ref shopItems, playerItem);

        // Refresh UI
        shopVisualController.RefreshUI(shopItems, itemsDatabase);
        PlayerInventoryController.Singleton.RefreshUI();
    }

    private void IncreaseItem(string itemId, ref Dictionary<string, InventoryItemData> items, InventoryItemData newItemData = null)
    {
        if (!items.ContainsKey(itemId))
            items.Add(itemId, new InventoryItemData()
            {
                id = newItemData.id,
                quantity = 1,
            });
        else
            items[itemId].quantity++;
    }

    private void ReduceItem(string itemId, ref Dictionary<string, InventoryItemData> items)
    {
        if (items[itemId].quantity == 1)
            items.Remove(itemId);
        else
            items[itemId].quantity--;
    }
    #endregion ----Methods----
}
