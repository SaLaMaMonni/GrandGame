using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackyardRenderToggler : MonoBehaviour
{

    SpriteRenderer sprite;
    PlayerMovement input;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        sprite = player.GetComponent<SpriteRenderer>();
        input = player.GetComponent<PlayerMovement>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        print("SOMETHING HIT ME.");
        if(collision.gameObject.layer == 3)
        {
            print("IT WAS PLAYER, THAT DIRTBAG!");
            print(input.movement.y);
            if(input.movement.y > 0f && sprite.sortingOrder == 10)
            {
                print("GOING OUTSIDE.");
                sprite.sortingOrder = 8;
            }

            if(input.movement.y < 0f)// && sprite.sortingOrder == 8)
            {
                print("GOING INSIDE");
                sprite.sortingOrder = 10;
            }
        }
    }
}
