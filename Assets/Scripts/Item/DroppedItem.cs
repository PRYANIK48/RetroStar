using System;
using System.Collections;
using Player;
using Player.Inventory;
using UnityEngine;

namespace Item
{
    [ExecuteInEditMode]
    public class DroppedItem : MonoBehaviour
    {
        public ItemType ItemType;
        public int Count;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

        }
        private void UpdateSprite()
        {
            if (spriteRenderer && ItemType)
            {
                spriteRenderer.sprite = ItemType.Sprite;
            }
        }

        public bool CanBePickedUp {get; private set;}

        private const float pickupDelay = 1.5f;

        public ItemStack IntoItemStack()
        {
            return new ItemStack(ItemType, Count);
        }

        public void LoadItemStack(ItemStack stack)
        {
            this.Count = stack.Count;
            this.ItemType = stack.ItemType;
            UpdateSprite();
        }

        private void Start()
        {
            if (!Application.IsPlaying(gameObject)) return;
            CanBePickedUp = false;
            StartCoroutine(PickUpCoroutine());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            #if UNITY_EDITOR
            if (!Application.IsPlaying(gameObject)) return;
            #endif
            if (CanBePickedUp && other.GetComponent<PlayerEntity>())
            {
                PickUp();
            }
        }

        private void PickUp()
        {
            PlayerEntity.instance.PickUpItem(IntoItemStack());
            Destroy(gameObject);
        }

        private void Update()
        {
            #if UNITY_EDITOR
            if (Application.IsPlaying(gameObject)) return;
            UpdateSprite();
            #endif
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator PickUpCoroutine()
        {
            yield return new WaitForSeconds(pickupDelay);
            CanBePickedUp = true;
            if (PlayerEntity.instance.GetComponent<Collider2D>().bounds
                .Intersects(this.GetComponent<Collider2D>().bounds))
            {
                PickUp();
            }
        }
    }
}