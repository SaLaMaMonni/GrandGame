using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data_Room
{
    public Collider2D trigger;
    public Transform[] foreground;
    public Transform[] background;
    public GameObject furniture;
    [HideInInspector]
    public List<SpriteRenderer> front;
    [HideInInspector]
    public List<SpriteRenderer> back;
}
