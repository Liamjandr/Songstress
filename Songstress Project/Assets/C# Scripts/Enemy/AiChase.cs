using UnityEngine;

public class AiChase : MonoBehaviour
{
    public Transform player;
    public float speed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float distance;
    private MeleeEnemy meleeEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meleeEnemy = GetComponent<MeleeEnemy>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            animator.SetBool("Walking", false);
            return;
        }

        // Stop moving if attacking
        if (meleeEnemy != null && meleeEnemy.isAttacking)
        {
            animator.SetBool("Walking", false);
            return;
        }

        // Stop moving if player is not in line of sight
        if (meleeEnemy != null && !meleeEnemy.HasLineOfSight()) 
        {
            animator.SetBool("Walking", false);
            return;
        }

        // Continue chasing the player
        Vector2 currentPos = transform.position;
        Vector2 targetPos = new Vector2(player.position.x, currentPos.y); // lock Y axis

        transform.position = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);

        Vector2 direction = player.position - transform.position;
        animator.SetBool("Walking", direction.magnitude > 0.01f);

        // Flip sprite
        if (player.position.x < transform.position.x)
            transform.localScale = new Vector3(-1.1f, 1.1f, 1.1f); // Face left
        else
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);  // Face right


    }
    
}
