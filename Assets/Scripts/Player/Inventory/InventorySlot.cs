using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text countText;
    private Item storedItem;
    private int stackSize = 0;

    public GameObject selectionFrame;

    public void SetItem(Item newItem, int count)
    {
        storedItem = newItem;
        stackSize = count;
        icon.sprite = newItem.itemIcon;
        icon.enabled = true;
        UpdateStackText();
    }

    public void IncreaseStack(int amount)
    {
        stackSize += amount;
        UpdateStackText();
    }

    public void DecreaseStack(int amount)
    {
        stackSize -= amount;
        UpdateStackText();
    }

    public void ClearSlot()
    {
        storedItem = null;
        stackSize = 0;
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
    }

    private void UpdateStackText()
    {
        countText.text = stackSize > 1 ? stackSize.ToString() : "";
    }

    public void SetHighlight(bool isActive)
    {
        selectionFrame.SetActive(isActive);
    }
}
