using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float overwhelm = 1;
    public float enemyHealth = 15;
    private Scoring score;
    void Start()
    {
        score = FindAnyObjectByType<Scoring>();
    }

    // Update is called once per frame
    void Update()
    {
        if (overwhelm >= enemyHealth)
        {
            poolManager.ReturnObjectToPool(this.gameObject);
            score.increaseScore(100);
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
