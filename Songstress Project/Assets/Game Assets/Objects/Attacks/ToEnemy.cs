using UnityEngine;

public class ToEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject Enemy;
    //private GameObject NoteFab;
    Transform NoteTransform;
    private GameObject Enemy2;

    float trackSpeedX = 10f;
    //float trackSpeedY = 0.001f;
    void Start()
    {
        NoteTransform = GetComponent<Transform>();
        //NoteFab = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy2 = GameObject.FindGameObjectWithTag("RadWorm");
        if(Enemy != null)
        {
            //Vector2 enemyPosition = new Vector2(Enemy.transform.position.x, Enemy.transform.position.y);
            //Vector2 NotePos = new Vector2(NoteTransform.position.x, NoteTransform.position.y);
            Vector2 TrackPos =  (Enemy2.transform.position - NoteTransform.position).normalized;
            NoteTransform.position = Vector2.MoveTowards(NoteTransform.position, Enemy2.transform.position, Time.deltaTime * trackSpeedX);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "RadWorm")
        {
            poolManager.ReturnObjectToPool(this.gameObject);
            Debug.Log("Collided with RadWorm");
        }
    }


    //For Collisions
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Collided");
    //    Destroy(NoteFab);
    //}
}
