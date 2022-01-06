using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogInteraction : Interactable
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        Pet();
    }

    // Causes the player to pet the dog, relieving their stress.
    // Won't last forever, dog will stop the petting moment and walk somewhere else.
    void Pet()
    {
        Debug.Log("You pet the dog, such happiness");
    }

    // Dog will move somewhere else after petting is done.
    // Maybe could choose the next location randomly from waypoint list? Teleport there when it exits player's view?
    void MoveElsewhere()
    {

    }
}
