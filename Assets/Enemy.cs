using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int difficulty = 1;
    private Rigidbody2D rb;
    Transform player;
    [SerializeField] float Speed = 2;
    public void Start()
    {
        player = FindObjectOfType<playerMovement>().transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        transform.position += (Vector3)direction.normalized * difficulty * Time.deltaTime * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aaa");
    }
}
