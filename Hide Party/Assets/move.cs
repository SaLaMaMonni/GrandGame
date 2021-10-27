using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    public float speed = 10f;
    private GameObject ob;
    Rigidbody2D rb;
    Vector2 inputti;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        inputti = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKey(KeyCode.DownArrow)) 
        {
            inputti = new Vector2(0, -1);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            inputti = new Vector2(0, 1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputti = new Vector2(-1, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            inputti = new Vector2(1, 0);
        }
        else
        {
            inputti = Vector2.zero;
        }

        rb.AddForce(inputti);

    }
}
