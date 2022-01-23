using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Controller : MonoBehaviour
{
    // Parts and references
    GameObject player;
    CurrentRoom playerRoom;
    Collider2D trigger;
    //HouseManager manager;

    // Attributes etc
    public int roomNumber = -1;
    bool playerInRoom = false;
    bool revealed = false;

    private void Awake()
    {
        trigger = GetComponent<Collider2D>();
        //manager = transform.parent.GetComponent<HouseManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRoom = player.GetComponent<CurrentRoom>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!playerInRoom && collision.gameObject == player)
        {
            playerInRoom = true;
            playerRoom.number = roomNumber;
            HouseManager.HM.Activate(roomNumber);

            if(!revealed)
            {
                HouseManager.HM.Reveal(roomNumber);
                revealed = true;
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
    }



}
