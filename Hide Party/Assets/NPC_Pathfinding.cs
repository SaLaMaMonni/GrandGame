using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Pathfinding : MonoBehaviour
{
    public bool getTarget = false;
    public int currentRoom = -1;

    Collider2D col;
    int layers;
    
    bool haveTarget;
    Vector2 originOffset;
    // Start is called before the first frame update
    void Start()
    {
        originOffset = new Vector2(0f, -0.3f);
        col = GetComponent<Collider2D>();
        layers = LayerMask.GetMask("Player", "Avoidable");
        print(layers);
    }

    // Update is called once per frame
    void Update()
    {
        if(getTarget)
        {
            Vector2 pos = (Vector2)transform.position + originOffset;
            // OH LORD I HAVE SINNED ONCE MORE
            col.enabled = false;

            Debug.DrawRay(pos, (Vector2)GameObject.FindGameObjectWithTag("Player").transform.position - pos,Color.red,10f);
            RaycastHit2D hit = Physics2D.Raycast(pos, (Vector2)GameObject.FindGameObjectWithTag("Player").transform.position-pos,50f,layers);

            if(hit.transform.gameObject != null)
            {
                print(hit.transform.gameObject.name);
                getTarget = false;
            }

            // SMITE ME DOWN
            col.enabled = true;
        }
    }
}
