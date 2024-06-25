using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public static Slime instance; 
    [Header("MoveTarget")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float chaseSpeed = 5f;
    [SerializeField] Vector3 areaCenter;
    [SerializeField] float areaRadius = 10f;
    [SerializeField] float distanceToKeep = 0.5f;
    [SerializeField] float obstacleAvoidanceDistance = 1f;
    [SerializeField] LayerMask obstacleLayer;

    Animator animator;
    Vector3 targetPosition;
    bool isChasing = false;
    GameObject player;
    public float dame;
    Rigidbody2D rb;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("SetRandomTargetPosition", 0f, 4f);
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        if (isChasing && player != null)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void SetRandomTargetPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * areaRadius;
        randomDirection += areaCenter;
        targetPosition = new Vector3(randomDirection.x, randomDirection.y, 0);
    }

    void Patrol()
    {
        MoveTowards(targetPosition, moveSpeed);
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            MoveTowards(player.transform.position, chaseSpeed);
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target);

        if (distanceToTarget <= distanceToKeep)
        {
            rb.velocity = Vector2.zero;
            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }
            return;
        }
        Vector3[] directions = new Vector3[]
        {
        direction,
        Quaternion.Euler(0, 0, 45) * direction,
        Quaternion.Euler(0, 0, -45) * direction,
        Quaternion.Euler(0, 0, 90) * direction,
        Quaternion.Euler(0, 0, -90) * direction
        };

        foreach (Vector3 dir in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, obstacleAvoidanceDistance, obstacleLayer);
            if (hit.collider == null)
            {
                direction = dir;
                break;
            }
        }

        rb.velocity = direction * speed;

        if (animator != null)
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
            player = null;
            animator.SetBool("IsMoving", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}