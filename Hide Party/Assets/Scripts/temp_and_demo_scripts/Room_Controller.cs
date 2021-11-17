using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Controller : MonoBehaviour
{
    // Parts and references
    GameObject player;
    HouseManager manager;

    // Attributes etc
    public int roomNumber = -1;
    bool playerInRoom = false;
    bool revealed = false;

    private void Awake()
    {
        manager = transform.parent.GetComponent<HouseManager>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!playerInRoom && collision.gameObject == player)
        {
            playerInRoom = true;
            manager.Activate(roomNumber);

            if(!revealed)
            {
                manager.Reveal(roomNumber);
                revealed = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (playerInRoom && collision.gameObject == player)
        {
            playerInRoom = false;
            manager.Deactivate(roomNumber);
        }
    }

}
