using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public Sprite itemIcon; // Иконка предмета
    public string itemName; // Название предмета
    public int maxStack = 1; // Максимальный стак (1 = нельзя складывать)
    public int currentStack = 1; // Количество в стаке

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory0.instance.AddItem(this);
        }
    }
}
