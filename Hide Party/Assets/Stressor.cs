using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stressor : MonoBehaviour
{
    [Range(-10f,10f)]
    public float stressorMultiplier = 1f;
    public float curStressRamp;
    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if(curStressRamp > 0f)
        {
            curStressRamp -= Time.deltaTime;
        }
    }

    public float ReturnStressAmount()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position);

        if (hit.collider.tag == "Player")
        {
            print("Hit player.");
            if (curStressRamp < 1f)
            {
                curStressRamp += Time.deltaTime * 2f;
            }

        }
        else
        {
            print(hit.collider.name);
        }
        return -1f;
    }
}
