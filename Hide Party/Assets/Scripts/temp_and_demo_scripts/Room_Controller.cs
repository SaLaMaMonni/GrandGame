using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Controller : MonoBehaviour
{
    // Parts and references
    GameObject player;
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
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!playerInRoom && collision.gameObject == player)
        {
            playerInRoom = true;
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
