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
    //private Vector2 idleDirection;
    private float idleRad = 5f;
    private bool pickedLoc = false;
    private Vector2 randomPoint;
    private Vector2 noteVec2;
    void Start()
    {
        noteTransform = transform;
        sprite = GetComponent<SpriteRenderer>();       
        //idleDirection = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        targetEnemy = FindNearestEnemyInRange();
        //Debug.Log(Mathf.Pow(125, 1f / 3f));
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
            //// Idle Sway (no target)
            //float idleWave = Mathf.Sin(Time.time * idleFrequency + idleOffset) * idleAmplitude;
            //Vector2 swayOffset = new Vector2(-idleDirection.y, idleDirection.x) * idleWave;

            //// Optionally drift slowly in idleDirection
            //Vector2 drift = idleDirection * (trackSpeed * 0.2f) * Time.deltaTime;

            //noteTransform.position += (Vector3)(swayOffset + drift);

            //// Optional: rotate to face movement direction
            //if (drift != Vector2.zero)
            //{
            //    noteTransform.right = idleDirection;
            //}
            
                noteVec2 = new Vector2(noteTransform.position.x, noteTransform.position.y);
            if(!pickedLoc)
            {
                randomPoint = noteVec2 + (Random.insideUnitCircle.normalized * idleRad);
                pickedLoc = true;
            }
            noteTransform.position = Vector2.MoveTowards(noteTransform.position, randomPoint, Time.deltaTime * 1f);
            //Vector3 dir = randomPoint - noteVec2;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //if (noteTransform.position.x < targetEnemy.transform.position.x) transform.rotation = Quaternion.Euler(0f, 0f, angle);
            //else transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);


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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("RadWorm");

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
        if (collision.gameObject.CompareTag("RadWorm"))
        {
            poolManager.ReturnObjectToPool(this.gameObject);
            Debug.Log($"Hit enemy: {collision.gameObject.name}");
        }
    }

    //private Vector2 WaveAnimation(Vector2 currentNotePos, Vector2 enemyPos)
    //{
    //    Vector2 A = currentNotePos;
    //    Vector2 B = enemyPos;
    //    //float arccosUP = (A.x * B.x) + (A.y * B.y);
    //    //float arccosDown1 = Mathf.Pow((A.x*A.x) + (A.y* A.y), 1f/3f);
    //    //float arccosDown2 = Mathf.Pow((B.x * B.x) + (B.y * B.y), 1f / 3f);
    //    //float directAngle = arccosUP / (arccosDown1 * arccosDown2);
    //    float directAngle = 3f;
    //    Debug.Log(directAngle);
    //    float rotateXPos = (A.x) * Mathf.Cos(directAngle) + (A.y) * Mathf.Sin(directAngle);
    //    float rotateYPos = -(A.x) * Mathf.Sin(directAngle) + (A.y) * Mathf.Cos(directAngle);

    //    Vector2 Wave = new Vector2(rotateXPos, rotateYPos);
    //    return Wave;

    //}

    //private Vector2 WaveAnimation2(Vector2 currentNotePos, Vector2 targetPos)
    //{
    //    Vector2 direction = (targetPos - currentNotePos).normalized;
    //    Vector2 perpendicular = new Vector2(-direction.y, direction.x); // Perpendicular to direction
    //    float waveOffset = Mathf.Sin(Time.time * waveFrequency) * waveAmplitude;

    //    return currentNotePos + perpendicular * waveOffset;
    //}

    //private Vector2 WaveAnimation3(Vector2 currentPos, Vector2 targetPos, float elapsedTime)
    //{
    //    Vector2 direction = (targetPos - currentPos).normalized;
    //    Vector2 perpendicular = new Vector2(-direction.y, direction.x);

    //    // Move forward toward the target
    //    Vector2 forwardStep = direction * trackSpeed * Time.deltaTime;

    //    // Apply wave offset perpendicular to direction of travel
    //    float waveOffset = Mathf.Sin(elapsedTime * waveFrequency) * waveAmplitude;
    //    Vector2 waveStep = perpendicular * waveOffset;

    //    return currentPos + forwardStep + waveStep;
    //}
}