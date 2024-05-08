using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public int EnemySpeed;
    public int XMoveDirection;
    private bool isDead = false;
    public bool isGrounded = true;
    
    // Update is called once per frame
    void Update()
    {
        if (isGrounded) // only move if the enemy is grounded
        {
            // Move Horizontally
             gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection * EnemySpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

            // Check for obstacle and flip direction
            RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2 (XMoveDirection, 0));
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    Flip ();
                    
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    Destroy (hit.collider.gameObject);                
                }
            }
        }

        if ( gameObject.transform.position.y < -8.1){ // if the enemy falls below y= -8.1, the enemy dies
            Destroy (gameObject);
        }
    }

    void Flip () 
    {
        if (XMoveDirection > 0)
        {
            XMoveDirection = -1;
        }
        else
        {
            XMoveDirection = 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

  

  
}
