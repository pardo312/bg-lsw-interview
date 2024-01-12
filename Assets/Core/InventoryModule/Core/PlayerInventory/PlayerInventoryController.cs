using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InventoryItemData
{
    public string id;
    public int quantity;
    public bool equipped;
}

public class PlayerInventoryController : MonoBehaviour
{
    #region ----Fields----
    public static PlayerInventoryController Singleton;

    [Header("References")]
    public ItemsDatabaseScriptableObject itemsDatabase;
    public Dictionary<string, InventoryItemData> playerItems = new Dictionary<string, InventoryItemData>();

    [SerializeField] private List<InventoryItemData> initialPlayerItems = new List<InventoryItemData>();
    [SerializeField] private PlayerInventoryVisualController playerInventoryVisualController;
    public UnityEvent<string> onSellItem;
    private bool isOpen;
    public bool isSellMode = false;
    #endregion ----Fields----

    #region ----Methods----
    public void Awake()
    {
        if (Singleton != null)
        {
            Destroy(this);
            return;
        }

        Singleton = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        playerItems = initialPlayerItems.ToDictionary(item => item.id, item => item);
        playerInventoryVisualController.Initialize(playerItems, itemsDatabase, (itemId) => EquipItem(itemId), (itemId) => onSellItem?.Invoke(itemId));

        PlayerInputListener.Singleton.onInventory += () => OpenInventory(false);
    }

    public void OpenInventory(bool sellMode)
    {
        bool disableUserClosingInventoryInShop = isOpen && !sellMode && playerInventoryVisualController.isSellMode;
        if (disableUserClosingInventoryInShop)
            return;
        
        isOpen = !isOpen;
        playerInventoryVisualController.isSellMode = sellMode;
        playerInventoryVisualController.OpenInventory(isOpen);
        RefreshUI();
        ChangeSellingMode(sellMode);
    }

    public void ChangeSellingMode(bool sellMode)
    {
        playerInventoryVisualController.EnableAlternateButton(sellMode);
        isSellMode = sellMode;
    }

    public void EquipItem(string itemId)
    {
        PlayerManager.Singleton.playerAnimatorController.SetOutfit(itemsDatabase.itemsDictionary[itemId].animatorController);
    }

    public void RefreshUI()
    {
        playerInventoryVisualController.RefreshUI(playerItems, itemsDatabase);
    }
    #endregion ----Methods----
}

