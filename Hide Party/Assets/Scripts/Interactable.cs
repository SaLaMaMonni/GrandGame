using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] public float radius = 2f;
    [SerializeField] private TextMeshPro textMesh;

    private InteractionController pcController;
    public bool canInteract;

    void Update()
    {
        CheckRadius();
    }

    // Checks whether a player character is inside the radius or not and acts upon it
    void CheckRadius()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero);

        if (hit)
        {
            pcController = hit.collider.GetComponent<InteractionController>();

            if (pcController != null)  // Was the hit caused by the player and if so, do stuff
            {
                textMesh.gameObject.SetActive(true);
                canInteract = true;

                // Gives the player a chance to interact with it
                pcController.SetFocus(gameObject);
            }
            else
            {
                textMesh.gameObject.SetActive(false);
                canInteract = false;
            }
        }
        else if (pcController != null)  // Checks if the ray isn't hitting the object anymore
        {
            textMesh.gameObject.SetActive(false);
            canInteract = false;

            // Prevents the player from interacting with the object after leaving the ray cast area
            pcController.RemoveFocus();
            pcController = null;
        }
        else
        {
            textMesh.gameObject.SetActive(false);
            canInteract = false;
        }

    }

    public void Interact()
    {
        if (canInteract)
        {
            // Do the interaction
            Debug.Log("Hey, you interacted with me!");
        }
        else
        {
            Debug.Log("You don't have a permission to interact with me");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
