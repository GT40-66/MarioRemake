using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int EnemySpeed;
    public int XMoveDirection;
    private bool isGrounded = true; // Assume enemy starts grounded

    void Update()
    {
        if (isGrounded)
        {
            // Move Horizontally
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection * EnemySpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);

            // Check for obstacle and flip direction
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    Flip();
                }
   
            }
        }

        if (gameObject.transform.position.y < -8.1f)
        {
            Destroy(gameObject);
        }
    }

    void Flip()
    {
        XMoveDirection *= -1; // Simplified flip direction
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
            // Check if the collision point is close to the top of the enemy
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Calculate the y-coordinate of the top of the enemy's Collider
            float enemyTopY = transform.position.y + GetComponent<Collider2D>().bounds.extents.y;

            // Check if the collision point is within a small margin of the top of the enemy
            if (contact.point.y >= enemyTopY - 0.1f) // Adjust the margin as needed
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player collided with enemy");

                // Calculate the direction in which the sprite should fly away
                    Vector2 flyAwayDirection = (transform.position - collision.transform.position).normalized;

                // Apply force to the enemy in the calculated direction
                    Rigidbody2D rb = GetComponent<Rigidbody2D>();
                    rb.AddForce(flyAwayDirection * 500f); // Adjust the force as needed

                    // Disable the box collider
                    Collider2D collider = GetComponent<Collider2D>();
                    if (collider != null)
                    {
                        collider.enabled = false;
                    }

                    // Disable freeze rotation
                    rb.freezeRotation = false;
                    // Destroy the enemy after a delay
                    Destroy(gameObject, 0.5f); // Adjust the delay as needed
                    //Destroy(gameObject);
                }
            }
        
            else // If collision occurs on the sides of the enemy
            {
                if (collision.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player collided with enemy's side");
                    Destroy(collision.gameObject); // Destroy the player object
                }
            }
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

