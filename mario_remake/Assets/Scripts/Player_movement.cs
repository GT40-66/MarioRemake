using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_movement : MonoBehaviour
{

    public float speed = 5f;
    public float jumpstrength = 5f;
    private Rigidbody2D rb;
    private float movementDirection;
    public float moveVelocity;
    private bool isGrounded;
    public  bool facingRight = true;
    

    // Start is called before the first frame update
    void Start()
    {
        // needed to get the rigid body component of the player character to initiate movement
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

    }


    void PlayerMove()
    {
       
         // Allows player to use "a" and "d" to control horizontal movement
        movementDirection = Input.GetAxis("Horizontal");
        moveVelocity = movementDirection * speed;

        //Checks for the jump input "spacebar"
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpstrength); // Apply jumpforce
        }

        //PLAYER DIRECTION
        if (movementDirection < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if (movementDirection > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
    }

    void FixedUpdate()
    {
        // applies speed to the player character
        rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // checks if the player collides with ground
        {
            isGrounded = true; // Set isGrounded to true
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // checks if the player exits ground
        {
            isGrounded = false; // Set isGrounded to false
        }
    }

    void FlipPlayer()
    {
        //Flips player to right
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale; // created new Vector 2 variable called localScale
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
