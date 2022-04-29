using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    public Animator squashStretchAnimator;

    private Rigidbody2D rb;

    private bool isGrounded;

    private int extraJumps;
    public int extraJumpValue;

    private bool facingRight = true;
    public Transform  groundCheck;
    public float checkRadius;
    public LayerMask whatIsGrounded;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;



    void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGrounded);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y); 

        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput <0)
        {
            Flip();
        }

        // Adds gravity to player
       // rb.velocity = new Vector2 (moveInput * speed, rb.velocity.y);
    }

    

    void Update()
    {


        if(isGrounded == true)
        {
            extraJumps = extraJumpValue;

        }
        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            squashStretchAnimator.SetTrigger("Jump"); // Start's Jump Animation

            extraJumps--;

            
        }
        else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
            squashStretchAnimator.SetTrigger("Jump"); // Start's Jump Animation

        }

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            
        }


        if(coyoteTimeCounter > 0f && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter =0f;
        }

         


    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

}
