using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public Sprite itemIcon;
    public string itemName;
    public int maxStack = 1;
    public int currentStack = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            Inventory.instance.AddItem(this);
        }
    }
}
