using Item;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text countText;
    private ItemStack itemStack;

    public GameObject selectionFrame;

    public void SetItem(ItemStack stack)
    {
        itemStack = stack;
        icon.sprite = stack.ItemType.Sprite;
        icon.enabled = true;
        UpdateStackText();
    }

    public void ClearSlot()
    {
        itemStack = null;
        icon.sprite = null;
        icon.enabled = false;
        countText.text = "";
    }

    public void UpdateStackText()
    {
        if (itemStack == null) return;
        countText.text = itemStack.Count > 1 ? itemStack.Count.ToString() : "";
    }

    public void SetHighlight(bool isActive)
    {
        selectionFrame.SetActive(isActive);
    }
}
