using System.Collections.Generic;
using Environment;
using Player;
using UnityEngine;

namespace Scriptable_Objects.WeaponScripts
{
    [RequireComponent(typeof(Collider))]
    public class WeaponProjectile : AbstractWeaponProjectile
    {
        private readonly List<Damageable> alreadyDamaged = new();
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var damageable = collision.GetComponent<Damageable>();
            if (damageable != null && !alreadyDamaged.Contains(damageable))
            {
                alreadyDamaged.Add(damageable);
                weapon.OnHit(PlayerEntity.instance, collision.gameObject);
            }
        }
    }
}
