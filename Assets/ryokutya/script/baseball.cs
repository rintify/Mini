using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseball : MonoBehaviour
{
    public float force;
    public bool disapper;
    private Rigidbody2D rb = null;
    private AudioSource audioSource = null;
    private float time = 1;
    private Renderer ball;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        ball = this.gameObject.GetComponent<SpriteRenderer>();
        Vector3 Force = new Vector3(0, force, 0);
        Invoke("Ball", time);
        //rb.AddForce(Force, ForceMode2D.Impulse);
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = rb.position;
       if(disapper)
        {
             if(Position.y < 2.5)
            {
                ball.enabled = false;
            }
            if (Position.y < -2)
            {
                ball.enabled = true;
                disapper = false;
            }
        }
    }
    void Ball()
    {
        Vector3 Force = new Vector3(0, force, 0);
        rb.AddForce(Force, ForceMode2D.Impulse);
        audioSource.Play();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "bat")
        {
            audioSource.Stop();
        }
    }
}
