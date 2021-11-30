using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private Interactable focus;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (focus != null)
            {
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
