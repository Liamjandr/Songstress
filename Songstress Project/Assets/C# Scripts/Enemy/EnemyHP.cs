using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private int overwhelm = 0;
    [SerializeField] private int enemyHealth = 100;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (overwhelm >= enemyHealth)
        {
            poolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note Attacks"))
        {
            //Debug.Log("Got Hit by " + collision.gameObject.tag);
            overwhelm++;
        }
        if (collision.gameObject.CompareTag("Charged Attacks"))
        {
            //Debug.Log("Got Hit by " + collision.gameObject.tag);
            overwhelm += 50;
        }
    }
}
