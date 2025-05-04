using System.Net;
using System;
using UnityEngine;

public class SampleInstant : MonoBehaviour
{
    [SerializeField] private GameObject SampleNote;
    [SerializeField] private GameObject SampleEighth;
    [SerializeField] private GameObject SampleQuarter;
    [SerializeField] private GameObject SampleHalf;
    [SerializeField] private GameObject SampleCharged_1;
    private Transform MCtransform;
    private SpriteRenderer Sprite;

    private float OffsetX = 1.109f;
    private float OffsetY = 0.553f;

    //private float pressedTimer = 0.1f;
    //private float timePressed = 0f;
    ////float testTime = 0f;
    //bool chargedChecker = false;
    //bool isKeypressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MCtransform = GetComponent<Transform>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Sprite.flipX == true)
        {
            if (OffsetX < 0) { }
            else OffsetX *= -1;
        }
        else
        {
            if (OffsetX > 0) { }
            else OffsetX *= -1;
        }
        Vector3 notePlacement = new Vector3(MCtransform.position.x + OffsetX, MCtransform.position.y + OffsetY, 0);




        //if (Input.GetKeyDown("1"))
        //{
        //    //if(Input.anyKey == true) ChargedAttack(notePlacement);
        //    timePressed = Time.time;
        //    isKeypressed = true;
        //    if (chargedChecker == false) attackKeys(notePlacement);
        //}

        //if (isKeypressed && Input.GetKey("1"))
        //{
        //    if ((Time.time - timePressed) >= pressedTimer)
        //    {
        //        Debug.Log("Key is now Pressed");
        //        chargedChecker = true;
        //    }
        //}

        ////Debug.Log(Time.time - timePressed);

        //if (Input.GetKey("1") && chargedChecker == false)
        //{
        //    isKeypressed = false;
        //}



        if (Input.GetKeyUp("1") && Input.GetKeyUp("2")) Instantiate(SampleCharged_1, notePlacement, Quaternion.identity);
        else attackKeys(notePlacement);
        
        //chargedChecker = false;
    }

    private bool ChargedAttack(Vector3 notePlacement)
    {
        Debug.Log("The Attack Keys Are Being Pressed");
        if (Input.anyKey == true) return true;
        else return false;
    }

    void attackKeys(Vector3 notePlacement)
    {
        if (Input.GetKeyUp("1")) poolManager.SpawnObject(SampleNote, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
        if (Input.GetKeyUp("2")) poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
        if (Input.GetKeyUp("3")) poolManager.SpawnObject(SampleQuarter, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
        if (Input.GetKeyUp("4")) poolManager.SpawnObject(SampleHalf, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
    }
}
