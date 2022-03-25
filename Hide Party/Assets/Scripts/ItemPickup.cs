using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;
    public AudioClip pickUpSound;

    // Called when player interacts with an item
    public override void Interact()
    {
        PickUp();
    }

    // Adds the item to player's inventory and deletes it from scene.
    // If the item can't be picked up (inventory full), nothing happens to the game object
    void PickUp()
    {
        Debug.Log("Picking up: " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);

        if(wasPickedUp)
        {
            PlayPickUpSound();
            Destroy(gameObject);
        }
    }

    public void PlayPickUpSound()
    {
        AudioSource source = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AudioSource>();

        //AudioSource source = gameObject.GetComponent<AudioSource>();

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.PlayOneShot(pickUpSound);
    }
}
