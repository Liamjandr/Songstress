using UnityEngine;

public class enemy : MonoBehaviour
{

    public Transform target;
    public float speed;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int overwhelm = 0;
    [SerializeField] private int enemyHealth = 100;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 scale = transform.localScale;

        if (player.transform.position.x < transform.position.x - 10)
        {
            scale.x = Mathf.Abs(scale.x) * -1;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        transform.localScale = scale;

        if (overwhelm >= enemyHealth)
        {
            poolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note Attacks"))
        {
            Debug.Log("Got Hit by " + collision.gameObject.tag);
            overwhelm++;
        }
    }
}
