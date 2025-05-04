using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 6f;
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator anim;
    private bool grounded;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    //FixedUpdate is fixed within the device's framerate
    //Framerate of the game
    void Update()
    {
        //Keyboard Inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        // move horizontically
        body.linearVelocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.linearVelocity.y);

        // left to right
        if (horizontalInput > 0.01f)    
        {
            transform.localScale = new Vector3((float)1, (float)1, (float)1);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3((float)-1, (float)1, (float)1);
        }
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        // Set walk animation parameter
        anim.SetBool("RunState", horizontalInput != 0);
        anim.SetBool("Grounded", grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            grounded = true;
    }
}
