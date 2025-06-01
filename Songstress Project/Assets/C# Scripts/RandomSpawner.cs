using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] Enemies = new GameObject[1];
    public int spawnCount = 10;
    public float radius = 8f;
    public float yValue = 0f;
    Vector3 spawnPoint;
    bool triggered = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(triggered == false)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                float random = Random.Range((transform.position.x - 8f), (transform.position.x + 9f));
                spawnPoint = new Vector3(random, yValue, transform.position.z);
                poolManager.SpawnObject(Enemies[0], spawnPoint, Quaternion.identity, poolManager.PoolType.GameObject);
            }
            triggered = true;
        }
    }
}
