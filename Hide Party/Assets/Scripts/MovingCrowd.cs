using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCrowd : MonoBehaviour
{
    [SerializeField] GameObject crowd;

    [SerializeField] Transform[] crowdWaypoints;
    List<Vector3> waypoints;

    Vector3 currentDestination;
    int currentWayPoint;

    bool goingForward = true;
    float targetThreshold = 0.01f;

    [SerializeField] float speed = 3f;

    [SerializeField] float pauseInterval = 5f;
    float timer;
    bool moveOnPause = false;

    void Start()
    {
        SetUpWaypoints();
        SetUpCrowd();
    }

    void Update()
    {
        if (moveOnPause)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                moveOnPause = false;
                timer = pauseInterval;
            }
        }
        else
        {
            MoveBetweenPoints();
        }
    }

    // Moves the crowd and if it reaches its destination, determines a new destination for it
    void MoveBetweenPoints()
    {
        float move = speed * Time.deltaTime;
        crowd.transform.position = Vector3.MoveTowards(crowd.transform.position, currentDestination, move);

        if (Vector3.Distance(crowd.transform.position, currentDestination) < targetThreshold)
        {
            if (currentWayPoint == 0)
            {
                moveOnPause = true;

                goingForward = true;

                currentWayPoint++;
                currentDestination = waypoints[currentWayPoint];
            }
            else if (currentWayPoint == waypoints.Count - 1)
            {
                moveOnPause = true;

                goingForward = false;

                currentWayPoint--;
                currentDestination = waypoints[currentWayPoint];
            }
            else
            {
                if (goingForward)
                {
                    currentWayPoint++;
                    currentDestination = waypoints[currentWayPoint];
                }
                else
                {
                    currentWayPoint--;
                    currentDestination = waypoints[currentWayPoint];
                }
            }
        }
    }

    // Puts all the waypoints into a list for later use.
    void SetUpWaypoints()
    {
        waypoints = new List<Vector3>();

        foreach(Transform waypoint in crowdWaypoints)
        {
            waypoints.Add(waypoint.position);
        }
    }

    // Sets up all the necessary things for the crowd to be able to move.
    void SetUpCrowd()
    {
        timer = pauseInterval;

        transform.position = waypoints[0];
        currentWayPoint++;
        currentDestination = waypoints[currentWayPoint];
    }
}
