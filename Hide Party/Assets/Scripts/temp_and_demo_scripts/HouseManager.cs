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

    private void Awake()
    {

        foreach(Data_Room room in rooms)
        {
            room.hider.SetActive(true);

            //print(index+" "+room.hider.activeSelf);


            //print("Found "+room.front.Length+" front renderers.");
            foreach (SpriteRenderer backwall in room.back)
            {
                //print(foreground.material.color);
                backwall.material.color = Color.black;
            }

        }
    }
    public void Reveal(int selection)
    {
        Data_Room room = rooms[selection];
        room.hider.SetActive(false);

        foreach (SpriteRenderer rend in room.back)
        {
            rend.material.color = Color.white;
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
        //print("Room " + oldActive + "deactivated.");
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
        //print("Faded OUT room "+playerInRoom+".");
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
        //print("Faded IN room "+playerInRoom+".");
    }
}

