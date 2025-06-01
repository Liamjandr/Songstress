using Unity.VisualScripting;
using UnityEngine;


public class trackingEnemy : MonoBehaviour
{
    private GameObject targetEnemy;
    private Transform noteTransform;
    private SpriteRenderer sprite;
    //Despawn
    private float despawner = 0f;
    private float despawnTime = 5f;
    private float despawnSize = 0.1f;
    private float noteSize = 1;
    private float endSize;
    //Speed & range
    [SerializeField] private float trackSpeed = 10f;
    [SerializeField] private float trackingRange = 15f;

    //Wave Animation
    [SerializeField] public float waveAmplitude = 400f;
    [SerializeField] public float waveFrequency = 500f;


    // Random idle movement
    private float idleRad = 5f;
    private bool pickedLoc = false;
    private Vector2 randomPoint;
    private Vector2 noteVec2;
    void Start()
    {
        noteTransform = transform;
        sprite = GetComponent<SpriteRenderer>();       
    }

    void Update()
    {
        targetEnemy = FindNearestEnemyInRange();
        if (targetEnemy != null)
        {
            noteTransform.localScale = new Vector2(noteSize, noteSize);

            noteTransform.position = Vector2.MoveTowards(noteTransform.position, targetEnemy.transform.position, Time.deltaTime * trackSpeed);
            
            Vector3 dir = targetEnemy.transform.position - noteTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if(noteTransform.position.x < targetEnemy.transform.position.x)transform.rotation = Quaternion.Euler(0f, 0f, angle);
            else transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);

        }
        else
        {
            
            noteVec2 = new Vector2(noteTransform.position.x, noteTransform.position.y);

            if(!pickedLoc)
            {
                randomPoint = noteVec2 + (Random.insideUnitCircle.normalized * idleRad);
                pickedLoc = true;
            }
            noteTransform.position = Vector2.MoveTowards(noteTransform.position, randomPoint, Time.deltaTime * 1f);

            noteSize -= (despawnSize * Time.deltaTime);
            noteTransform.localScale = new Vector2(noteSize, noteSize);

            despawner += Time.deltaTime; 
            if (despawner > despawnTime || noteTransform.localScale.x < 0) 
            { 
                poolManager.ReturnObjectToPool(this.gameObject);
                despawner = 0;
                noteSize = 1;
            };

        }
        
    }

    GameObject FindNearestEnemyInRange()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float enemyDistance = Vector2.Distance(noteTransform.position, enemy.transform.position);
            if (enemyDistance < minDistance && enemyDistance <= trackingRange)
            {
                minDistance = enemyDistance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            poolManager.ReturnObjectToPool(this.gameObject);
            //Debug.Log($"Hit enemy: {collision.gameObject.name}");
        }
    }
}