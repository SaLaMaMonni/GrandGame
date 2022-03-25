using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stressor : MonoBehaviour
{
    [Range(-10f,10f)]
    public float stressToAdd = 1f;
    public float rampUpTime = 6f;
    public float curStressRamp;
    public float activationDistance = 2f;
    public float deactivateDistance = 2.4f;
    Transform player;
    PlayerStress pStress;
    bool active;
    bool playerLeft;
    //LayerMask playerMask;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pStress = player.GetComponent<PlayerStress>();
        active = false;

        if(activationDistance > 0f)
        {
            CircleCollider2D stressTrigger = gameObject.AddComponent<CircleCollider2D>();
            stressTrigger.isTrigger = true;
            stressTrigger.radius = activationDistance;
        }
    }
    private void Update()
    {
        if (active && !DialogueManager.instance.isTalking)
        {
            float dist = Vector2.Distance(transform.position, player.position);
            //print("DISTANCE: "+dist);

            if(dist < deactivateDistance)
            {
                // Inverse luku eli jos nolla niin 1 ja jos ulkoreunassa eli 2.1 niin nolla
                AdjustStressAmount(1f - (dist / deactivateDistance));
            }
            else if(dist > deactivateDistance && playerLeft)
            {
                active = false;
                //print("DEACTIVATED.");
            }
        }
        else if (curStressRamp > 0f)
        {
            curStressRamp -= Time.deltaTime / rampUpTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active && collision.transform == player)
        {
            active = true;
            playerLeft = false;
            //print("ACTIVATED");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!active && collision.transform == player)
        {
            playerLeft = true;
        }
    }

    public void AdjustStressAmount(float distMult)
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, player.position - transform.position);
        //print("Hit player.");
        if (curStressRamp < 1f)
        {
            curStressRamp += Time.deltaTime / rampUpTime;
        }
        else
        {
            curStressRamp = 1f;
        }

        //print("STRESS ADDED = " + curStressRamp * stressorMultiplier);

        pStress.AdjustStress(stressToAdd, curStressRamp);
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.DrawSphere(transform.position, 2f);
    //}
}
