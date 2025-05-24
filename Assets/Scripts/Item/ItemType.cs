using UnityEngine;

namespace Item
{
    [CreateAssetMenu(fileName = "MyItem", menuName = "DefaultItem", order = 0)]
    public class ItemType : ScriptableObject
    {
        public Sprite Sprite;
        public string Name;
        public string Description;
        
        public int MaxStack;
    }
}