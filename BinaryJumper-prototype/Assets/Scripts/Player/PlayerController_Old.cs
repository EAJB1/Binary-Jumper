using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController_Old : MonoBehaviour
{
    public float speed = 17f;
    public float jumpForce = 15f;
    public float fallForce = 1.5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private float moveInputH;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private bool isGrounded;
    private bool isJumping;
    private bool jumpKeyHeld;
    //private Vector2 counterJumpForce;

    public static bool isDead = false;
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;

    //private int extraJumps;
    //public int extraJumpsValue;

    void Start()
    {
        isDead = false;
        //extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInputH = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInputH * speed, rb.velocity.y);

        // Flip the player when moving left and right
        if (facingRight == false && moveInputH > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInputH < 0)
        {
            Flip();
        }

        if (isGrounded)
        {
            isJumping = false;
        }

        // Once max jump hieght is reached, start falling.
        if (isJumping)
        {
            /* If jump key is not held and the player is actively moving up, add force down.
            if (!jumpKeyHeld && Vector2.Dot(rb.velocity, Vector2.up) > 0)
            {
                rb.AddForce(Vector2.down * rb.mass * Time.deltaTime);
            }*/
        }
    }

    void Update()
    {
        Jump();
        //JumpArc();
        Fall();
        //ExtraJumps();

        // If player is below the y value 3 then destroy object
        if (this.transform.position.y <= -10)
        {
            Destroy(gameObject); // Make particle effect on death
            isDead = true;
        }
    }

    void Flip()
    {
        // Flips scale of sprite. For example, scale.x = 4 would equal scale.x = -4.
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BinaryStorm")
        {
            Destroy(gameObject); // Make particle effect on death
            isDead = true;
        }
    }

    void Jump()
    {
        // If jump key is pressed, jump.
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            jumpKeyHeld = true;

            if (isGrounded)
            {
                isJumping = true;

                //rb.velocity = Vector2.up * jumpForce;
                rb.AddForce(Vector2.up * jumpForce * rb.mass, ForceMode2D.Impulse);
            }
        }
        // If jump key is released, fall.
        else if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            jumpKeyHeld = false;

            if (isJumping)
            {
                isJumping = false;
            }
        }

        // Smooth descent
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow) || !Input.GetKey(KeyCode.W))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Fall()
    {
        // If 'S' then go down
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) && !isGrounded)
        {
            rb.velocity = Vector2.down * fallForce;
            fallMultiplier = 5f;
        }
        else if (isGrounded)
        {
            fallMultiplier = 2.5f;
        }
    }

    /*void ExtraJumps()
    {
        // Setting up extra jumps
        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }*/
}