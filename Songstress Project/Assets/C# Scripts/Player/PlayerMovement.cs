using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField] float jumpingHeight;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    private bool grounded;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

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
        else if (Input.GetKey("d")) sprite.flipX = false;
        else if (Input.GetKey(KeyCode.LeftArrow)) sprite.flipX = true;
        else if (Input.GetKey(KeyCode.RightArrow)) sprite.flipX = false;

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
        
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        animator.SetBool("Grounded", grounded);
    }

    //Framerate of the game
    void Update()
    {
        
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingHeight);
        animator.SetTrigger("Jump");
        animator.SetTrigger("Fall");
        grounded = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* if (!grounded)
         {
             animator.SetTrigger("Fall");
         }*/
        
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetTrigger("Endfall");
            grounded = true;
        }
            
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            animator.SetTrigger("Fall");
            grounded = false;
        }
    } 
    private void Anim(float moveInput)
    {
        if (moveInput != 0)
        {
            animator.SetBool("RunState", true);
        }
        else
        {
            animator.SetBool("RunState", false);
        }
    }
}
