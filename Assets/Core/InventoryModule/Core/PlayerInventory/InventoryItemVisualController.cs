using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemVisualController : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemQuantity;
    public TMP_Text itemPrice;
    public Image itemIcon;
    public Button itemButton;
    public Button itemButtonAlt;
    private bool shouldShow = false;


    public void InitializeItem(string id, string name, int quantity, string price, Sprite icon, Action onButtonPressed, bool showButton = true, Action alternateButtonPressed = null)
    {
        itemName.text = name;
        itemQuantity.text = quantity.ToString();
        itemPrice.text = price;
        itemIcon.sprite = icon;

        shouldShow = showButton;
        if (!showButton)
        {
            itemButton.gameObject.SetActive(false);
            itemButtonAlt.gameObject.SetActive(false);
        }
        else
        {
            itemButton.onClick.AddListener(() => onButtonPressed?.Invoke());
            if (alternateButtonPressed != null)
                itemButtonAlt.onClick.AddListener(() => alternateButtonPressed?.Invoke());
        }
    }

    public void AlternateButtons(bool enableAlt)
    {
        if (!shouldShow)
            return;
        itemButtonAlt.gameObject.SetActive(enableAlt);
        itemButton.gameObject.SetActive(!enableAlt);
    }
}
