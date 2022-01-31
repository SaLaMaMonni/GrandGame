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
            room.hider.SetActive(true);

            foreach (SpriteRenderer backwall in room.back)
            {
                //backwall.material.color = Color.black;
            }
        }
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Reveal(int selection)
    {
        Data_Room room = rooms[selection];
        room.hider.SetActive(false);

        foreach (SpriteRenderer rend in room.back)
        {
            //print(rend.gameObject.name);
            //rend.material.color = Color.white;
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
            if (rooms[nextRoom].front.Length > 0)
            {
                foreach (SpriteRenderer rend in rooms[nextRoom].front)
                {
                    Color col = rend.material.color;
                    col.a = ft;
                    rend.material.color = col;
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
            if (rooms[previousRoom].front.Length > 0)
            {
                foreach (SpriteRenderer rend in rooms[previousRoom].front)
                {
                    Color col = rend.material.color;
                    col.a = ft;
                    rend.material.color = col;
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

