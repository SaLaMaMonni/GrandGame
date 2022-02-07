using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{
    SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }


    public void Hide()
    {
        Color col = renderer.color;
        col.a = 0f;
        renderer.color = col;
    }

    public void Unhide()
    {
        Color col = renderer.color;
        col.a = 1f;
        renderer.color = col;
    }
}
