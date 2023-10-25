using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseball1 : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    public float force;
    public bool ac;
    private Rigidbody2D rb = null;
    private AudioSource audioSource = null;
    private AudioClip[] audioClips;
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
        //audioSource.PlayOneShot(sound1);
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = rb.position;
       if(ac)
        {
             if(Position.y < 2.4 &&  2.3< Position.y)
            {
                Invoke("acBall", 0);
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
    void acBall()
    {
        force = -1.0f;
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
