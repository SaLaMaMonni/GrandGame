using System.Collections;
using System.Collections.Generic;
using UnityEngine;





/*
 * Varmasti voi yhdist‰‰ systeemin niin ettei tarvii kahta kertaa tehd‰ juttuja. Siis kun arvot on valmiiks joko pos tai neg ni pit‰is toimii.
 * Normalisoi. Vauhti tuntuu hitaammalt Lauran koneel (ehk‰ ruutu lag? en usko)
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

public class PlayerStress : MonoBehaviour
{
    [Range(0.1f, 10f)]
    public float stressSpeedModifier = 1f;
    public float stressMultiplier = 1.01f;
    public float checkInterval = 0.1f;

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

    public bool hasCurve;

    PlayerMovement movement;
    Collider2D[] hits;
    int stressorsLayer;

    public AnimationCurve curve;
    


    //DEBUG
    float lastStress = 0f;

    private void Awake()
    {
        checkTimer = checkInterval;
        stressToAdd = 0f;
        //feetOffSet = new Vector2(0f, -1.8f);
        curStress = 0f;
        //curStressModifier = 1f;
        //curReliefModifier = 1f;
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
        //--------fsfsdfsd--------------------------------------------------fsfdsfsdf---------------------------
        checkTimer -= Time.deltaTime;

        if(checkTimer <= 0f)
        {
            checkTimer = checkInterval;

            //Physics2D.OverlapCircleAll(transform.position,2)
        }

    }

    private void GameOver()
    {
        print("I'm outta here!");
        movement.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this);
    }

}
