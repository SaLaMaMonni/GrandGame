using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNavigator : MonoBehaviour
{
    [Header("TARGET")]
    public string targetName = "None";
    public Vector2 targetPos;
    [Header("ACTIVE WAYPOINT")]
    public string WPName = "None";
    public Vector2 WPPos;
    [Header("WAYPOINTS")]
    public List<Waypoint> WPList;

    public bool moving;
    public CurrentRoom curRoom;
    private Pathfinding pathfinder;
    public GameObject targetTest;
    private Rigidbody2D rb;
    public float speed = 1f;

    private float lerpPos;
    private Vector2 startPossi;

    private void Awake()
    {
        curRoom = GetComponent<CurrentRoom>();
        rb = GetComponent<Rigidbody2D>();
        lerpPos = 0f;
    }

    private void Start()
    {
        pathfinder = GameObject.Find("/Pathfinding").GetComponent<Pathfinding>();
    }
    public void ChangeTarget()
    {

    }
    private void FixedUpdate()
    {
        if (!moving)
        {
            moving = true;
            WPList = pathfinder.GetWaypoints(gameObject, targetTest);
            startPossi = transform.position;


            foreach(Waypoint wp in WPList)
            {
                print(wp.name);
            }

            print("LENGTH OF THE WAYPOINTS IS: " + WPList.Count);

            targetPos = WPList[WPList.Count - 1].transform.position;
        }

        if (moving)
        {
            //transform.LookAt(targetPos);
            rb.MovePosition(Vector2.Lerp(startPossi, targetPos, lerpPos));
            lerpPos += Time.deltaTime * speed;

            if(lerpPos >= 1f)
            {
                moving = false;
                lerpPos = 0f;
            }
        }

    }
}
