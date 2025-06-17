using System.Collections.Generic;
using Item;

namespace Player.Inventory
{
    public enum Equipment
    {
        HEAD = 0,
        BODY = 1,
        BOOTS = 2,
        ARTEFACT = 3,
        EXPANSION = 4
    }
    public class InventoryStorage
    {
        public static readonly InventoryStorage instance = new InventoryStorage();
        private readonly ItemStack[] items = new ItemStack[6*3];
        private readonly ItemStack[] hotbar = new ItemStack[6];
        private readonly ItemStack[] equipment = new ItemStack[3+2];

        public void LoadInventory()
        {
            // TODO
        }
        
        public void SetBagItem(int index, ItemStack item)
        {
            items[index] = item;
        }

        public ItemStack GetBagItem(int index)
        {
            return items[index];
        }
        public void SetHotbarItem(int index, ItemStack item)
        {
            hotbar[index] = item;
        }

        public ItemStack GetHotbarItem(int index)
        {
            return hotbar[index];
        }
        public void SetEquipment(Equipment index, ItemStack item)
        {
            equipment[(int) index] = item;
        }

        public ItemStack GetEquipment(Equipment index)
        {
            return equipment[(int) index];
        }
        
        // 0-5 Инвентарь
        // 6-23 Хотбар
        // 24-28 Снаряжение
        public void SetItem(int index, ItemStack item)
        {
            switch (index)
            {
                case >= 0 and <= 5:
                    SetHotbarItem(index, item);
                    break;
                case >= 6 and <= 23:
                    SetBagItem(index-6, item);
                    break;
                case >= 24 and <= 28:
                    SetEquipment((Equipment) (index-24), item);
                    break;
            }
        }

        public ItemStack GetItem(int index)
        {
            return index switch
            {
                >= 0 and <= 5 => GetHotbarItem(index),
                >= 6 and <= 23 => GetBagItem(index - 6),
                >= 24 and <= 28 => GetEquipment((Equipment)(index - 24)),
                _ => null
            };
        }

        public int GetFirstEmptySlot()
        {
            for (int i = 0; i < 29; i++)
            {
                var item = GetItem(i);
                if (item == null) return i;
            }

            return -1;
        }

        public bool AddItem(ItemStack item, out ItemStack dropped)
        {
            for (int i = 0; i < 29; i++)
            {
                var currentItem = GetItem(i);
                if (currentItem == null) continue;
                
                if (item.ItemType == currentItem.ItemType && currentItem.Count != currentItem.ItemType.MaxStack)
                {
                    if (currentItem.ItemType.MaxStack >= currentItem.Count + item.Count)
                    {
                        currentItem.Count += item.Count;
                    }
                    else
                    {
                        var remain = currentItem.ItemType.MaxStack - currentItem.Count;
                        currentItem.Count = currentItem.ItemType.MaxStack;

                        if (AddItem(new ItemStack(item.ItemType, item.Count - remain), out var stack))
                        {
                            dropped = null;
                            return true;
                        }
                        
                        PlayerEntity.instance.DropItem(stack);
                    }

                    dropped = null;
                    return true;
                }
            }
            var emptySlot = GetFirstEmptySlot();
            if (emptySlot == -1)
            {
                dropped = item;
                return false;
            }
            
            SetItem(emptySlot, item);
            dropped = null;
            return true;
        }
        

        public ItemStack GetSelectedItem()
        {
            return GetHotbarItem(Inventory.staticSelectedSlotIndex);
        }
    }
}