using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{

    BoxCollider2D roomTrigger;
    bool isActive; 
    TilemapRenderer floorTM;
    TilemapRenderer floorDTM;
    TilemapRenderer wallTM;
    Transform foreground;
    Transform furnObj;
    SpriteRenderer[] fgRenderer;
    SpriteRenderer[] furniture;
    public bool fogOfWar = false;
    public bool hideOnLeave = false;
    public bool remove = false;
    public bool shrink = false;
    public bool fade = false;
    [Range(0.01f,2f)]
    public float removal_speed = 1f;
    void Start()
    {
        int wallCount = foreground.childCount;
        fgRenderer = new SpriteRenderer[wallCount];
        int iter = 0;

        foreach(Transform child in foreground)
        {
            fgRenderer[iter] = child.GetComponent<SpriteRenderer>();
            iter++;
        }

        iter = 0;

        furnObj = transform.Find("Furniture");

        furniture = new SpriteRenderer[furnObj.childCount];

        foreach(Transform f in furnObj)
        {
            furniture[iter] = f.GetComponent<SpriteRenderer>();
            iter++;
        }


        if (fogOfWar)
        {
            floorTM.enabled = false;
            floorDTM.enabled = false;
            wallTM.enabled = false;

            foreach (SpriteRenderer r in furniture)
            {
                r.enabled = false;
            }
        }

        //       for (int i = 0; i < wallCount; i++)
        //       {
        //           fgRenderer[i] = foreground.GetChild(i).GetComponent<SpriteRenderer>();
        //       }
    }

    private void Awake()
    {
        roomTrigger = transform.GetComponent<BoxCollider2D>();
        isActive = false;
        foreground = transform.Find("Foreground");
        floorTM = transform.Find("Floors").GetComponent<TilemapRenderer>();
        floorDTM = transform.Find("Floors_det").GetComponent<TilemapRenderer>();
        wallTM = transform.Find("Walls").GetComponent<TilemapRenderer>();


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isActive && collision.gameObject.tag == "Player")
        {
            isActive = true;
            RemoveWalls();

            if (fogOfWar)
            {
                floorTM.enabled = true;
                floorDTM.enabled = true;
                wallTM.enabled = true;

                foreach (SpriteRenderer r in furniture)
                {
                    r.enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isActive && collision.gameObject.tag == "Player")
        {
            isActive = false;
            StartCoroutine("Restore");
        }
           
        if(hideOnLeave)
        {
            floorTM.enabled = false;
            floorDTM.enabled = false;
            wallTM.enabled = false;

            foreach (SpriteRenderer r in furniture)
            {
                r.enabled = false;
            }
        }
    }

    private void RemoveWalls()
    {
        if (remove)
        {
            foreach (SpriteRenderer renderer in fgRenderer)
            {
                renderer.enabled = false;
            }
        }
        else if (shrink)
        {
            StartCoroutine("Shrink");
        }
        else if (fade)
        {
            StartCoroutine("Fade");
        }
    }

    IEnumerator Fade()
    {
        for (float ft = 1f; ft > 0.2; ft -= (0.02f * removal_speed))
        {
            foreach (SpriteRenderer r in fgRenderer)
            {
                Color c = r.material.color;
                c.a = ft;
                r.material.color = c;
                yield return null;
            }

        }
    }

    IEnumerator Shrink()
    {
        for (float size = 1; size > 0f; size -= (0.01f * removal_speed))
        {
            foreground.localScale = new Vector3(1f, size);
            yield return null;
        }
        foreground.localScale = new Vector2(1f, 0f);
    }

    IEnumerator Restore()
    {
        if(fade)
        {
            for (float ft = 0f; ft < 1; ft += (0.02f * removal_speed))
            {
                foreach (SpriteRenderer r in fgRenderer)
                {
                    Color c = r.material.color;
                    c.a = ft;
                    r.material.color = c;
                    yield return null;
                }

            }
        }
        else if (shrink)
        {
            for (float size = 0; size < 1f; size += (0.01f * removal_speed))
            {
                foreground.localScale = new Vector3(1f, size);
                yield return null;
            }
            foreground.localScale = new Vector2(1f, 1f);
        }
        else
        {
            foreach (SpriteRenderer renderer in fgRenderer)
            {
                renderer.enabled = true;
            }
        }
    }
}


