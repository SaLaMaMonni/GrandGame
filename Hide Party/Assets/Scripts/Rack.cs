using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rack : Interactable
{
    Inventory inventory;

    [SerializeField] Item neededItem;
    bool hasNeededItem;

    public AudioClip interactSound;

    void Start()
    {
        inventory = Inventory.instance;
    }

    public override void Interact()
    {
        base.Interact();

        if (!hasNeededItem)
        {
            TakeJacket();
        }
    }

    void TakeJacket()
    {
        for (int i = 0; i < inventory.items.Count; i++)
        {
            Item item = inventory.items[i];

            if (item == neededItem && !hasNeededItem)
            {
                inventory.Remove(item);
                hasNeededItem = true;
                Debug.Log("Removed " + item.name);

                GameManager.Instance.HideMatti();

                textMesh.gameObject.SetActive(false);
                canInteract = false;
                enabled = false;

                return;
            }
            else
            {
                Debug.Log("You can't give a jacket to me?");
            }
        }
    }

    public void PlayPickUpSound()
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        source.PlayOneShot(interactSound);
    }
}
