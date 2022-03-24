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
    public float universalStressSpeed = 1f;
    //public float stressMultiplier = 1.01f;
    //public float checkInterval = 0.1f;

    bool gettingStressed;
    bool gettingRelief;
    //Vector2 feetOffSet;
    float curStress;
    //float curStressModifier;
    //float curReliefModifier;
    //int stressorCount;
    //float stressToAdd;

    //float checkTimer;

    public Transform stressBarMask;

    public bool hasCurve;

    PlayerMovement movement;
    Collider2D[] hits;
    int stressorsLayer;

    public AnimationCurve curve;

    private bool lost;

    //DEBUG
    float lastStress = 0f;

    private void Awake()
    {
        //checkTimer = checkInterval;
        //tressToAdd = 0f;
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

    public void AdjustStress(float adjustment, float rampMult)
    {
        /*
        float dir;
        if(adjustment > 0f)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }

        curStress +=  curve.Evaluate(adjustment) * dir;
        */

        // Ramp
        if (rampMult < 1f && rampMult > 0f)
        {
            curStress += (curve.Evaluate(rampMult) * adjustment) * universalStressSpeed * 0.05f * Time.deltaTime;
        }
        // Raw
        else
        {
            // Voiko raaka lukua k‰ytt‰‰? Kyll‰ kai? Pakko, kun k‰yr‰ voi olla vaan 0 ja 1 v‰lill‰ ni melkeen sama mutta isommilla nopeuksilla.
            curStress += universalStressSpeed * 0.05f * adjustment * Time.deltaTime;
        }
        
        
        if(curStress > 1f)
        {
            stressBarMask.localScale = new Vector3(1f, 1f, 1f);

            if (!GameManager.Instance.hasLost && !lost)
            {
                GameOver();
            }
        }
        else
        {
            stressBarMask.localScale = new Vector3(curStress, 1f, 1f);
        }
    }


    //Update is called once per frame
    /*
    void Update()
    {
        
        //--------fsfsdfsd--------------------------------------------------fsfdsfsdf---------------------------
        checkTimer -= Time.deltaTime;

        if(checkTimer <= 0f)
        {
            // Ei nollata niin on tarkempi average.
            checkTimer += checkInterval;

            Collider2D[] stressors = Physics2D.OverlapCircleAll(transform.position, 2f, stressorsLayer);

            foreach(Collider2D modifier in stressors)
            {
                if (Physics2D.Raycast(transform.position, transform.position - modifier.transform.position))
                {
                    Stressor stress = modifier.GetComponent<Stressor>();

                    curStress += curve.Evaluate(stress.ReturnStressAmount());
                    print(curStress);
                }
            }
        }
        
    }
    */
    private void GameOver()
    {
        lost = true;

        print("I'm outta here!");
        movement.enabled = false;
        GameManager.Instance.GameOver();
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(this);
    }

}
