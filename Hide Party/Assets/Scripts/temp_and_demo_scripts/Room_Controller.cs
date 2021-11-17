using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Controller : MonoBehaviour
{
    // Parts and references
    GameObject hider;
    GameObject player;
    SpriteRenderer wallRenderer;

    // REMAKE STARTS HERE
    public int roomNumber = -1;
    //public bool alwaysShowFront = false;
    private HouseManager manager;

    // Attributes etc.
    //public float fade_speed = 10f;
    //public float fadeDisplay = -1f;
    //bool moreThanOneWall = false;
    bool playerInRoom = false;
    //bool hidden;

    // REMREMREMAKE
    bool revealed = false;

    private void Awake()
    {
        manager = transform.parent.GetComponent<HouseManager>();
        //
        //hider = transform.Find("hider").gameObject;
        //wallRenderer = transform.GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        //hider.SetActive(true);
    }

    private void Start()
    {
        /*
        if (alwaysShowFront)
        {
            wallRenderer.material.color = Color.white;
        }
        */

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!playerInRoom && collision.gameObject == player)
        {
            playerInRoom = true;
            //StartCoroutine("FadeOut");

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
            //StartCoroutine("FadeIn");
            //print("Should deactivate.");
            manager.Deactivate(roomNumber);
        }
    }

    // TODO: SHIT TEMP CODE THAT NEEDS TO BE REPLACED
    //IEnumerator FadeOut()
    //{
    //    for (float ft = 1f; ft > 0.2; ft -= (0.02f * fade_speed * Time.deltaTime))
    //    {
    //        Color c = wallRenderer.material.color;
    //        c.a = ft;
    //        wallRenderer.material.color = c;
    //        fadeDisplay = ft;

    //        yield return null;
    //    }
    //    print("Done.");
    //}

    //// TODO: DITTO, POOP BE HERE
    //IEnumerator FadeIn()
    //{
    //    for (float ft = 0.2f; ft < 1; ft += (0.02f * fade_speed * Time.deltaTime))
    //    {
    //        Color c = wallRenderer.material.color;
    //        c.a = ft;
    //        wallRenderer.material.color = c;
    //        fadeDisplay = ft;

    //        yield return null;
    //    }
    //    print("Done.");
    //}
}
