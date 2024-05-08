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
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("DESTROY");
            Destroy(gameObject);
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

