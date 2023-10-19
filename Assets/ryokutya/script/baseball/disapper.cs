using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disapper : MonoBehaviour
{
    private Renderer ball;
    private Rigidbody2D rb = null;
    private AudioSource audioSource = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Position = rb.position;
        if (Position.y < 2.5)
        {
            ball.enabled = false;
            audioSource.Play();
        }
        if (Position.y < -2)
        {
            ball.enabled = true;
            audioSource.Play();
        }
    }
}
