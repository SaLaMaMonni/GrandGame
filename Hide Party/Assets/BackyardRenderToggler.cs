using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackyardRenderToggler : MonoBehaviour
{

    public int yDirection;
    public int xDirection;
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
            if (yDirection != 0)
            {
                print("IT WAS PLAYER, THAT DIRTBAG!");
                print(input.movement.y);
                if (  sprite.sortingOrder == 10 && ( yDirection > 0 && input.movement.y > 0f || yDirection < 0 && input.movement.y < 0f ))
                {
                    print("GOING OUTSIDE.");
                    sprite.sortingOrder = 8;
                }


                else if (sprite.sortingOrder == 8 && (yDirection < 0 && input.movement.y > 0f || yDirection > 0 && input.movement.y < 0f))// && sprite.sortingOrder == 8)
                {
                    print("GOING INSIDE");
                    sprite.sortingOrder = 10;
                }
            }

            if (xDirection != 0)
            {
                print("IT WAS PLAYER, THAT DIRTBAG!");
                print(input.movement.y);
                if (sprite.sortingOrder == 10 && (xDirection > 0 && input.movement.x > 0f || xDirection < 0 && input.movement.x < 0f))
                {
                    print("GOING OUTSIDE.");
                    sprite.sortingOrder = 8;
                }


                else if (sprite.sortingOrder == 8 && (xDirection < 0 && input.movement.x > 0f || xDirection > 0 && input.movement.x < 0f))// && sprite.sortingOrder == 8)
                {
                    print("GOING INSIDE");
                    sprite.sortingOrder = 10;
                }
            }
        }
    }
}
