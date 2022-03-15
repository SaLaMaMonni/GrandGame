using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveTest : MonoBehaviour
{
    public AnimationCurve curve;
    public Transform start;
    public Transform end;
    float moveTimer;
    float direction = 1f;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        moveTimer += Time.deltaTime * direction;

        if(moveTimer > 1f)
        {
            direction = -1f;
            moveTimer = 0.999f;
        }

        if(moveTimer < 0f)
        {
            direction = 1f;
            moveTimer = 0.001f;
        }

        rb.MovePosition(Vector3.Lerp(start.position, end.position, curve.Evaluate(moveTimer)));
        
    }
}
