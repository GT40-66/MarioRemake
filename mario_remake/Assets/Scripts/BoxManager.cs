using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 originalPosition;
    private bool isHit = false;

    public float upwardForce = 100f;
    public float returnDelay = 1f;
    public float returnSpeed = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isHit)
        {
            Debug.Log("Player hit the box from below");
            rb.bodyType = RigidbodyType2D.Dynamic; // Change the body type to "Dynamic"
            rb.AddForce(Vector2.up * upwardForce); // Apply upward force to the box
            isHit = true;

            StartCoroutine(ReturnToOriginalPosition());
        }
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(returnDelay);

        while (transform.position.y > originalPosition.y)
        {
            float step = returnSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, step);
            yield return null;
        }

        // Ensure the box reaches its original position precisely
        transform.position = originalPosition;
        rb.bodyType = RigidbodyType2D.Kinematic; // Change the body type back to "Kinematic"
        isHit = false;
    }
}
