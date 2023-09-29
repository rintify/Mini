using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveobject : MonoBehaviour
{
    public GameObject[] movePoint; //移動経路
    public float speed = 1.0f; //移動速度
    private Rigidbody2D rb = null;
    private int nowPoint = 0;
    private bool returnPoint = false;
    private Vector2 oldPos = Vector2.zero;
    private Vector2 myVelocity = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(movePoint != null && movePoint.Length > 0 && rb != null)
        {
            rb.position = movePoint[0].transform.position;
            oldPos = rb.position;
        }
    }
    public Vector2 GetVelocity()
    {
        return myVelocity;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!returnPoint)
        {
            int nextPoint = nowPoint + 1;
            if(Vector2.Distance(transform.position,movePoint[nextPoint].transform.position) > 0.1f)
            {
                Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                rb.MovePosition(toVector);
            }
            else
            {
                rb.MovePosition(movePoint[nextPoint].transform.position);
                ++nowPoint;
                if(nowPoint + 1 >= movePoint.Length)
                {
                    returnPoint = true;
                }
            }
        }
        else
        {
            int nextPoint = nowPoint - 1;
            if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
            {
                Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                rb.MovePosition(toVector);
            }
            else
            {
                rb.MovePosition(movePoint[nextPoint].transform.position);
                --nowPoint;
                if (nowPoint <= 0)
                {
                    returnPoint = false;
                }
            }
        }
        myVelocity = (rb.position - oldPos) / Time.deltaTime;
        oldPos = rb.position;
    }
}
