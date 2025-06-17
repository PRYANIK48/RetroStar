using Player;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ZombieAI : MonoBehaviour
{
    [Header("Параметры")]
    public float detectionRange = 6f;
    public float Health = 100f;
    public float MaxHealth = 100f;
    public float Damage = 10f;

    [Header("Цель")]
    public Transform target;

    [Header("Эффекты урона")]
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float hitFlashDuration = 0.1f;
    [SerializeField] private float knockbackForce = 5f;
    [SerializeField] private AudioClip hitSound;

    private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    private float checkInterval = 0.25f;
    private float checkTimer = 0f;
    private float detectionRangeSqr;
    private bool isDead = false;
    private Coroutine hitFlashRoutine;
    private Color originalColor;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        originalColor = spriteRenderer.color;
        detectionRangeSqr = detectionRange * detectionRange;

        agent.avoidancePriority = Random.Range(30, 80);

        if (target == null && PlayerEntity.instance != null)
            target = PlayerEntity.instance.transform;
    }

    void Update()
    {
        checkTimer -= Time.deltaTime;

        if (checkTimer <= 0f)
        {
            checkTimer = checkInterval;
            UpdateMovement();
        }

        FlipSpriteBasedOnVelocity();
    }

    private void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (target == null) return;

        Vector2 toTarget = target.position - transform.position;
        if (toTarget.sqrMagnitude <= detectionRangeSqr)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.ResetPath();
        }
    }

    private void FlipSpriteBasedOnVelocity()
    {
        Vector2 velocity = agent.velocity;
        if (velocity.x > 0.01f)
            spriteRenderer.flipX = false;
        else if (velocity.x < -0.01f)
            spriteRenderer.flipX = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerEntity.instance?.Damage(Damage);
        }
    }

<<<<<<< Updated upstream
=======
    void Damageable.Damage(float damage)
    {
        if (isDead) return;

        Health -= damage;
        PlayHitEffects();

        if (Health <= 0f)
        {
            isDead = true;
            Die();
        }
    }

    private void PlayHitEffects()
    {
        FlashOnHit();
        // Knockback();
        PlayHitSound();
    }

    private void FlashOnHit()
    {
        if (hitFlashRoutine != null)
            StopCoroutine(hitFlashRoutine);
        hitFlashRoutine = StartCoroutine(HitFlashCoroutine());
    }

    private IEnumerator HitFlashCoroutine()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitFlashDuration);
        spriteRenderer.color = originalColor;
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void Die()
    {
        // TODO: пророботанная смэртб, и анимка
        Destroy(gameObject);
    }
>>>>>>> Stashed changes
}
