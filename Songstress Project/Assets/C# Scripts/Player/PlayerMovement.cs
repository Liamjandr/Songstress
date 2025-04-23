using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed = 400f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame

    //FixedUpdate is fixed within the device's framerate
    private void FixedUpdate()
    {
        //Keyboard Inputs
        // "Horizontal" is in Edit/Project Settings/Input Manager
        float HorizontalInput = Input.GetAxisRaw("Horizontal");
        float HorizontalMovement = HorizontalInput * speed * Time.deltaTime;
        rb.linearVelocity = new Vector2 (HorizontalMovement, rb.linearVelocityY);

        Anim(HorizontalInput);
        if (Input.GetKey("a")) sprite.flipX = true;
        if (Input.GetKey("d")) sprite.flipX = false;

    }

    //Framerate of the game
    void Update()
    {
        
    }

    private void Anim(float moveInput)
    {
        if(moveInput != 0)
        {
            animator.SetBool("RunState", true);
        }
        else
        {
            animator.SetBool("RunState", false);
        }
    }
}
