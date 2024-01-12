using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventoryVisualController : MonoBehaviour
{
    public InventoryItemVisualController itemPrefab;
    public Transform inventoryGrid;
    public GameObject inventoryPanelParent;

    [Header("Fields")]
    private List<InventoryItemVisualController> inventoryVisualItems = new List<InventoryItemVisualController>();
    public bool isSellMode = false;

    public void Initialize(Dictionary<string, InventoryItemData> shopItems, ItemsDatabaseScriptableObject itemsDatabase, Action<string> OnButtonPressed, Action<string> OnAlternatePressed = null)
    {
        foreach (var item in shopItems)
        {
            InventoryItemVisualController itemVisual = Instantiate(itemPrefab, inventoryGrid);

            var itemInfo = itemsDatabase.itemsDictionary[item.Key];
            bool canBeEquipped = itemInfo.animatorController != null || isSellMode;
            itemVisual.InitializeItem(item.Key, itemInfo.name, item.Value.quantity, itemInfo.price, itemInfo.icon, () => OnButtonPressed?.Invoke(item.Key), canBeEquipped, () => OnAlternatePressed?.Invoke(item.Key));

            inventoryVisualItems.Add(itemVisual);
        }
        EnableAlternateButton(isSellMode);
        onPressedAction = OnButtonPressed;
        onAltPressedAction = OnAlternatePressed;
    }

    private Action<string> onPressedAction;
    private Action<string> onAltPressedAction;

    public void RefreshUI(Dictionary<string, InventoryItemData> inventoryItems, ItemsDatabaseScriptableObject itemsDatabase)
    {
        foreach (var visualItem in inventoryVisualItems)
            Destroy(visualItem.gameObject);
        inventoryVisualItems.Clear();

        Initialize(inventoryItems, itemsDatabase, onPressedAction, onAltPressedAction);
    }

    public void OpenInventory(bool isOpen)
    {
        if (isOpen)
        {
            inventoryPanelParent.transform.localScale = Vector3.one * 0.1f;
            inventoryPanelParent.SetActive(true);
            LeanTween.scale(inventoryPanelParent, Vector3.one, 0.3f).setEase(LeanTweenType.easeInOutBack);
        }
        else
        {
            LeanTween.scale(inventoryPanelParent, Vector3.one * 0.1f, 0.3f).setEase(LeanTweenType.easeInOutBack).setOnComplete(() =>
                inventoryPanelParent.SetActive(false)
            );
        }
    }

    public void EnableAlternateButton(bool enableAlt)
    {
        foreach (var visualItem in inventoryVisualItems)
            visualItem.AlternateButtons(enableAlt);
    }
}