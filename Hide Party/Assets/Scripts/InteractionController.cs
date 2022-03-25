using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private Interactable focus;
    public float focusRemover = 1f;

    void Update()
    {
        /*
        if (focus != null)
        {
            Vector3 focusPos = focus.interactionTransform.transform.position;
            float distance = Vector3.Distance(transform.position, focusPos);

            if (distance > focusRemover)
            {
                focus = null;
                Debug.Log("Removed focus");
            }
        }
        */
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (focus != null)
            {
                Debug.Log("Focus is not null");
                if (!DialogueManager.instance.isTalking)
                {
                    focus.Interact();
                }
            }
            else
            {
                Debug.Log("Nothing to focus on");
            }
        }
    }

    public void SetFocus(GameObject newFocus)
    {
        focus = newFocus.GetComponent<Interactable>();
    }

    public void RemoveFocus()
    {
        focus = null;
    }
}
