using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : MonoBehaviour
{
    private Dog dog;
    public float speed = 1;
    Vector2 dir;
    public float force;
    
    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("Dog").GetComponent<Dog>();
        dir = (dog.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        var dogDir = (dog.transform.position - transform.position).normalized;
        transform.position += (Vector3)(speed * Time.deltaTime * dir);
        if(Vector2.Dot(dir,dogDir) < 0.9){
            dir = Quaternion.Euler(0, 0, 540f*Time.deltaTime)*dir;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        other.GetComponent<Rigidbody2D>().AddForce(dir*force);
        dir *= -1;
    }
}
