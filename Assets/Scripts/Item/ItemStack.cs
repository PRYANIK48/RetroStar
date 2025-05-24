using System;

namespace Item
{
    public class ItemStack : IEquatable<ItemStack>
    {
        public readonly ItemType ItemType;

        public int Count;

        public ItemStack(ItemType itemType, int count)
        {
            this.ItemType = itemType;
            this.Count = count;
        }

        public bool Equals(ItemStack other)
        {
            return Equals(ItemType, other.ItemType) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemStack other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ItemType, Count);
        }
    }
}