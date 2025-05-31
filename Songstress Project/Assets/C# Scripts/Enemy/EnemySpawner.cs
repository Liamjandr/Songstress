using NUnit.Framework;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] private GameObject[] Enemies = new GameObject[4];

    [SerializeField] Vector3[] setSpawn = new Vector3[10];

    void Start()
    {
        for (int i = 0; i < setSpawn.Length; i++)
        {
            poolManager.SpawnObject(Enemies[0], setSpawn[i], Quaternion.identity, poolManager.PoolType.GameObject);
            //Debug.Log(poolManager.PoolType.GameObject);
        }    
    }

    //Create random spawn point on runtime
    //create enemies when the player entered certain area.
}
