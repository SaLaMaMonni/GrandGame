using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding instance;
    Waypoint[] allWaypoints;
    Queue<Waypoint> process;
    Dictionary<Waypoint,Waypoint> paths;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            int waypointCount = transform.GetChildCount();
            allWaypoints = new Waypoint[waypointCount];

            for(int iWP = 0; iWP < waypointCount; iWP++)
            {
                allWaypoints[iWP] = transform.GetChild(iWP).GetComponent<Waypoint>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public Waypoint[] GetWaypoints(GameObject target)
    {
        int targetRoom = target.GetComponent<CurrentRoom>().number;

        Waypoint startWP = GetClosestWaypoint(targetRoom, target);
        if (startWP != null)
        {
            // OTA KERRALLA ALKU JA LOPPU JA VAIHDA NIIN ETTÄ KU NPC LIIKKUU NI SE TRIGGERONENTER MUUTTAA CURRENTROOMI.
        }
        Waypoint[] points = new Waypoint[0];

        return points;
    }

    private Waypoint GetClosestWaypoint(int targetRoom, GameObject target)
    {
        float closest = 999999f;
        int closestIndex = -1;

        int index = 0;
        foreach(Waypoint wp in allWaypoints)
        {
            if(wp.room == targetRoom)
            {
                float dist = Vector3.Distance(wp.transform.position, target.transform.position);
                if(dist < closest)
                {
                    closest = dist;
                    closestIndex = index;
                }
            }
            index++;
        }

        if (index > 0)
        {
            return allWaypoints[index];
        }
        else
        {
            return null;
        }
    }
}
