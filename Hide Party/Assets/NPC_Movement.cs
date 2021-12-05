using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{

    Vector2 targetPos;

    public bool getTarget = false;


    private void Update()
    {
        if (getTarget)
        {
            GetValidWaypoint();
            getTarget = false;
        }
    }


    private void GetValidWaypoint()
    {

    }
}
