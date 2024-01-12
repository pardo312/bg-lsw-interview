using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemVisualController : MonoBehaviour
{
    public TMP_Text itemName;
    public TMP_Text itemQuantity;
    public TMP_Text itemPrice;
    public Image itemIcon;
    private string itemId;


    public void InitializeItem(string id, string name, int quantity, string price, Sprite icon)
    {
        itemId = id;
        itemName.text = name;
        itemQuantity.text = quantity.ToString();
        itemPrice.text = price;
        itemIcon.sprite = icon;
    }
}
