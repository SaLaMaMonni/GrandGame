using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Pathfinding : MonoBehaviour
{
    public bool getTarget = false;
    public int currentRoom = -1;

    Collider2D col;
    int layers;
    Vector2 targetPos;
    Transform player;
    
    bool haveTarget;
    Vector2 originOffset;
    // Start is called before the first frame update
    void Start()
    {
        originOffset = new Vector2(0f, -0.3f);
        col = GetComponent<Collider2D>();
        layers = LayerMask.GetMask("Avoidable");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        print(layers);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            getTarget = true;
        }
        if(getTarget)
        {
            Vector2 pos = (Vector2)transform.position + originOffset;
            // OH LORD I HAVE SINNED ONCE MORE
            col.enabled = false;

            targetPos = (Vector2)player.position + originOffset;
            Debug.DrawRay(pos,targetPos - pos,Color.red,10f);
            RaycastHit2D hit = Physics2D.Raycast(pos, targetPos-pos,500f,layers);

            if (hit)
            {
                print("HIT A " + hit.transform.gameObject.name);


                Obstacle_Waypoints wp = hit.transform.gameObject.GetComponent<Obstacle_Waypoints>();
                if (wp != null)
                {
                    print(wp.gameObject.name);
                    wp.GetWaypoints(hit.point, targetPos);
                }
            }
            // SMITE ME DOWN
            col.enabled = true;
            getTarget = false;
        }
    }
}
