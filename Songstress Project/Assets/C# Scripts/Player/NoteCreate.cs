using System.Net;
using System;
using UnityEngine;
using Unity.VisualScripting;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Collections;
using static InstrummentManager;

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
    //private bool RangeChecker = false;
    
    //combo Checker
    private float comboWindow = 0.12f;
    private List<int> currentCombo = new List<int>();
    private float comboTimer = 0f;

    private float comboCooldown = 0f;

    private int Instrument = 0;
    /*
    0 - Kalimba
    1 - E Guitar
    2 - Guitar
    3 - Sax
    4 - Harmonica
    5 - Djembe
     
     */

    public void InstrumentSelection()
    {
        Instrument = InstrumentManager.SelectedInstrument;

        // optional switch for processing:
        switch (Instrument)
        {
            case 0: Debug.Log("Kalimba"); break;
            case 1: Debug.Log("E Guitar"); break;
            case 2: Debug.Log("Guitar"); break;
            case 3: Debug.Log("Sax"); break;
            case 4: Debug.Log("Harmonica"); break;
            case 5: Debug.Log("Djembe"); break;
                // etc.
        }
    }

    void Start()
    {
        MCtransform = GetComponent<Transform>();
        Instrument = InstrummentManager.SelectedInstrument;
        Debug.Log("Instrument: " + Instrument);
    }
    private void Awake()
    {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        //if (RangeChecker == false) Debug.Log("Nah I'd Wave");
    }

    void Update()
    {
        if (Sprite.flipX == true){if (OffsetX < 0) { } else OffsetX *= -1;}
        else{if (OffsetX > 0) { }else OffsetX *= -1;}
        notePlacement = new Vector3(MCtransform.position.x + OffsetX, MCtransform.position.y + OffsetY, 0);

       
        grounded = movement.grounded;
        if (grounded)
        {
            InputChecker();
            bool ComboActivate = false;

            if (Input.GetKey("1") || Input.GetKey("2") || Input.GetKey("3") || Input.GetKey("4") || Input.GetKey("5") || Input.GetKey("6") || Input.GetKey("7") || Input.GetKey("8") || Input.GetKey("8") || Input.GetKey("9"))
            {
                comboTimer += Time.deltaTime;
            }
            //combo

            if (currentCombo.Count > 0 && (!Input.GetKey("1") && !Input.GetKey("2") && !Input.GetKey("3") && !Input.GetKey("4") && !Input.GetKey("5") && !Input.GetKey("6") && !Input.GetKey("7") && !Input.GetKey("8") && !Input.GetKey("8") && !Input.GetKey("9")))
            {              
                if (comboTimer >= comboWindow && comboCooldown <= 0f)
                {
                    ComboActivate = ChargedAttack(notePlacement, currentCombo);
                    currentCombo.Clear();
                    comboTimer = 0f;
                }
            }
            if (ComboActivate) comboCooldown += 5f;
            if(comboCooldown > 0f) comboCooldown -= Time.deltaTime;

            attackKeys(notePlacement);



        }
    }
    
    private void InputChecker()
    {
        if (!currentCombo.Contains(1)) if (Input.GetKey("1")) currentCombo.Add(1);
        if (!currentCombo.Contains(2)) if (Input.GetKey("2")) currentCombo.Add(2);
        if (!currentCombo.Contains(3)) if (Input.GetKey("3")) currentCombo.Add(3);
        if (!currentCombo.Contains(4)) if (Input.GetKey("4")) currentCombo.Add(4);
        if (!currentCombo.Contains(5)) if (Input.GetKey("5")) currentCombo.Add(5);
        if (!currentCombo.Contains(6)) if (Input.GetKey("6")) currentCombo.Add(6);
        if (!currentCombo.Contains(7)) if (Input.GetKey("7")) currentCombo.Add(7);
        if (!currentCombo.Contains(8)) if (Input.GetKey("8")) currentCombo.Add(8);
        if (!currentCombo.Contains(9)) if (Input.GetKey("9")) currentCombo.Add(9);
    }

    private bool ChargedAttack(Vector3 notePlacement, List<int> comboKeys)
    {
        if (comboKeys.Contains(1) && comboKeys.Contains(2) && comboKeys.Count == 2)
        {
            Debug.Log("Combo CA1 got triggered ");
            poolManager.SpawnObject(Charged_1, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            return true;
        }

        if (comboKeys.Contains(1) && comboKeys.Contains(3) && comboKeys.Contains(5) && comboKeys.Count == 3)
        {
            Debug.Log("Combo CA2 got triggered ");
            poolManager.SpawnObject(Charged_2, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);        
            return true;
        }

        return false;
    }

    void attackKeys(Vector3 notePlacement)
    {
        if (Input.GetKeyUp("1")) { 
            poolManager.SpawnObject(SampleNote, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.C5);
        }
        if (Input.GetKeyUp("2")) { 
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.D5);
        }
        if (Input.GetKeyUp("3")) { 
            poolManager.SpawnObject(SampleQuarter, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.E5);
        }
        if (Input.GetKeyUp("4")) { 
            poolManager.SpawnObject(SampleHalf, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.F5);
        }
        if (Input.GetKeyUp("5"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.G5);
        }
        if (Input.GetKeyUp("6"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.A5);
        }
        if (Input.GetKeyUp("7"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.B5);
        }
        if (Input.GetKeyUp("8"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.C6);
        }
        if (Input.GetKeyUp("9"))
        {
            poolManager.SpawnObject(SampleEighth, notePlacement, Quaternion.identity, poolManager.PoolType.GameObject);
            SoundManager.PlayInstrument(Instrument, Octave.D6);
        }
    }
}
