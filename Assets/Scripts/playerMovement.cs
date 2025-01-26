using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f; // Duraci�n del dash
    public float dashCooldownTime = 3f;
    public float friction = 0.9f;
    public BubbleController bubbleController;
    public staminaView staminaView;

    private Rigidbody2D rb;
    private bool dashCooldown;
    private bool isDashing;
    private bool isInvincible;
    private Vector2 dashDirection;
    private Vector2 movementDirection;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isDashing)
        {
            movementDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
            {
                movementDirection += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                movementDirection += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                movementDirection += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                movementDirection += Vector2.right;
            }

            if (movementDirection != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, smoothedAngle);
            }
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !dashCooldown && !isDashing)
        {
            if (movementDirection != Vector2.zero)
            {
                dashDirection = movementDirection.normalized;
                StartCoroutine(Dash());
            }

        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = dashDirection * dashSpeed;
        }
        else if (movementDirection != Vector2.zero)
        {
            rb.velocity = movementDirection.normalized * speed;
        }
        else
        {
            rb.velocity *= friction;
        }
    }



private IEnumerator Dash()
    {
        isDashing = true;
        dashCooldown = true;
        isInvincible = true;
        bubbleController.multiplierActive = true;

        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        bubbleController.multiplierActive = false;

        StartCoroutine(staminaView.barRefill(dashCooldownTime));
        yield return new WaitForSeconds(dashCooldownTime);

        dashCooldown = false;
        yield return new WaitForSeconds(1);
        isInvincible = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            Debug.Log("Player collided with a wall!");

            // Prevent further movement by zeroing out velocity
            rb.velocity = Vector2.zero;

            // Get the point of contact and adjust the position to stay within bounds
            ContactPoint2D contactPoint = collision.contacts[0];
            Vector2 correctionDirection = contactPoint.normal; // Normal points away from the wall

            // Slightly push the player away from the wall to prevent overlapping
            transform.position = (Vector2)transform.position + correctionDirection * 0.01f;

            // If dashing, stop the dash
            if (isDashing)
            {
                StopCoroutine(Dash());
                isDashing = false;
            }
        }
    }

}
