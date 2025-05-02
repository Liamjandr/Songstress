using System.Net;
using System;
using UnityEngine;

public class SampleInstant : MonoBehaviour
{
    [SerializeField] private GameObject SampleNote;
    [SerializeField] private GameObject SampleEighth;
    [SerializeField] private GameObject SampleQuarter;
    [SerializeField] private GameObject SampleHalf;
    private Transform MCtransform;
    private SpriteRenderer Sprite;

    private float OffsetX = 1.109f;
    private float OffsetY = 0.553f;

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
        if (Input.GetKey("1")) Instantiate(SampleNote, notePlacement, Quaternion.identity);
        if (Input.GetKeyDown("2")) Instantiate(SampleEighth, notePlacement, Quaternion.identity);
        if (Input.GetKeyDown("3")) Instantiate(SampleQuarter, notePlacement, Quaternion.identity);
        if (Input.GetKeyDown("4")) Instantiate(SampleHalf, notePlacement, Quaternion.identity);
       
    }
}
