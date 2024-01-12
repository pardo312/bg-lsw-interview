using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    #region ----Fields----
    [Header("References")]
    public ItemsDatabaseScriptableObject itemsDatabase;

    public List<InventoryItemData> playerItems = new List<InventoryItemData>();
    public InventoryItemVisualController itemPrefab;
    public Transform inventoryGrid;
    public GameObject inventoryPanelParent;

    [Header("Fields")]
    private List<InventoryItemVisualController> playerVisualItems = new List<InventoryItemVisualController>();
    private bool isOpen;
    #endregion ----Fields----

    #region ----Methods----
    void Start()
    {
        foreach (var item in playerItems)
        {
            InventoryItemVisualController itemVisual = Instantiate(itemPrefab, inventoryGrid);

            var itemInfo = itemsDatabase.itemsDictionary[item.id];
            itemVisual.InitializeItem(item.id, itemInfo.name, item.quantity, itemInfo.price, itemInfo.icon);

            playerVisualItems.Add(itemVisual);
        }
    }

    public void OpenInventory()
    {
        isOpen = !isOpen;
        inventoryPanelParent.SetActive(isOpen);
    }
    #endregion ----Methods----
}
