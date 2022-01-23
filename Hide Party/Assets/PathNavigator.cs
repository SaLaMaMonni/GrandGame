using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNavigator : MonoBehaviour
{
    [Header("TARGET")]
    public string targetName = "None";
    public Vector3 targetPos;
    [Header("ACTIVE WAYPOINT")]
    public string WPName = "None";
    public Vector3 WPPos;
    [Header("WAYPOINTS")]
    public Waypoint[] WPList;

    public bool moving;


    public void ChangeTarget()
    {

    }
    private void FixedUpdate()
    {
        if (!moving)
        {
            WPList = Pathfinding.instance.GetWaypoints(this.gameObject);

            moving = true;
        }
        else
        {

        }

    }
}
