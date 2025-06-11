using Environment;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Item.SpecialItemTypes
{
    [CreateAssetMenu(fileName = "MyWeapon", menuName = "ItemTypes/Weapon", order = 0)]
    
    public class Weapon : ItemType
    {
        public UnityEvent<Vector2> onAttackEvent;
        public UnityEvent<GameObject> onHitEvent;
        public float Damage;
        public virtual void OnAttack(PlayerEntity player, Vector2 direction)
        {
            onAttackEvent?.Invoke(direction);
        }

        public virtual void OnHit(PlayerEntity player, GameObject entity)
        {
            onHitEvent?.Invoke(entity);
            entity.GetComponent<Damageable>().Damage(Damage);
        }
    }
}