using System;
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

        protected void Awake()
        {
            instance = this;
            
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
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
    }
}
