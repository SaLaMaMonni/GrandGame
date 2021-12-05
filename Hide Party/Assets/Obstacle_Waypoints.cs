using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Waypoints : MonoBehaviour
{
    public Collider2D col;
    public GameObject marker;
    public float offset = 0.5f;
    public GameObject wpParent;

    Vector2[] wpPos;
    List<int> waypoints;
    int blockerLayers;
    private void Awake()
    {
        col = transform.GetComponent<Collider2D>();
        blockerLayers = LayerMask.GetMask("Obstacle", "Avoidable");
    }
    // Start is called before the first frame update
    void Start()
    {
        print(col.bounds.center);
        print(col.bounds.size);

        wpPos = new Vector2[4];

        wpPos[0] = new Vector2(col.bounds.min.x - offset,col.bounds.max.y + offset);
        wpPos[1] = new Vector2(col.bounds.max.x + offset, col.bounds.max.y + offset);
        wpPos[2] = new Vector2(col.bounds.max.x + offset, col.bounds.min.y - offset);
        wpPos[3] = new Vector2(col.bounds.min.x - offset, col.bounds.min.y - offset);
        waypoints = new List<int>();

        int vIndex = 0;

        foreach(Vector2 vec in wpPos)
        {
            if (Physics2D.OverlapCircle(vec, 0.5f, blockerLayers) != null)
            {
                print("Waypoint on " + transform.name + " hit a " + Physics2D.OverlapCircle(vec, 0.5f, blockerLayers).gameObject.name + " in " + transform.parent.parent.name);                
            }
            else
            {
                var intToEnum = (WPdir)vIndex;
                Instantiate(marker, vec, Quaternion.identity, wpParent.transform).name = intToEnum.ToString();
                waypoints.Add(vIndex);
            }
            vIndex++;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            foreach(int dir in waypoints)
            {
                print((WPdir)dir+" is at "+ wpPos[dir]);
            }
        }
    }

    public List<Vector2> GetWaypoints(Vector2 hit, Vector2 target)
    {

        List<Vector2> waypointResults = new List<Vector2>();

        // Pointless, if there are no waypoints just do nothing.
        if (waypoints.Count > 0)
        {
            float lastTargetDistance = 100f;
            float lastHitDistance = 100f;
            int closestToHit = -1;
            int closestToTarget = -1;

            //print("WAYPOINTTEJA ON MUKAMAS " + waypoints.Count);
            for (int i = 0; i < waypoints.Count; i++)
            {
                float hDist = Vector2.Distance(hit, wpPos[waypoints[i]]);
                if (hDist < lastHitDistance)
                {
                    closestToHit = waypoints[i];
                    lastHitDistance = hDist;
                }

                float tDist = Vector2.Distance(target, wpPos[waypoints[i]]);
                if (tDist < lastTargetDistance)
                {
                    closestToTarget = waypoints[i];
                    lastTargetDistance = tDist;
                }
                print(wpPos[waypoints[i]]);
            }

            print(closestToHit + " " + closestToTarget);
            print("PLAYER: " + (WPdir)closestToTarget + "   " + "NPC: " + (WPdir)closestToHit);

            waypointResults.Add(wpPos[closestToHit]);
            if ( Mathf.Abs(Mathf.Abs(closestToHit) - Mathf.Abs(closestToTarget)) == 2)
            {
                
            }
            waypointResults.Add(wpPos[closestToTarget]);
        }
        return waypointResults;
    }
}

enum WPdir
{
    NW,NE,SE,SW
};
