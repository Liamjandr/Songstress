using System.Net;
using System;
using UnityEngine;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEditor.Experimental.GraphView;

public class NoteCreate : MonoBehaviour
{
    //Main Character
    [SerializeField] private SpriteRenderer Sprite;
    //Attack Prefabs
    [SerializeField] private GameObject SampleNote;
    [SerializeField] private GameObject SampleEighth;
    [SerializeField] private GameObject SampleQuarter;
    [SerializeField] private GameObject SampleHalf;
    [SerializeField] private GameObject Charged_1;
    [SerializeField] private GameObject Charged_2;
    [SerializeField] private GameObject Charged_3;
    [SerializeField] private GameObject Charged_4;
    [SerializeField] private GameObject Charged_5;

    private GameObject Enemy;
    private Transform MCtransform;
    //Attack SpawnPoint
    private float OffsetX = 1.109f;
    private float OffsetY = 0.553f;
    private Vector3 notePlacement;
    //Ground Checker
    Movement movement;
    private bool grounded;
    
    private bool RangeChecker = false;

    private float pressedTimer = 0.2f;
    private float timePressed = 0f;
    bool[] keyChecker = new bool[9];
 /*   bool chargedChecker = false;
    bool isKeypressed = false;*/

    void Start()
    {
        MCtransform = GetComponent<Transform>();
    }
    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        if (RangeChecker == false) Debug.Log("Nah I'd Wave");

        for (int i = 0; i < keyChecker.Length; i++) { keyChecker[i] = false; }
    }

    void Update()
    {
        if (Sprite.flipX == true){if (OffsetX < 0) { } else OffsetX *= -1;}
        else{if (OffsetX > 0) { }else OffsetX *= -1;}
        notePlacement = new Vector3(MCtransform.position.x + OffsetX, MCtransform.position.y + OffsetY, 0);

        
        //kahlil
        grounded = movement.grounded;
        if (grounded)
        {
            InputChecker();

            if (Input.GetKey("1") || Input.GetKey("2") || Input.GetKey("3") || Input.GetKey("4") || Input.GetKey("5") || Input.GetKey("6") || Input.GetKey("7") || Input.GetKey("8") || Input.GetKey("8") || Input.GetKey("9"))
            {
                timePressed += Time.deltaTime;
                if(Input.GetKeyUp("1") && Input.GetKeyUp("2")) poolManager.SpawnObject(Charged_1, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);

            }
            else { timePressed = 0f; }
            if(timePressed < pressedTimer) attackKeys(notePlacement);
        }
        //chargedChecker = false;

    }

    private bool[] InputChecker()
    {
        bool[] inputKeys = new bool[9];
        if (Input.GetKey("1")) inputKeys[0] = true;
        if (Input.GetKey("2")) inputKeys[1] = true;
        if (Input.GetKey("3")) inputKeys[2] = true;
        if (Input.GetKey("4")) inputKeys[3] = true;
        if (Input.GetKey("5")) inputKeys[4] = true;
        if (Input.GetKey("6")) inputKeys[5] = true;
        if (Input.GetKey("7")) inputKeys[6] = true;
        if (Input.GetKey("8")) inputKeys[7] = true;
        if (Input.GetKey("9")) inputKeys[8] = true;
        return inputKeys;
    }

    private bool ChargedAttack(Vector3 notePlacement)
    {
        Debug.Log("The Attack Keys Are Being Pressed");
        if (Input.anyKey == true) return true;
        else return false;
    }

    //Instantiating objects and at the same time storing it in an object pool.
    void attackKeys(Vector3 notePlacement)
    {
        if (Input.GetKeyUp("1")) { 
            poolManager.SpawnObject(SampleNote, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_C5);
        }
        if (Input.GetKeyUp("2")) { 
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_D5);
        }
        if (Input.GetKeyUp("3")) { 
            poolManager.SpawnObject(SampleQuarter, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_E5);
        }
        if (Input.GetKeyUp("4")) { 
            poolManager.SpawnObject(SampleHalf, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_F5);
        }
        if (Input.GetKeyUp("5"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_G5);
        }
        if (Input.GetKeyUp("6"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_A5);
        }
        if (Input.GetKeyUp("7"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_B5);
        }
        if (Input.GetKeyUp("8"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_C6);
        }
        if (Input.GetKeyUp("9"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayKalimba(Kalimba.kal_D6);
        }
    }

    //Player's Collision on Detecting Enemies within Attack Range.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) RangeChecker = true;
        Debug.Log("Enemies are in Range!");
        Debug.Log("The Enemy is " + collision.gameObject.tag);
        Enemy = collision.gameObject;
        //Debug.Log(collision.gameObject.transform);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //RangeChecker = true;

            Enemy = collision.gameObject;
            //Debug.Log(collision.gameObject.transform.position);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9) RangeChecker = false;
        Debug.Log("Enemies are out of Range!");
    }


    /*if (Input.GetKeyDown("1"))
        {
            //if(Input.anyKey == true) ChargedAttack(notePlacement);
            timePressed = Time.time;
            isKeypressed = true;
            if (chargedChecker == false) attackKeys(notePlacement);
        }

        if (isKeypressed && Input.GetKey("1"))
        {
            if ((Time.time - timePressed) >= pressedTimer)
            {
                Debug.Log("Key is now Pressed");
                chargedChecker = true;
            }
        }

        //Debug.Log(Time.time - timePressed);

        if (Input.GetKey("1") && chargedChecker == false)
        {
            isKeypressed = false;
        }*/
    
}
