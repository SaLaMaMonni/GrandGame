using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    Waypoint[] allWaypoints;
    ContactFilter2D filter;


    private void Awake()
    {
        int waypointCount = transform.childCount;
        allWaypoints = new Waypoint[waypointCount];

        for (int iWP = 0; iWP < waypointCount; iWP++)
        {
            allWaypoints[iWP] = transform.GetChild(iWP).GetComponent<Waypoint>();
        }

        filter.useTriggers = false;
    }

    public List<Waypoint> GetWaypoints(GameObject actor,GameObject target)
    {
        Queue<Waypoint> process = new Queue<Waypoint>();
        Dictionary<Waypoint, Waypoint> paths = new Dictionary<Waypoint, Waypoint>();
        bool lookingForEnd = true;
        //int targetRoom = target.GetComponent<CurrentRoom>().number;

        Waypoint startWP = GetClosestWaypoint(actor);
        Waypoint lastWP = GetClosestWaypoint(target);

        process.Enqueue(startWP);

        int looper = 50;
        while(process.Count > 0 && lookingForEnd)
        {
            looper--;
            //print("eka loop");
            //loop kaikki connected -> jonoon jos ei oo jo.
            Waypoint active = process.Dequeue();
            //print("FSFSDFSD " + active.connections.Length);

            foreach(Waypoint connection in active.connections)
            {
                if (!paths.ContainsKey(connection) && connection != startWP)
                {
                    print(connection.name + " added");
                    process.Enqueue(connection);
                    paths.Add(connection, active);

                    if(connection == lastWP)
                    {
                        lookingForEnd = false;
                        break;
                    }
                    
                }
            }
        }

        return CreateRoute(paths,lastWP);
    }

    private List<Waypoint> CreateRoute(Dictionary<Waypoint,Waypoint> paths, Waypoint final)
    {
        List<Waypoint> reversePath = new List<Waypoint>();

        Waypoint cur = final;

        int counter = 0;

        print("Dictionary size: " + paths.Count);

        while(cur != null)
        {
            print("Reversepath loop count: " + counter);
            counter++;
            reversePath.Add(cur);
            Waypoint temp;
            paths.TryGetValue(cur,out temp);
            cur = temp;
        }

        return reversePath;
    }

    private Waypoint[] GetPath()
    {
        return null;
    }

    private Waypoint GetClosestWaypoint(GameObject obj)
    {
        print(obj.name);
        float closest = 999999f;
        int closestIndex = -1;
        int targetRoom = obj.GetComponent<CurrentRoom>().number;

        int index = 0;
        foreach(Waypoint wp in allWaypoints)
        {
            if(wp.room == targetRoom )
            {
                float dist = Vector3.Distance(wp.transform.position, obj.transform.position);
                // Check with raycast that path not blocked.
                //print(Physics2D.Linecast(transform.position, Input.mousePosition,0).collider.name);
                RaycastHit2D[] hits = new RaycastHit2D[2];
                Physics2D.Linecast(obj.transform.position, wp.transform.position, filter, hits);
                if (hits[0].collider != null && !hits[0].collider.name.Equals(obj.name))
                {
                    print(hits[0].collider.name+" esti objectia "+obj.name+" kun tämä testasi "+wp.name+" ja huoneet on "+" o:"+targetRoom+" wp:"+wp.room);
                }
                if (hits[1].collider != null)
                {
                    print(hits[1].collider.name + " esti objectia " + obj.name + " kun tämä testasi " + wp.name + " ja huoneet on " + " o:" + targetRoom + " wp:" + wp.room);
                }
                else if (dist < closest)
                {
                    closest = dist;
                    closestIndex = index;
                }
            }
            index++;
        }

        if (closestIndex != -1)
        {
            return allWaypoints[closestIndex];
        }
        else
        {
            return null;
        }
    }
}
