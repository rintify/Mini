using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseball : MonoBehaviour
{
    public float force;
    private Rigidbody2D rb = null;
    private AudioSource audioSource = null;
    private float time = 1; 


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();

        Vector3 Force = new Vector3(0, force, 0);
        Invoke("Ball", time);
        //rb.AddForce(Force, ForceMode2D.Impulse);
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

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
