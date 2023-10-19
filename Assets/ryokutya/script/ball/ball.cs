using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float speed;
    public bool On = false;
    private Rigidbody2D rb = null;
    private AudioSource audioSource = null;
    private float xforce;
    private float yforce;
    

    // Start is called before the first frame update
    void Start()
    {
        xforce = Random.Range(-1, 1);
        yforce = Random.Range(6, 10);
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Vector3 force = new Vector3(xforce, yforce, 0);
        Vector3 Force = force.normalized * speed;
        rb.AddForce(Force,ForceMode2D.Impulse);
}

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "ground")
        {
            On = true;
            Destroy(this.gameObject);
            //Debug.Log("衝突");
        }
        else if(collision.collider.tag == "wall" || collision.collider.tag == "Player")
        {
            audioSource.Play();
            //Debug.Log("音");
        }
    }
}
