using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_object : MonoBehaviour
{
    public float yspeed;
    private Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, yspeed);
    }
}
