using UnityEngine;

public class AiChase : MonoBehaviour
{
    public Transform player;
    public float speed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (player != null)
        {
            // Get current and target positions
            Vector2 currentPos = transform.position;
            Vector2 targetPos = new Vector2(player.position.x, currentPos.y); // lock Y axis

            // Move toward player (X axis only)
            transform.position = Vector2.MoveTowards(currentPos, targetPos, speed * Time.deltaTime);
            animator.SetBool("Walking", direction.magnitude > 0.01f);

            if (player.position.x < transform.position.x)
                transform.localScale = new Vector3(-1.1f, 1.1f, 1.1f); // Face left
            else
                transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);  // Face right
        }
        else
        {
            animator.SetBool("Walking", false);
        }
        

    }
    
}
