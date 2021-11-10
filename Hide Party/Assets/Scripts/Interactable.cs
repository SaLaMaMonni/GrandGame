using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interactable : MonoBehaviour
{
    [SerializeField] public float radius = 2f;
    [SerializeField] private TextMeshPro textMesh;

    public bool canInteract;

    void Update()
    {
        CheckRadius();
    }

    // Checks whether a player character is inside the radius or not and acts upon it
    void CheckRadius()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, Vector2.zero);

        if (hit == false)
        {
            textMesh.gameObject.SetActive(false);
            return;
        }

        if (hit.collider.gameObject.layer == 3)
        {
            textMesh.gameObject.SetActive(true);
            canInteract = true;
        }
        else
        {
            textMesh.gameObject.SetActive(false);
            canInteract = false;
        }
    }

    void Interact()
    {
        // Do the interaction
        Debug.Log("Hey, you interacted with me!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
