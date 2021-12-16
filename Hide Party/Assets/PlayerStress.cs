using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStress : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float stressSpeedModifier = 1f;
    public float stressMultiplier = 1.01f;
    public float checkInterval = 5f;

    bool gettingStressed;
    bool gettingRelief;
    Vector2 feetOffSet;
    float curStress;
    float curStressModifier;
    float curReliefModifier;
    int stressorCount;
    float stressToAdd;

    float checkTimer;

    public Transform stressBarMask;

    PlayerMovement movement;
    Collider2D[] hits;
    int stressorsLayer;



    //DEBUG
    float lastStress = 0f;

    private void Awake()
    {
        checkTimer = checkInterval;
        stressToAdd = 0f;
        feetOffSet = new Vector2(0f, -1.8f);
        curStress = 0f;
        curStressModifier = 1f;
        curReliefModifier = 1f;
        movement = GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        stressorsLayer = LayerMask.GetMask("NPC","StressRelief");
    }

    // Update is called once per frame
    void Update()
    {
        checkTimer -= Time.deltaTime;
        if (checkTimer <= 0f)
        {
            checkTimer = checkInterval;
            hits = Physics2D.OverlapCircleAll((Vector2)transform.position + feetOffSet, 1f,stressorsLayer);

            // Parempi systeemi pitää olla, mutta alkaa mieliala murtuu.
            float stressValue = 0f;
            if(hits.Length > 0)
            {
                foreach (Collider2D hit in hits)
                {
                    Stressor target = hit.GetComponent<Stressor>();
                    if(target != null)
                    {
                        stressValue += target.stressorMultiplier * Vector2.Distance(transform.position, target.transform.position);
                        //print(stressValue);
                    }
                }

                if(stressValue > 0f)
                {
                    curReliefModifier = 1f;
                    gettingStressed = true;
                    gettingRelief = false;
                }

                if(stressValue < 1f)
                {
                    gettingStressed = false;
                    gettingRelief = true;
                }
            }
            else
            {
                gettingRelief = false;
                gettingStressed = false;
            }
        }

        if (gettingStressed)
        {
            foreach (Collider2D hit in hits)
            {
                Stressor target = hit.GetComponent<Stressor>();
                if (target != null)
                {
                    stressToAdd += target.stressorMultiplier * Vector2.Distance(transform.position, target.transform.position) / 20f;
                }
            }

            if (curStressModifier < 300f)
            {
                curStressModifier *= stressMultiplier;// * stressorCount;
            }
            curStress += (stressToAdd * curStressModifier * Time.deltaTime) * stressSpeedModifier;
            //curStress += stressBarMask.localScale.x;
            //if(Time.deltaTime > 0.0)
            //print(Time.deltaTime);
            //print(curStress-lastStress);
            if(curStress > 1f)
            {
                curStress = 1f;
                stressBarMask.localScale = new Vector3(curStress, 1f, 1f);
                GameOver();
            }
        }
        else
        {
            if (curStressModifier > 1f)
            {
                curReliefModifier *= stressMultiplier;
                curStressModifier -= curReliefModifier;
            }
            else
            {
                curStressModifier = 1f;
            }
        }
        
        if(!gettingStressed && gettingRelief)
        {
            foreach (Collider2D hit in hits)
            {
                Stressor target = hit.GetComponent<Stressor>();
                if (target != null)
                {
                    stressToAdd -= target.stressorMultiplier;
                }
            }

            curStress -= (stressToAdd * Time.deltaTime) * stressSpeedModifier;

            if(curStress < 0f)
            {
                curStress = 0f;
            }
        }
        //stressorCount = 0;
        stressToAdd = 0f;


        if(gettingStressed || gettingRelief)
        {
            stressBarMask.localScale = new Vector3(curStress, 1f, 1f);
        }

        //print(curReliefModifier);
    }

    private void GameOver()
    {
        print("I'm outta here!");
        movement.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this);
    }

    /*
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(transform.position + new Vector3(0f,-1.8f,0f), 1f);
    }
    */
}
