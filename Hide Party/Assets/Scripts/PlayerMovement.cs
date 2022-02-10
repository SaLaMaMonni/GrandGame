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

    private Animator animator;

    private DialogueManager dManager;


    private int uselessTestInt = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dManager = FindObjectOfType<DialogueManager>();
    }

    // Handles the input processing
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //uselessTestInt++;
        //if (uselessTestInt > 200)
        //{
        //    uselessTestInt = 0;
        //    RaycastHit2D hit = Physics2D.Linecast(transform.position, Input.mousePosition);
        //    if (hit != null)
        //    {
        //        print(hit.collider.name);
        //    }
        //}
    }

    // Moves the character according to the input
    private void FixedUpdate()
    {
        if (dManager != null)
        {
            if (!DialogueManager.instance.isTalking && !GameManager.Instance.isInteracting)
            {
                Move();
                UpdateAnimations();
            }
        }
        else
        {
            Move();
            UpdateAnimations();
        }
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

    void UpdateAnimations()
    {
        if (movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);

            if (movement.x < 0)
            {
                animator.SetBool("isLeft", true);
                animator.SetBool("isRight", false);
            }
            else if (movement.x > 0)
            {
                animator.SetBool("isLeft", false);
                animator.SetBool("isRight", true);
            }
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
