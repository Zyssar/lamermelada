using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int difficulty = 1;
    public Rigidbody2D rb;
    public Transform player;
    [SerializeField] public float Speed = 2;
    [SerializeField] public float rotationSpeed = 1f;
    [SerializeField] public int damage = 2;
    public BubbleController bubbleController;
    public Vector2 direction;

    public void Start()
    {
        bubbleController = FindObjectOfType<BubbleController>();
        player = FindObjectOfType<playerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        direction = player.position - transform.position;
        RotateTowardsDirection(direction);
        MoveTowardsPlayer(direction);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collision.gameObject.GetComponent<playerMovement>().isInvincible)
        {
            Debug.Log("Contacto con jugador");

            // Activar invulnerabilidad en el jugador por 1.5 segundos
            StartCoroutine(collision.gameObject.GetComponent<playerMovement>().InvincibilityAfterHit());

            // Llamar al daño
            StartCoroutine(bubbleController.damageBubble(damage));
        }
    }

    public IEnumerator RotateTowardsDirection(Vector2 direction)
    {
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(rb.rotation, targetAngle)) > 0.1f)
        {
            float angle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.deltaTime * rotationSpeed);
            rb.rotation = angle;

            yield return null;
        }
        rb.rotation = targetAngle;
    }

    public void MoveTowardsPlayer(Vector2 direction)
    {
        transform.position += (Vector3)direction.normalized * difficulty * Time.deltaTime * Speed;
    }

    public IEnumerator exitScreen()
    {
        Vector2 exitDirection = Vector2.zero;
        int rollDir;

        while (exitDirection == Vector2.zero)
        {
            rollDir = Random.Range(-1, 1);
            Vector2 upDown = Vector2.up * rollDir;
            rollDir = Random.Range(-1, 1);
            Vector2 rightLeft = Vector2.right * rollDir;
            exitDirection = upDown + rightLeft;
        }
        float distanceMoved = 0f;
        StartCoroutine(RotateTowardsDirection(exitDirection));
        while (true)
        {
            float moveAmount = Speed * Time.deltaTime;
            rb.position += exitDirection * moveAmount;
            distanceMoved += moveAmount;

            if (distanceMoved >= 20f) // Adjust this to how far you want the object to move
            {
                Destroy(gameObject);
                yield break;
            }

            yield return null;
        }
    }
}
