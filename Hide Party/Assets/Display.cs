using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    SpriteRenderer renderer;
    bool hidden = false;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }


    public void Hide()
    {
        if (!hidden)
        {
            Color col = renderer.color;
            col.a = 0f;
            renderer.color = col;
            hidden = true;
        }
    }

    public void Unhide()
    {
        if (hidden)
        {
            Color col = renderer.color;
            col.a = 1f;
            renderer.color = col;
            hidden = false;
        }
    }
}
