using UnityEngine;

public class ToEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private GameObject Enemy;
    Transform NoteTransform;

    float trackSpeedX = 0.01f;
    float trackSpeedY = 0.001f;
    void Start()
    {
        NoteTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyPosition = new Vector2(Enemy.transform.position.x, Enemy.transform.position.y);
        Vector2 NotePos = new Vector2(NoteTransform.position.x, NoteTransform.position.y);

        float EnemyX = enemyPosition.x;
        float EnemyY = enemyPosition.y;

        if (enemyPosition.x < 0) trackSpeedX *= -1;
        if (enemyPosition.y < 0) trackSpeedY *= -1;

        if (NotePos != enemyPosition)
        {
            NoteTransform.position = new Vector3(NotePos.x + trackSpeedX, NotePos.y + trackSpeedY);
        }
    }
}
