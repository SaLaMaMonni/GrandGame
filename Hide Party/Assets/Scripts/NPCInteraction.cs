using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : Interactable
{
    [SerializeField] Item neededItem;
    private bool hasNeededItem = false;

    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    public override void Interact()
    {
        base.Interact();
        GiveItem();
    }

    // Checks if the player has the item the NPC needs and if so, gives it to them 
    // and removes it from the inventory.
    // Also checks if the NPC already has the item or not, in case there are duplicates in the game
    void GiveItem()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            Item item = inventory.items[i];

            if (item == neededItem && !hasNeededItem)
            {
                inventory.Remove(item);
                hasNeededItem = true;
                Debug.Log("Removed " + item.name);
                return;
            }
            else
            {
                Debug.Log("You don't have the right item or I already have that item");
            }
        }
    }
}
