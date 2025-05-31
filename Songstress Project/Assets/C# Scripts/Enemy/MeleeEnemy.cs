using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int dmg;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float lineOfSightLength = 5f;
    [SerializeField] private LayerMask obstacleLayer;

    private float cooldownTimer = Mathf.Infinity;
    private Transform player;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Checks if player is in sight
        if (PlayerInSight() && HasLineOfSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
                isAttacking = true;
                Invoke(nameof(ResetAttack), 1f);
            }
        }
    }
    private void ResetAttack()
    {
        isAttacking = false;
    }
    private bool PlayerInSight()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        Vector2 castOrigin = boxCollider.bounds.center;

        RaycastHit2D hit = Physics2D.BoxCast(
            castOrigin,
            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y),
            0,
            directionToPlayer,
            colliderDistance,
            playerLayer
        );

        return hit.collider != null;
    }

    void OnDrawGizmos()
    {
        if (boxCollider == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + Vector3.right * range * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        );

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * lineOfSightLength);
        Gizmos.DrawLine(transform.position, transform.position - Vector3.right * lineOfSightLength); // both directions
    }

    public bool isAttacking { get; private set; }

    public bool HasLineOfSight()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, lineOfSightLength, playerLayer | obstacleLayer);

        return hit.collider != null && hit.collider.CompareTag("Player");
    }


}
