using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseball2 : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public float force;
    private Rigidbody2D rb = null;
    private AudioSource audioSource = null;
    private AudioClip[] audioClips;
    private float time = 1;
    private Renderer ball;
    private bool ac = true;
    private bool bk = true;
    private bool disapper = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        ball = this.gameObject.GetComponent<SpriteRenderer>();
        Vector3 Force = new Vector3(0, force, 0);
        Invoke("Ball", time);
        //rb.AddForce(Force, ForceMode2D.Impulse);
        //audioSource.PlayOneShot(sound1);
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = rb.position;
       if(bk)
        {
             if(Position.y < 2 &&  1< Position.y)
            {
                Invoke("bkBall", 0);
                bk = false;
            }
        }
       if(disapper)
        {
            if (Position.y < 1 && 0 < Position.y)
            {
                ball.enabled = false;
                audioSource.PlayOneShot(sound2);
                disapper = false;
            }
        }
       if(ac)
        {
            if (Position.y < 0 && -1.2 < Position.y)
            {
                Invoke("acBall", 0);
                ball.enabled = true;
                audioSource.PlayOneShot(sound2);

                ac = false;
            }
        }
    }
    void Ball()
    {
        Vector3 Force = new Vector3(0, force, 0);
        rb.AddForce(Force, ForceMode2D.Impulse);
        audioSource.PlayOneShot(sound1);
        //audioSource.Play();
    }
    void bkBall()
    {
        force = 2.5f;
        Vector3 Force = new Vector3(0, force, 0);
        rb.AddForce(Force, ForceMode2D.Impulse);
    }
    void acBall()
    {
        force = -2.0f;
        Vector3 Force = new Vector3(0, force, 0);
        rb.AddForce(Force, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "bat")
        {
            audioSource.Stop();
        }
    }
}
