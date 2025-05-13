using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public bool used = false;
    public Sprite stoneWithSword, stoneWithoutSword;
    public Item swordItem; 
    public SpriteRenderer spriteRenderer;
    public void PickupSword()
    {
        if (!used)
        {
            used = true;
            spriteRenderer.sprite = stoneWithoutSword;
            Inventory.instance.AddItem(Instantiate(swordItem)); 
        }
    }
}
