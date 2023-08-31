using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 3f;
    private float player;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            player = -speed;

        } else if(Input.GetKey(KeyCode.RightArrow)){
            player = speed;
        }else {
            player = 0;
        }
        rb.velocity = new Vector2(player,rb.velocity.y);
    }
}
