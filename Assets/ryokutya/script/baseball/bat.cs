using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bat : MonoBehaviour
{
    public float savetime;
    public float position;
    public float impulseTorque;
    public bool ball = false;
    private Rigidbody2D rb = null;
    private AudioSource audioSource = null;
    private float time = 0;
    private float speed;
    private bool space = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.centerOfMass = new Vector3(-position, 0, 0);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (space)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddTorque(impulseTorque, ForceMode2D.Impulse);
                space = false;
            }
        }
        if (!space)
        {
            speed = rb.angularVelocity;
            rb.angularVelocity = speed;
            time += Time.deltaTime;
            if (time > savetime)
            {
                time = 0;
                rb.angularVelocity = 0;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ball")
        {
            rb.angularVelocity = speed;
            audioSource.Play();
            ball = true;
            //Debug.Log("衝突");
        }
    }
}
