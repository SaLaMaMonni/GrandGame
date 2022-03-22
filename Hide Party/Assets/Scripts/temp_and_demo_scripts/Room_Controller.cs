using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Controller : MonoBehaviour
{
    // Parts and references
    GameObject player;
    CurrentRoom playerRoom;
    Collider2D trigger;
    SpriteRenderer[] floorsGraphics;
    ContactFilter2D npcs;
    //SpriteRenderer floor;
    //HouseManager manager;
    List<Display> hidden;

    // Attributes etc
    public int roomNumber = -1;
    bool playerInRoom = false;
    bool revealed = false;

    private void Awake()
    {
        //floor = GetComponent<SpriteRenderer>();
        trigger = GetComponent<Collider2D>();
        //manager = transform.parent.GetComponent<HouseManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRoom = player.GetComponent<CurrentRoom>();
        //floor.color = Color.black;

        int loops = transform.parent.childCount;
        floorsGraphics = new SpriteRenderer[loops];

        for (int fl = 0; fl < loops; fl++)
        {
            SpriteRenderer floorSprite = transform.parent.GetChild(fl).GetComponent<SpriteRenderer>();

            if (floorSprite != null)
            {
                floorSprite.color = Color.black;
            }

            floorsGraphics[fl] = floorSprite;

        }
        npcs = new ContactFilter2D();
        //floor.sortingOrder = 100;
    }

    private void Start()
    {

        LayerMask npcMask = new LayerMask();
        //npcMask |= (1 << LayerMask.NameToLayer("Crowd"));
        npcMask |= (1 << LayerMask.NameToLayer("NPC"));
        npcs.layerMask = npcMask;
        npcs.useTriggers = false;

        //hidden = new List<Display>();

        Hide();
    }

    private void Hide()
    {
        List<Collider2D> inRoom = new List<Collider2D>();
        trigger.OverlapCollider(npcs, inRoom);

        foreach (Collider2D found in inRoom)
        {
            Display hider = found.GetComponent<Display>();

            if (hider != null)
            {
                hider.Hide();
                //hidden.Add(hider);
            }
        }
    }

    private void Reveal()
    {
        List<Collider2D> inRoom = new List<Collider2D>();
        trigger.OverlapCollider(npcs, inRoom);

        foreach (Collider2D found in inRoom)
        {
            Display hider = found.GetComponent<Display>();

            if (hider != null)
            {
                hider.Unhide();
                //hidden.Add(hider);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerInRoom && collision.gameObject == player)
        {
            playerInRoom = true;
            playerRoom.number = roomNumber;
            HouseManager.HM.Activate(roomNumber);

            if (!revealed)
            {
                foreach (SpriteRenderer flo in floorsGraphics)
                {
                    flo.color = Color.white;
                    //floor.sortingOrder = 0;
                }

                Reveal();

                HouseManager.HM.Reveal(roomNumber);

                revealed = true;
            }
        }
        else if (collision.gameObject.tag == "NPC")
        {
            Display view = collision.GetComponent<Display>();
            
            if (revealed)
            {
                view.Unhide();
            }
            else
            {
                view.Hide();
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerInRoom && collision.gameObject == player)
        {
            playerInRoom = false;
            HouseManager.HM.Deactivate(roomNumber);
        }
        //else if (collision.gameObject.tag == "NPC" && !revealed)
        //{
        //    SpriteRenderer npc = collision.gameObject.GetComponent<SpriteRenderer>();
        //    npc.color = new Color(1f, 1f, 1f, 1f);
        //}
    }
}
