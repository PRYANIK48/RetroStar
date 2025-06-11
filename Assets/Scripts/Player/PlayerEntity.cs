using System;
using Item;
using Item.SpecialItemTypes;
using JetBrains.Annotations;
using Player.Inventory;
using Player.Inventory.InventoryGUI;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerEntity : MonoBehaviour
    {
        public static PlayerEntity instance;

        //Режим разработчика
        public bool isDev = false;

        public float Health {private set; get;}
        private float maxHealth;
        
        private float maxEnergy;
        private float energy;
        
        private float speed = 5f;
        
        // Управление
        
        private static MainInputSystem.PlayerMapActions actionMap;
        
             // При остановке сбрасывается до [0, 0]
        private Vector2 movement; 
             // При остановке не сбрасывается
        private Vector2 movementDirection = Vector2.down;
        
        // Физика
        
        private Rigidbody2D rb;
        
        private Vector2 velocity;
        // private const float acceleration = 0.5f;
        // private const float deceleration = 0.5f;
        
        
        // Анимация
        
        private Animator animator;

        private static readonly int moveXHash = Animator.StringToHash("moveX"); 
        private static readonly int moveYHash = Animator.StringToHash("moveY"); 
        private static readonly int dirXHash = Animator.StringToHash("lastX"); 
        private static readonly int dirYHash = Animator.StringToHash("lastY");
        
        // Инвентарь
        
        [CanBeNull] private InventoryGUI inventoryGUI => InventoryGUI.instance.gameObject.activeInHierarchy ? InventoryGUI.instance : null;

        [CanBeNull] private Inventory.Inventory inventory => Inventory.Inventory.instance.gameObject.activeInHierarchy ? Inventory.Inventory.instance : null;

        private AudioClip dropSound;
        private AudioClip pickupSound;
        
        public DroppedItem EmptyItemPrefab;
        
        // Звуки
        
        private AudioSource audioSource;

        protected void Awake()
        {
            instance = this;
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody2D>();
            
            pickupSound = Resources.Load<AudioClip>("Audio/Take");
            dropSound = Resources.Load<AudioClip>("Audio/drop");
        }

        private void OnEnable()
        {
            MainInputManager.InputSystem.BlockInDialogs.OpenInventory.performed += ChangeInventoryState;
            MainInputManager.InputSystem.PlayerMap.DropItem.performed += DropItemEvent;
            MainInputManager.InputSystem.PlayerMap.Attack.performed += OnAttack;
        }
        private void OnDisable()
        {
            MainInputManager.InputSystem.BlockInDialogs.OpenInventory.performed -= ChangeInventoryState;
            MainInputManager.InputSystem.PlayerMap.DropItem.performed -= DropItemEvent;
            MainInputManager.InputSystem.PlayerMap.Attack.performed -= OnAttack;
            
        }

        private void DropItemEvent(InputAction.CallbackContext obj)
        {
            inventory?.HandleDropInput();
        }


        private void ChangeInventoryState(InputAction.CallbackContext callbackContext)
        {
            Inventory.Inventory.instance.gameObject.SetActive(InventoryGUI.instance.gameObject.activeSelf);
            InventoryGUI.instance.gameObject.SetActive(!InventoryGUI.instance.gameObject.activeSelf);
        }

        protected void Update()
        {
            movement = actionMap.Movement.ReadValue<Vector2>().normalized;
            
            if (movement != Vector2.zero)
            {
                movementDirection = movement;
                animator.SetFloat(dirXHash, movementDirection.x);
                animator.SetFloat(dirYHash, movementDirection.y);
                
            }
            
            animator.SetFloat(moveXHash, movement.x);
            animator.SetFloat(moveYHash, movement.y);

        }

        protected void FixedUpdate()
        {
            velocity = movement * speed;
            if (velocity != Vector2.zero)
            {
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            }
        }

        private void Start()
        {
            inventoryGUI?.gameObject.SetActive(false);
            
            actionMap = MainInputManager.InputSystem.PlayerMap;
            
            this.energy = this.maxEnergy;
            this.Health = this.maxHealth;
            
            this.LoadSavedPlayerData();
        }

        private void LoadSavedPlayerData()
        {
            // TODO: Загрузка данных игрока при появлении на сцене 
        }

        public void Damage(float amount)
        {
            Health = Math.Max(Health - amount, 0);
            
            if (Health <= 0)
            {
                //смэртб
            }
        }

        public void Heal(float amount)
        {
            Health = Math.Min(Health + amount, maxHealth);
        }

        public void SetHealth(float amount)
        {
            Health = Math.Clamp(amount, 0, maxHealth);
        }

        public void SetEnergy(float amount)
        {
            energy = amount;
        }
        
        
        
        // Inventory

        public void PickUpItem(ItemStack item)
        {
            if (InventoryStorage.instance.AddItem(item, out var dropped))
            {
                PlaySound(pickupSound);
                inventory?.ShowPickupText(item.ItemType.Name);
                
                inventory?.UpdateHotbarUI();

                inventoryGUI?.UpdateSlots();
            }
            else
            {
                Debug.Log("Инвентарь заполнен!");
            }
        }

        public void DropItem(int index)
        {
            var item = InventoryStorage.instance.GetItem(index);
            if (item == null) return; 
            
            if (item.Count <= 1) InventoryStorage.instance.SetItem(index, null);
            else
            {
                item.Count -= 1;
            }
            DropItem(item);
            
            inventory?.UpdateHotbarUI();

            inventoryGUI?.UpdateSlots();
        }

        public void DropItem(ItemStack item)
        {
            DroppedItem droppedItem = Instantiate(EmptyItemPrefab, PlayerEntity.instance.gameObject.transform.position, Quaternion.identity);
            droppedItem.LoadItemStack(item);
            droppedItem.Count = 1;

            PlaySound(dropSound);
        }

        private void PlaySound(AudioClip clip)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        private void OnAttack(InputAction.CallbackContext callbackContext)
        {
            var item = InventoryStorage.instance.GetSelectedItem();
            if (item is { ItemType: Weapon weapon })
            {
                weapon.OnAttack(this, movementDirection);
            }
        }
    }
}
