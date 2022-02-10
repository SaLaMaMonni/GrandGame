using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    [SerializeField]
    private List<Data_Room> rooms;
    [SerializeField]
    private float fade_speed = 1f;
    private int playerInRoom;
    private int nextRoom = -1;
    private int previousRoom = -1;

    private GameObject player;

    public static HouseManager HM;
    public Sprite debugSprite;

    private void Awake()
    {
        if (HM != null && HM != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            HM = this;
        }

        foreach (Data_Room room in rooms)
        {
            room.furniture.SetActive(false);
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        int loopCounter = 0;
        foreach(Data_Room room in rooms)
        {
            //room.furniture.SetActive(false);
            if (room.foreground.Length > 0)
            {
                foreach(Transform f in room.foreground)
                {
                    int renCount = f.childCount;

                    for(int setRend = 0; setRend < renCount; setRend++)
                    {
                        SpriteRenderer activeRend = f.GetChild(setRend).GetComponent<SpriteRenderer>();
                        Color trans = activeRend.color;
                        trans.a = 1f;
                        activeRend.color = trans;
                        room.front.Add(activeRend);
                        //print("Set foreground part " + activeRend.name + " in room " + loopCounter);
                    }
                }
            }

            if (room.background.Length > 0)
            {
                foreach (Transform f in room.background)
                {
                    int renCount = f.childCount;

                    for (int setRend = 0; setRend < renCount; setRend++)
                    {
                        SpriteRenderer activeRend = f.GetChild(setRend).GetComponent<SpriteRenderer>();
                        activeRend.color = Color.black;
                        room.back.Add(activeRend);
                        //print("Set background part " + activeRend.name + " in room " + loopCounter);
                    }
                }
            }

            loopCounter++;
        }
    }

    public void Reveal(int selection)
    {
        Data_Room room = rooms[selection];

        room.furniture.SetActive(true);
        foreach (SpriteRenderer back in room.back)
        {
            //print("SHOWING "+back.gameObject.name+" "+back.transform.parent.parent.name);
            back.color = Color.white;
        }
    }

    public void Activate(int newActive)
    {
        playerInRoom = newActive;
        nextRoom = newActive;
        StartCoroutine("FadeOut");
    }

    public void Deactivate(int oldActive)
    {
        previousRoom = oldActive;
        StartCoroutine("FadeIn");
    }


    // TODO: SHIT TEMP CODE THAT NEEDS TO BE REPLACED
    IEnumerator FadeOut()
    {
        for (float ft = 1f; ft > 0.2; ft -= (0.02f * fade_speed * 100f * Time.deltaTime))
        {
            if (rooms[nextRoom].front.Count > 0)
            {
                //print("fadeout");
                foreach (SpriteRenderer rend in rooms[nextRoom].front)
                {
                    Color col = rend.color;
                    col.a = ft;
                    rend.color = col;
                }
            }

            yield return null;
        }
    }

    // TODO: DITTO, POOP BE HERE
    IEnumerator FadeIn()
    {
        for (float ft = 0.2f; ft < 1; ft += (0.02f * fade_speed * 100f * Time.deltaTime))
        {
            if (rooms[previousRoom].front.Count > 0)
            {
                //print("fadein");
                foreach (SpriteRenderer rend in rooms[previousRoom].front)
                {
                    Color col = rend.color;
                    col.a = ft;
                    rend.color = col;
                }
            }
            yield return null;
        }
    }

    public Collider2D[] GetRoomWaypoints(int userRoom)
    {
        Collider2D trigger = rooms[userRoom].trigger;

        Collider2D[] hits = Physics2D.OverlapBoxAll(trigger.bounds.center, trigger.bounds.size, 0f, LayerMask.GetMask("Waypoint"));

        foreach (Collider2D col in hits)
        {
            col.gameObject.GetComponent<SpriteRenderer>().sprite = debugSprite;
        }

        return hits;
    }
}

