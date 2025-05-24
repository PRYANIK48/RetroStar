using Item;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public bool used = false;
    public Sprite stoneWithSword, stoneWithoutSword;
    public ItemType swordItem; 
    public SpriteRenderer spriteRenderer;
    public void PickupSword()
    {
        if (!used)
        {
            used = true;
            spriteRenderer.sprite = stoneWithoutSword;
            Inventory.instance.AddItem(new ItemStack(swordItem, 1)); 
        }
    }
}
