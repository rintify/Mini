using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paperplane : MonoBehaviour
{
    public float Speed;
    public bool touch = false;
    private Rigidbody2D rb = null;
    private float size = 1;
    private float xspeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xspeed = -Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(size, size, 1);
            xspeed = -Speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(-size, size, 1);
            xspeed = Speed;
        }
        rb.velocity = new Vector2(xspeed,0);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "wall")
        {
            touch = true;
            Destroy(this.gameObject);
            //Debug.Log("衝突");
        }
    }
}
