using Item.SpecialItemTypes;
using UnityEngine;

namespace Scriptable_Objects.WeaponScripts
{
    public abstract class AbstractWeaponProjectile : MonoBehaviour
    {
        protected Weapon weapon;

        public void SetWeapon(Weapon _weapon)
        {
            this.weapon = _weapon;
        }
    }
}