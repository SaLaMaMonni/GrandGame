using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{

    Vector2 targetPos;
    Vector2[] curWaypoits;
    public int currentRoom;
    public bool getTarget = false;
    Collider2D col;
    public Sprite hitSprite;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

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
        Collider2D[] waypoints = HouseManager.HM.GetRoomWaypoints(currentRoom);
        int closestIndex = -1;
        float closestDistance = 10000f;

        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = false;

        RaycastHit2D[] hit = new RaycastHit2D[10];
        for(int i = 0; i < waypoints.Length; i++)
        {
            Debug.DrawRay(col.bounds.center, waypoints[i].bounds.center - col.bounds.center, Color.yellow, 20f);
            if (Physics2D.Raycast(col.bounds.center, waypoints[i].bounds.center- col.bounds.center, filter,hit, 1000f) > 0)
            {
                print(hit[0].transform.name);
                if(hit[0].transform.gameObject.layer == LayerMask.GetMask("Waypoint"))
                {
                    print(hit[0].transform.position);
                    hit[0].transform.GetComponent<SpriteRenderer>().sprite = hitSprite;
                }
            }
        }
    }
}
