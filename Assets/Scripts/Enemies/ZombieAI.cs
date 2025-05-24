using Player;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 6f;
    private Transform _transform;

    private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float Health;
    public float MaxHealth;
    public float Damage;

    [SerializeField]public float distance = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        _transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        distance = Vector2.Distance(_transform.position, target.position);


        if (distance <= detectionRange)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            agent.ResetPath();
        }

        //Следующие страшные строчки говнокода отвечают за поворот зомби,извините пока чтото получше не придумал
        Vector2 velocity = agent.velocity;
        if (velocity.normalized.x >= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerEntity.instance.Damage(Damage);
        }
    }

}
