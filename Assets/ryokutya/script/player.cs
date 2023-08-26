using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb = null;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  //Rigidbody2D�̎擾
    }

    // Update is called once per frame
    void Update()
    {
        float xSpeed = 0.0f;
       if(Input.GetKey(KeyCode.LeftArrow))
        {
            xSpeed = -speed;
        }
       else if(Input.GetKey(KeyCode.RightArrow))
        {
            xSpeed = speed;
        }
       else
        {
            xSpeed = 0.0f;
        }
        rb.velocity = new Vector2(xSpeed, rb.velocity.y);
    }
}
