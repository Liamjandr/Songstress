using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework.Constraints;


public class poolManager : MonoBehaviour
{
    public static List<PooledObjectDetect> ObjectPools = new List<PooledObjectDetect>();

    private GameObject _objectPoolEmptyHolder;
    private static GameObject _particleSystemsEmpty;
    private static GameObject _gameObjectEmpty;

    public enum PoolType
    { 
        ParticleSystem,
        GameObject,
        None
    }
    public static PoolType PoolingType;
    private void Awake()
    {
        CreateEmpty();
    }

    private void CreateEmpty()
    {
        _objectPoolEmptyHolder = new GameObject("Pooled Objects");
        _particleSystemsEmpty = new GameObject("Pooled Effects");
        _particleSystemsEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
        _gameObjectEmpty = new GameObject("GameObjects");
        _gameObjectEmpty.transform.SetParent(_objectPoolEmptyHolder.transform);
    }
    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, PoolType poolType = PoolType.None)
    {
        PooledObjectDetect pool = ObjectPools.Find(p => p.LookupString == objectToSpawn.name);

        if(pool == null)
        {
            pool = new PooledObjectDetect() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }

        GameObject spawnableObj = null;
        foreach (GameObject obj in pool.InactiveObjects)
        {
            if (obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }

        if (spawnableObj == null)
        {
            GameObject parentObject = SetParentObject(poolType);
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
            if(parentObject != null)
            {
                spawnableObj.transform.SetParent(parentObject.transform);
            }
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }

        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {

        string cloneRemover = obj.name.Substring(0, obj.name.Length -7);
        PooledObjectDetect pool = ObjectPools.Find(p => p.LookupString == cloneRemover);
        
        if(pool == null)
        {
            Debug.Log("Trying to remove nonpooled Object" + obj.name);
        }
        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
    }

    private static GameObject SetParentObject(PoolType pooltype)
    {
        switch (pooltype)
        {
            case PoolType.ParticleSystem:
                return _particleSystemsEmpty;

            case PoolType.GameObject:
                return _gameObjectEmpty;

            case PoolType.None:
                return null;

            default:
                return null;
        }


    }

}


public class PooledObjectDetect
{
    public string LookupString;
    public List<GameObject> InactiveObjects = new List<GameObject>();

}
