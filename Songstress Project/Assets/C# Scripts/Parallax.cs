using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startingPos, backgroundLength;
    public GameObject cam;
    public float delayEffect;
    void Start()
    {
        startingPos = transform.position.x;
        backgroundLength = GetComponent<SpriteRenderer>().bounds.size.x;
        Debug.Log(backgroundLength);
    }

    // Update is called once per frame
    void Update()
    {
        float distance = cam.transform.position.x * delayEffect;
        float movement = cam.transform.position.x * (1 - delayEffect);

        transform.position = new Vector2(startingPos + distance, transform.position.y);

        if (movement > startingPos + backgroundLength) startingPos += backgroundLength;
        if (movement < startingPos - backgroundLength) startingPos -= backgroundLength;

    }
}
