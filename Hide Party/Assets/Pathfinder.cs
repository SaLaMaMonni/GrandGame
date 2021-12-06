using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Vector2[] wPos;
    int wCount;

    public bool debuggy = false;
    // Start is called before the first frame update
    void Start()
    {
        wCount = transform.childCount;
        wPos = new Vector2[wCount];

        for(int i = 0; i < wCount; i++)
        {
            wPos[i] = transform.GetChild(i).transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (debuggy)
        {
            foreach(Vector2 pos in wPos)
            {
                print(pos);
            }

            debuggy = false;
        }
    }

    public void GetPath(Vector2 start,Vector2 end)
    {
        // ETI LÄHIMMÄT WAYPOINTIT
        //PÄIVITÄ KU HUONEET VAIHTUU (TODO?)
        int startWP = -1;
        int endWP = -1;

        float startDist = 10000f;
        float endDist = 10000f;

    }
}
