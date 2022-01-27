using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : Interactable
{
    [SerializeField] Item neededItem;
    private bool hasNeededItem = false;

    private Inventory inventory;
    private DialogueTrigger dialogueTrigger;
    private DialogueTrigger[] dialogueTriggers;

    public bool canGiveItem = false;
    public bool canTalk = true;

    public int interactionPhase = 0;

    // For quests
    public int questId;
    public bool isQuestOpen = true;
    public string questDescription;

    private void Start()
    {
        inventory = Inventory.instance;
        dialogueTriggers = GetComponents<DialogueTrigger>();
    }

    // Checks whether the player can start the NPC's quest or not and acts upon it.
    public override void Interact()
    {
        if (isQuestOpen)
        {
            CheckAction();
        }
        else
        {
            dialogueTriggers[dialogueTriggers.Length - 1].TriggerDialogue();
        }
    }

    void CheckAction()
    {
        // Check which dialogue etc should be tirggered when player interacts with the NPC
        // Maybe use phase count system or something like that?
        // For example, if player hasn't talked with them yet, display phase 0 material

        switch (interactionPhase)
        {
            case 0:
                dialogueTriggers[interactionPhase].TriggerDialogue();
                canGiveItem = true;
                interactionPhase = 1;
                break;
            case 1:
                GiveItem();
                break;
            case 3:
                dialogueTriggers[interactionPhase].TriggerDialogue();
                break;
            default:
                Debug.Log("Something is wrong, you ain't supposed to see me!");
                break;
        }
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

                canGiveItem = false;

                interactionPhase = 2;
                dialogueTriggers[interactionPhase].TriggerDialogue();
                interactionPhase = 3;

                if (questId != 0)
                {
                    QuestManager.Instance.OpenNextQuest();
                }

                return;
            }
            else
            {
                Debug.Log("You don't have the right item or I already have that item");
            }
        }

        // If the player doesn't have the correct item, trigger this dialogue
        dialogueTriggers[interactionPhase].TriggerDialogue();
    }

    public void GiveItemRemotely()
    {
        interactionPhase = 2;

        QuestManager.Instance.OpenNextQuest();
    }
}
