using Player;
using Scriptable_Objects.WeaponScripts;
using UnityEngine;

namespace Item.SpecialItemTypes
{
    [CreateAssetMenu(fileName = "MyMeleeWeapon", menuName = "ItemTypes/MeleeWeapon", order = 0)]
    public class MeleeWeapon : Weapon
    {
        public GameObject projectilePrefab;
        public float lifetime;
        public float hitboxLifetime;
        public bool glueToPlayer = true;
        
        public override void OnAttack(PlayerEntity player, Vector2 direction)
        {
            base.OnAttack(player, direction);

            var attack = Instantiate(projectilePrefab, glueToPlayer ? player.transform : null, false);
            if (!glueToPlayer)
            {
                attack.transform.position = player.transform.position;
            }
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 90;
            attack.transform.rotation = Quaternion.Euler(0, 0, angle);

            var weaponSlash = attack.GetComponentInChildren<AbstractWeaponProjectile>();
            weaponSlash.SetWeapon(this);
            Destroy(weaponSlash, hitboxLifetime);
            
            Destroy(attack, lifetime);
        }
    }
}