using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldownTime = 3f;
    public float friction = 0.9f;
    public BubbleController bubbleController;
    public staminaView staminaView;
    public SpriteRenderer playerRenderer; 

    public Rigidbody2D rb;
    private bool dashCooldown;
    private bool isDashing;
    public bool isInvincible;
    public bool isAlive = true;
    private Vector2 dashDirection;
    private Vector2 movementDirection;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isAlive)
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
    }

    private void FixedUpdate()
    {
        if (isAlive)
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
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashCooldown = true;
        isInvincible = true;
        bubbleController.multiplierActive = true;

        rb.velocity = Vector2.zero;

        StartCoroutine(ChangeOpacity());

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        bubbleController.multiplierActive = false;

        StopCoroutine(ChangeOpacity());

        StartCoroutine(staminaView.barRefill(dashCooldownTime));
        yield return new WaitForSeconds(dashCooldownTime);

        dashCooldown = false;
        yield return new WaitForSeconds(0.3f);
        isInvincible = false;
    }

    private IEnumerator ChangeOpacity()
    {
        float time = 0f;
        while (isInvincible)
        {
            float alpha = Mathf.PingPong(time, 1f) * 0.5f + 0.5f; 
            Color color = playerRenderer.color;
            color.a = alpha;
            playerRenderer.color = color;

            time += Time.deltaTime;

            yield return null;
        }

        Color finalColor = playerRenderer.color;
        finalColor.a = 1f;
        playerRenderer.color = finalColor;
    }

    public IEnumerator InvincibilityAfterHit()
    {
        isInvincible = true;
        StartCoroutine(ChangeOpacity());
        yield return new WaitForSeconds(1.5f);
        isInvincible = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlive)
        {
            if (collision.gameObject.CompareTag("basicBubble"))
            {
                Destroy(collision.gameObject);
                bubbleController.regenerateBubble(1);
            }
            if (collision.gameObject.CompareTag("varyingBubble"))
            {
                if (collision.gameObject.GetComponent<VaryingSizeBubbleBehaviour>().level == 1)
                {
                    bubbleController.regenerateBubble(1);

                    Destroy(collision.gameObject);
                }
                if (collision.gameObject.GetComponent<VaryingSizeBubbleBehaviour>().level == 2)
                {
                    bubbleController.regenerateBubble(2);

                    Destroy(collision.gameObject);
                }
                else if (collision.gameObject.GetComponent<VaryingSizeBubbleBehaviour>().level == 3)
                {
                    bubbleController.regenerateBubble(3);

                    Destroy(collision.gameObject);
                }
                else if (collision.gameObject.GetComponent<VaryingSizeBubbleBehaviour>().level == 4)
                {
                    bubbleController.regenerateBubble(4);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
