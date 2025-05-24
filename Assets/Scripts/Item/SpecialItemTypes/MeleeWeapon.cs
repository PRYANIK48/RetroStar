using UnityEngine;

namespace Item.SpecialItemTypes
{
    [CreateAssetMenu(fileName = "MyWeapon", menuName = "MeleeWeapon", order = 0)]
    
    public class MeleeWeapon : ItemType
    {
        public float Damage;
    }
}