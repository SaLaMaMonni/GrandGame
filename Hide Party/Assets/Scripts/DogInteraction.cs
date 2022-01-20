using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteraction : Interactable
{
    private Animator animator;

    public float speed;
    private bool onTheMove = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (onTheMove)
        {
            Vector3 move = transform.position;
            move.x -= transform.position.x * speed * Time.deltaTime;
            
            transform.position = move;
        }
    }

    public override void Interact()
    {
        base.Interact();

        StartPetting();
    }

    public override void OnBecameInvisible()
    {
        base.OnBecameInvisible();

        onTheMove = false;
    }

    // Causes the player to pet the dog, relieving their stress.
    // Won't last forever, dog will stop the petting moment and walk somewhere else.
    void StartPetting()
    {
        animator.SetTrigger("pet");
        Debug.Log("You pet the dog, such happiness");
    }

    // Dog will move somewhere else after petting is done.
    public void StartMovingAway()
    {
        onTheMove = true;
    }

    void TeleportAway()
    {

    }

    private Vector3 RandomPosition()
    {
        Vector3 randomPos = Vector3.zero;

        return randomPos;
    }
}
