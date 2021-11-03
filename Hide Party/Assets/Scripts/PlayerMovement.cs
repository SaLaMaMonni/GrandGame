using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 10f;
    private Vector2 movement;

    public bool isSlowed;

    [Tooltip("The percent for how slow the player moves compared to normal speed")] [Range(0f, 1f)]
    public float slowedPercent = 0.5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Handles the input processing
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // Moves the character according to the input
    private void FixedUpdate()
    {
        Move();
    }

    // Base method for movement
    void Move()
    {
        if (!isSlowed)
        {
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.MovePosition(rb.position + movement.normalized * (moveSpeed * slowedPercent) * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            isSlowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            isSlowed = false;
        }
    }
}
