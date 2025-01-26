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
    public Vector2 direction;
    public void Start()
    {
        player = FindObjectOfType<playerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        direction = player.position - transform.position;
        RotateTowardsPlayer(direction);
        MoveTowardsPlayer(direction);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aaa");
    }

    public IEnumerator RotateTowardsPlayer(Vector2 direction)
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
}
